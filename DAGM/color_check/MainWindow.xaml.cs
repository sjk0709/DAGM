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
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;


using DAGM.solver;

namespace DAGM
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        private camera.CameraView InputView;
        solver_ui.ResultMainView resultMain;
        private solver_ui.CnnResultView cnnResultView;
        private solver_ui.SolverSettings solverSettings;

        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();
                        
            ResultView.DataContext = resultMain;

            InputView = new camera.CameraView();
            cnnResultView = new solver_ui.CnnResultView();
            solverSettings = new solver_ui.SolverSettings();

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "png files (*.png)|*.png";    
            dlg.Title = "Open Images";

            //Open the Pop-Up Window to select the file 
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                new FileInfo(dlg.FileName);
                //using (Stream s = dlg.OpenFile())
                //{
                //    TextReader reader = new StreamReader(s);
                //    string st = reader.ReadToEnd();
                //    //txtPath.Text = dlg.FileName;
                //}

                InputView.SetImage(dlg.FileName);
                ImageView.DataContext = InputView;


                //ResultView.DataContext = resultMain;
                //string[] result = cnnResultView.Run(InputView.ImgMat);
                ClassNo.Text = "None";
                nDefect.Text = "None";
                FinalResult.Text = "None";
            }
        }

        private void CnnSetting_Click(object sender, RoutedEventArgs e)
        {
            solver_ui.CnnSetting cnnSetting = new solver_ui.CnnSetting();
            cnnSetting.Owner = System.Windows.Application.Current.MainWindow;
            cnnSetting.ShowDialog();     
        }

        private void CnnRun_Click(object sender, RoutedEventArgs e)
        {

            string mainInspectSettingPath = new Def().MainInspectSettingPath;

            if (System.IO.Directory.Exists(mainInspectSettingPath))      // if there is the folder
            {
                if(Directory.EnumerateFileSystemEntries(mainInspectSettingPath).Any())   // if the folder isn't empty)
                {
                    if (!InputView.IsEmpty)
                    {
                        ResultView.DataContext = cnnResultView;
                        string[] result = cnnResultView.Run(InputView.ImgMat);
                        ClassNo.Text = result[0];
                        nDefect.Text = result[1];
                        FinalResult.Text = result[2];
                    }
                }
            }            

        }

        private void ManuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
