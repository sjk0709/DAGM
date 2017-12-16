using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using OpenCvSharp;
using System.Drawing;
using System.Diagnostics;


namespace DAGM.solver
{
    public class Utils
    {
        private Def def = new Def();

        // it deletes all files in a folder 
        public void DeleteAllFilesInFolder(string folderPath)   // created by JK 
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        // it get an absolute Path of idx's file in folder
        public string GetXMLFullPath(string rootPath, int idx)  // modifed by JK
        {
            string fullPath = "";

            if (idx > -1)
            {
                DirectoryInfo dInfo = new DirectoryInfo(rootPath);
                FileInfo[] Files = dInfo.GetFiles(Def.SavedSettingExtention);
                if (Files.Count() > idx)
                {
                    fullPath = Files[idx].FullName;
                }
            }

            return fullPath;
        }

        public bool OpenFolder(ref string folderPath)
        {
            bool res = false;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            
            try
            {
                fbd.SelectedPath = folderPath;
            }
            catch { }

            System.Windows.Forms.DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = fbd.SelectedPath;
                res = true;
            }

            return res;
        }

        public bool OpenMainSetting(ref string fileName)
        {
            bool res = false;
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";
            ofd.InitialDirectory = def.MainInspectSettingPath;
            ofd.Title = "Test 이미지 선택";
            if ((bool)ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                res = true;
            }

            return res;
        }

        public bool OpenBmpFiles(ref string[] imagePaths)
        {
            bool res = false;
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "bmp files (*.bmp)|*.bmp";
            ofd.InitialDirectory = @"c:\";
            ofd.Title = "Test 이미지 선택";
            if ((bool)ofd.ShowDialog() == true)
            {
                imagePaths = ofd.FileNames;
                res = true;
            }

            return res;
        }

        public bool CreateFolder(string fullPath, string folderName, bool bOverwrite = true)
        {
            bool res = true;
            string path = fullPath + "\\" + folderName;
            DirectoryInfo di = new DirectoryInfo(fullPath + path);

            if (bOverwrite)
            {
                if (di.Exists == false)
                {
                    di.Create();
                }
            }
            else
            {
                if (di.Exists == false)
                {
                    res = false;
                }
            }

            return res;
        }

        public bool SaveBitmap(Bitmap bitmap, string name)
        {
            bool bResult = true;

            if (bitmap != null)
            {
                try
                {
                    bitmap.Save(name);
                }
                catch (Exception e)
                {
                    bResult = false;
                    Debug.WriteLine("Bitmap save error: " + e.Message);
                }
            }
            else
            {
                bResult = false;
            }

            return bResult;
        }

        public void LoadXML(Type type, out object setting, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            using (StreamReader streamReader = new StreamReader(path))
            {
                setting = xmlSerializer.Deserialize(streamReader);
            }
        }

