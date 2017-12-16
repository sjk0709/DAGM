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
using System.Windows.Shapes;

using System.Diagnostics;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using DAGM.solver;


namespace DAGM.solver_ui
{
    /// <summary>
    /// CnnSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CnnSetting : Window
    {
        private Def _def = new Def();
        private Utils _utils = new Utils();

        public CnnSetting()
        {
            InitializeComponent();

            RefreshListBox();
        }


        private void RefreshListBox()
        {
            RefreshListBox(ListBoxSolverSetting, _def.DAGMSettingPath);
            
            //if (rbBasic.IsChecked == true)
            //{
            //    RefreshListBox(lbSolverSetting, def.BasicSettingPath);
            //}
            //else if (rbSVM.IsChecked == true)
            //{
            //    RefreshListBox(lbSolverSetting, def.SVMSettingPath);
            //}
            //else if (rbCNN.IsChecked == true)
            //{
            //    RefreshListBox(lbSolverSetting, def.CNNSettingPath);
            //}

        }

        private void RefreshListBox(ListBox listbox, string path)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                listbox.Items.Clear();
                FileInfo[] Files = dInfo.GetFiles(Def.SavedSettingExtention);

                foreach (FileInfo file in Files)
                {
                    listbox.Items.Add(file.Name);
                }
            }
            catch { }
        }

        private string GetXMLFullPath(string rootPath, int idx)
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

        private void UpdateInfoXML(Label lbl, TextBlock tb, string path, int selectedIdx)
        {
            try
            {
                string fullPath = GetXMLFullPath(path, selectedIdx);
                string xml = _utils.GetXML(fullPath);
                tb.Text = xml;
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.Message);
            }
        }


        private void ListBoxSolverSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInfoXML(LabelInfoSolverSetting, TextBlockSolverSetting, _def.DAGMSettingPath, ListBoxSolverSetting.SelectedIndex);
        }

        private void btnCreateNewSetting_Click(object sender, RoutedEventArgs e)
        {
            solver_ui.CreatCnnSetting createCnnSetting = new solver_ui.CreatCnnSetting();
            createCnnSetting.Owner = System.Windows.Application.Current.MainWindow;
            createCnnSetting.ShowDialog();
            RefreshListBox(ListBoxSolverSetting, _def.DAGMSettingPath);
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
