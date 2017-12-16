using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Drawing.Imaging;

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
        public static extern IntPtr runCnnBlockC1(int width, int height, IntPtr pImage);

        [DllImport("tensorflowJK.dll")]
        public static extern int ReleaseMemory(IntPtr ptr);

        public CnnResult Run(Mat image)
        {
            int nx = 10;
            int ny = 3;
            int nAreas = nx * ny;
              
            IntPtr inputPtr = image.Ptr();
            
            IntPtr resultPtr = runCnnBlockC1(image.Cols, image.Rows, inputPtr);

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
