using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Drawing.Imaging;

using System.IO;

using OpenCvSharp;
using OpenCvSharp.Extensions;



namespace DAGM.solver
{
    class CnnSolver
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        [DllImport("tensorflowJK.dll")]
        public static extern IntPtr runCnnBlockC1(string graphName, string resultImagePath, int width, int height, IntPtr pImage);

        [DllImport("tensorflowJK.dll")]
        public static extern int ReleaseMemory(IntPtr ptr);

        private Def _def = new Def();
        private Utils _utils = new Utils();
        private DAGMsetting _dagmSetting = null;

        public CnnSolver()
        {
            _dagmSetting = new DAGMsetting();
        }


        public CnnResult Run(Mat image, string graphName)
        {
            //object setting;
            //Type type = typeof(DAGMsetting);
            //_utils.LoadXML(type, out setting, _utils.GetXMLFullPath(_def.MainInspectSettingPath, 0));
            //_dagmSetting = (DAGMsetting)setting;
            //ModelSetting modelSetting = _dagmSetting.ModelSetting;
            //string graphName = modelSetting.ModelName + modelSetting.FeatureWidth + "x" + modelSetting.FeatureHeight;
            ////Console.WriteLine("test : " + graphName);
            
            IntPtr inputPtr = image.Ptr();
            
            IntPtr resultPtr = runCnnBlockC1(graphName, _def.ResultImagePath, image.Cols, image.Rows, inputPtr);

            int[] results = new int[2];           // result is allocated
            Marshal.Copy(resultPtr, results, 0, 2);   // copy resultPtr to result             
            ReleaseMemory(resultPtr);

            int classNo = results[0];
            int nDefect = results[1];
            CnnResult result = new CnnResult(classNo, nDefect);               

            return result;
        }

    }
}
