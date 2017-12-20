using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Runtime.InteropServices;

using System.Drawing;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using DAGM.solver;

namespace DAGM.solver_ui
{
    /// <summary>
    /// CNN.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CnnResultView : UserControl
    {

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        private Def _def = new Def();
        private Utils _utils = new Utils();
        private DAGMsetting _dagmSetting = null;

        public CnnResultView()
        {
            InitializeComponent();            
        }

        public string[] Run(Mat image)
        {
            //   Bitmap m_Bitmap = new Bitmap("a.jpg");
            //Mat imgMat = new Mat();
            //imgMat = BitmapConverter.ToMat(m_Bitmap);         
            //imgMat = Cv2.ImRead("S8_A_02NG_01_L190.bmp");
            //Cv2.ImShow("1", imgMat);

            object setting;
            Type type = typeof(DAGMsetting);
            _utils.LoadXML(type, out setting, _utils.GetXMLFullPath(_def.MainInspectSettingPath, 0));
            _dagmSetting = (DAGMsetting)setting;
            ModelSetting modelSetting = _dagmSetting.ModelSetting;            
            string graphPath = _def.TensorflowGraphPath + modelSetting.ModelName;

            // parameters in CNN settings           
            int featureWidth = Convert.ToInt32(modelSetting.FeatureWidth);
            int featureHeight = Convert.ToInt32(modelSetting.FeatureHeight);
            int width = image.Cols;
            int height = image.Rows;
            int nx = width/featureWidth;
            int ny = height/featureHeight;            
            int nAreas = nx * ny;

            // solver
            solver.CnnSolver cnn = new solver.CnnSolver();
            solver.CnnResult cnnResult = cnn.Run(image, graphPath);
            
            int classNo = cnnResult.ClassNo;
            int nDefect = cnnResult.nDefect;

            if (classNo < 0)
            {
                string[] result = new string[3];
                result[1] = "None";
                result[2] = "None";
                result[3] = "None";
                return result;
            }

            Mat resultImage = Cv2.ImRead(_def.ResultImagePath);

            //Console.WriteLine("classNo : " + classNo.ToString());
            //Console.WriteLine("nDefect : " + nDefect.ToString());

            //for (int j = 0; j < ny; j++)
            //    for (int i = 0; i < nx; i++)
            //    {
            //        int startX = i * featureWidth;
            //        int startY = j * featureHeight;

            //        OpenCvSharp.CPlusPlus.Point point = new OpenCvSharp.CPlusPlus.Point(startX, startY + (int)(0.75 * featureHeight));
            //        CvScalar color = new CvScalar(0, 255, 255);

            //        ===== call defect inspection results =====//
            //        String blockResult = "12";
            //        Cv2.PutText(temp, blockResult, point, 0, 0.7, color, 2);

            //        OpenCvSharp.CPlusPlus.Rect block = new OpenCvSharp.CPlusPlus.Rect(startX, startY, featureWidth, featureHeight);
            //        Cv2.Rectangle(temp, block, color, 2);
            //    }


            WriteableBitmap wb = WriteableBitmapConverter.ToWriteableBitmap(resultImage);
            ResultImage.Source = wb;

            string[] finalResult = new string[3];
            finalResult[0] = classNo.ToString();
            finalResult[1] = nDefect.ToString();
            finalResult[2] = "OK";
            if (nDefect > 0)
                finalResult[2] = "NG";

            return finalResult;
        }
    }
}
