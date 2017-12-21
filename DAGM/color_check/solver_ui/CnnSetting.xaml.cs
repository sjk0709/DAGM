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
        private DAGMsetting _dagmSetting = null;
        private bool _existsGraphFile = false;

        public CnnSetting()
        {
            InitializeComponent();

            RefreshListBox();
        }


        private void RefreshListBox()
        {
            RefreshListBox(ListBoxSolverSetting, _def.modelSettingPath);
            
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

        private void UpdateInfoXML(Label lbl, TextBlock tb, string path, int selectedIdx)
        {
            try
            {
                string fullPath = _utils.GetXMLFullPath(path, selectedIdx);
                string xml = _utils.GetXML(fullPath);
                tb.Text = xml;
                
                object setting;
                Type type = typeof(ModelSetting);
                _utils.LoadXML(type, out setting, fullPath);
                ModelSetting modelSetting = (ModelSetting)setting;


                string graphFileFullPath = _def.TensorflowGraphPath + modelSetting.ModelName + ".pb";
                _existsGraphFile = File.Exists(graphFileFullPath);
                //Console.WriteLine(File.Exists(graphFileFullPath) ? "File exists." : "File does not exist.");
                
                //string fileName = System.IO.Path.GetFileName(fullPath);
                string fileName = "None";
                if (_existsGraphFile)
                {
                    fileName = modelSetting.ModelName;
                }                
                GraphFileName.Text = fileName;
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.Message);
            }
        }


        private void ListBoxSolverSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInfoXML(LabelInfoSolverSetting, TextBlockSolverSetting, _def.modelSettingPath, ListBoxSolverSetting.SelectedIndex);

        }

        private void btnCreateNewSetting_Click(object sender, RoutedEventArgs e)
        {
            solver_ui.CreatCnnSetting createCnnSetting = new solver_ui.CreatCnnSetting();
            createCnnSetting.Owner = System.Windows.Application.Current.MainWindow;
            createCnnSetting.ShowDialog();
            RefreshListBox(ListBoxSolverSetting, _def.modelSettingPath);
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ConstSetting()
        {
            _dagmSetting = new DAGMsetting();

            if (ListBoxSolverSetting.SelectedIndex > -1)
            {
                object setting;
                Type type = typeof(ModelSetting);
                _utils.LoadXML(type, out setting, _utils.GetXMLFullPath(_def.modelSettingPath, ListBoxSolverSetting.SelectedIndex));
                _dagmSetting.ModelSetting = (ModelSetting)setting;
            }        
        }
                

        private void ButtonSaveSetting_Click(object sender, RoutedEventArgs e)
        {
            _utils.makeFolder(_def.MainInspectSettingPath);
            
            ConstSetting();
                        
            if(!_existsGraphFile)
            {
                System.Windows.MessageBox.Show("Current setting doesn't have any graph files.");

            }
            //if (TextBoxSaveSetting.Text == "" || TextBoxSaveSetting.Text == null)
            //{
            //    MessageBox.Show("Fill Inspect Setting Name");
            //}
            else
            {
                try
                {
                    _utils.DeleteAllFilesInFolder(_def.MainInspectSettingPath);

                    //string path = _def.MainInspectSettingPath + TextBoxSaveSetting.Text + ".xml";
                    string path = _def.MainInspectSettingPath + "dagmSettings.xml";
                    _utils.SaveXML(_dagmSetting, path);

                    this.Close();
                }
                catch (Exception e1)
                {
                    Debug.WriteLine(e1);
                    MessageBox.Show("Fail to save setting.");
                }
            }
        }
    }
}