        public void SaveXML(object setting, string path)
        {
            string folderPath = System.IO.Path.GetDirectoryName(path);
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (di.Exists == false)
            {
                di.Create();
            }
            XmlSerializer xmlSerializer = new XmlSerializer(setting.GetType());
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                xmlSerializer.Serialize(streamWriter, setting);
            }
        }

        public bool SaveXmlDialog(string path, ref string fullName)
        {
            bool res = false;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.InitialDirectory = path;
            dlg.Title = "Save Settings";

            DirectoryInfo di = new DirectoryInfo(dlg.InitialDirectory);

            if (di.Exists == false)
            {
                di.Create();
            }

            if (dlg.ShowDialog() == true)
            {
                res = true;
                fullName = dlg.FileName;
            }

            return res;
        }

        public string GetXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            doc.Load(fs);
            StringBuilder sb = new StringBuilder();
            System.IO.TextWriter tr = new System.IO.StringWriter(sb);
            XmlTextWriter wr = new XmlTextWriter(tr);
            wr.Formatting = Formatting.Indented;
            doc.Save(wr);
            wr.Close();
            return sb.ToString();
        }

        public class CIE_Lab
        {
            public double L = 0;
            public double a = 0;
            public double b = 0;
            public CIE_Lab() { }
            public CIE_Lab(double CIE_L, double CIE_a, double CIE_b)
            {
                L = CIE_L;
                a = CIE_a;
                b = CIE_b;
            }
        }

        public CIE_Lab RGB2Lab(RGB rgb)
        {
            CIE_Lab Lab = new CIE_Lab(0, 0, 0);

            double scaled_R = rgb.R / (double)Def.Gray_Max_Value;
            double scaled_G = rgb.G / (double)Def.Gray_Max_Value;
            double scaled_B = rgb.B / (double)Def.Gray_Max_Value;

            if (scaled_R > 0.04045)
            {
                scaled_R = Math.Pow((scaled_R + 0.055) / 1.055, 2.4);
            }
            else
            {
                scaled_R /= 12.92;
            }
        
            if (scaled_G > 0.04045)
            {
                scaled_G = Math.Pow((scaled_G + 0.055) / 1.055, 2.4);
            }
            else
            {
                scaled_G /= 12.92;
            }                    

            if (scaled_B > 0.04045)
            {
                scaled_B = Math.Pow((scaled_B + 0.055) / 1.055, 2.4);
            }
            else
            {
                scaled_B /= 12.92;
            }
                    
            scaled_R *= 100;
            scaled_G *= 100;
            scaled_B *= 100;

            // -- Observer. = 2°, Illuminant = D65
            double X = scaled_R * 0.4124 + scaled_G * 0.3576 + scaled_B * 0.1805;
            double Y = scaled_R * 0.2126 + scaled_G * 0.7152 + scaled_B * 0.0722;
            double Z = scaled_R * 0.0193 + scaled_G * 0.1192 + scaled_B * 0.9505;

            // 2) XYZ -> Lab
            // Observer= 2°, Illuminant= D65
            double ref_X = 95.047;
            double ref_Y = 100.000;
            double ref_Z = 108.883;

            double var_X = X / ref_X;
            double var_Y = Y / ref_Y;
            double var_Z = Z / ref_Z;

            double param1 = 1.0 / 3.0;
            double param2 = 16.0 / 116.0;

            if (var_X > 0.008856)
            {
                var_X = Math.Pow(var_X, param1);
            }
            else
            {
                var_X = 7.787 * var_X + param2;
            }                

            if (var_Y > 0.008856)
            {
                var_Y = Math.Pow(var_Y, param1);
            }
            else
            {
                var_Y = 7.787 * var_Y + param2;
            }

            if (var_Z > 0.008856)
            {
                var_Z = Math.Pow(var_Z, param1);
            }
            else
            {
                var_Z = (7.787 * var_Z) + param2;
            }

            Lab.L = (116 * var_Y) - 16;
            Lab.a = 500 * (var_X - var_Y);
            Lab.b = 200 * (var_Y - var_Z);

            return Lab;
        }

        public class RGB
        {
            public double R = 0;
            public double G = 0;
            public double B = 0;
            public RGB() { }
            public RGB(double R, double G, double B)
            {
                this.R = R;
                this.G = G;
                this.B = B;
            }
        }

        public RGB Lab2RGB(CIE_Lab Lab)
        {
            RGB rgb = new RGB(0, 0, 0);

            double var_Y = (Lab.L + 16) / 116;
            double var_X = Lab.a / 500 + var_Y;
            double var_Z = var_Y - Lab.b / 200;

            double param1 = 1.0 / 2.4;
            double param2 = 16.0 / 116.0;

            if (Math.Pow(var_Y, 3) > 0.008856)
            {
                var_Y = Math.Pow(var_Y, 3);
            }
            else
            {
                var_Y = (var_Y - param2) / 7.787;
            }

            if (Math.Pow(var_X ,3) > 0.008856)
            {
                var_X = Math.Pow(var_X, 3);
            }
            else
            {
                var_X = (var_X - param2) / 7.787;
            }

            if (Math.Pow(var_Z ,3) > 0.008856)
            {
                var_Z = Math.Pow(var_Z ,3);
            }
            else
            {
                var_Z = (var_Z - param2) / 7.787;
            }                

            // Observer= 2°, Illuminant= D65
            double ref_X = 95.047;
            double ref_Y = 100.000;
            double ref_Z = 108.883;

            double X = ref_X * var_X;
            double Y = ref_Y * var_Y;
            double Z = ref_Z * var_Z;

            // Observer = 2°, Illuminant = D65
            var_X = X / 100;  // Where X = 0 ÷  95.047
            var_Y = Y / 100;  // Where Y = 0 ÷ 100.000
            var_Z = Z / 100;  // Where Z = 0 ÷ 108.883

            double var_R = var_X * 3.2406 + var_Y * -1.5372 + var_Z * -0.4986;
            double var_G = var_X * -0.9689 + var_Y * 1.8758 + var_Z * 0.0415;
            double var_B = var_X * 0.0557 + var_Y * -0.2040 + var_Z * 1.0570;

            if (var_R > 0.0031308)
            {
                var_R = 1.055 * Math.Pow(var_R, param1) - 0.055;
            }
            else
            {
                var_R *= 12.92;
            }
            
            if (var_G > 0.0031308)
            {
                var_G = 1.055 * Math.Pow(var_G, param1) - 0.055;
            }
            else
            {
                var_G *= 12.92;
            }
                
            if (var_B > 0.0031308)
            {
                var_B = 1.055 * Math.Pow(var_B, param1) - 0.055;
            }
            else
            {
                var_B *= 12.92;
            }                

            rgb.R = var_R * 255;
            rgb.G = var_G * 255;
            rgb.B = var_B * 255;

            return rgb;
        }
    }
}
