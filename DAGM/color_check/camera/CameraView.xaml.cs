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
using System.Drawing.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace DAGM.camera
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CameraView : UserControl
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        
        //[DllImport("dlltest.dll")]
        //public static extern bool LoadBmp(char* filename, byte** pImage);
        
        [DllImport("dlltest.dll")]
        public static extern bool SaveBmp(string path, IntPtr pImage, int width, int height);
        


        private Mat imgMat;
        private bool _isEmpty;

        public Mat ImgMat
        {
            get
            {
                return imgMat;
            }
            set
            {
                imgMat = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
            }
        }

        public CameraView()
        {
            _isEmpty = true;
        }

        public bool SetImage(string path)
        {
            InitializeComponent();

            //   Bitmap m_Bitmap = new Bitmap("a.jpg");
            imgMat = new Mat();
            //imgMat = BitmapConverter.ToMat(m_Bitmap); 

            imgMat = Cv2.ImRead(path, 0);
            
            //Cv2.ImWrite("a.bmp", imgMat);
            //Cv2.ImShow("1", imgMat);
            Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(imgMat);

            BitmapData bmpData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite,
            System.Drawing.Imaging.PixelFormat.Format24bppRgb);

       
            bitmap.UnlockBits(bmpData); //Remember to unlock!!!

            byte[] bytes1 = imgMat.ToBytes(".png");
            byte[] bytes2;
            Cv2.ImEncode(".jpg", imgMat, out bytes2);


           // Cv2.ImWrite("tiger_clone.bmp", imgMat);
            

           // SaveBmp("tiger_clone.bmp", bmpData.Scan0, bitmap.Width, bitmap.Height);
          
            WriteableBitmap wb = WriteableBitmapConverter.ToWriteableBitmap(imgMat);
            InputImage.Source = wb;

            _isEmpty = false;

            return imgMat.Empty();
        }
       
    }
}
