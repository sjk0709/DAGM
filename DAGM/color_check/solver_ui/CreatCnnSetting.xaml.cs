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

using System.IO;
using System.Xml;
using System.Xml.Serialization;

using DAGM.solver;

namespace DAGM.solver_ui
{
    /// <summary>
    /// CreatCnnSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CreatCnnSetting : Window
    {        
        private Def _def = new Def();
        private Utils _utils = new Utils();        

        private DAGMsettingDataSet _dagmSettingDataSet = null;

        public CreatCnnSetting()
        {
            InitializeComponent();

            _dagmSettingDataSet = new DAGMsettingDataSet();

        }

        private void btnSaveSetting_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            if (_utils.SaveXmlDialog(new Def().DAGMSettingPath, ref fileName))
            {
                _dagmSettingDataSet.ModelName = ModelName.Text;
                _dagmSettingDataSet.FeatureWidth = FeatureWidth.Text;
                _dagmSettingDataSet.FeatureHeight = FeatureHeight.Text;
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(DAGMsettingDataSet));
                    using (StreamWriter streamWriter = new StreamWriter(fileName))
                    {
                        xmlSerializer.Serialize(streamWriter, _dagmSettingDataSet);
                        // Cleanup
                        streamWriter.Close();
                    }                   

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("XML 생성 실패");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
