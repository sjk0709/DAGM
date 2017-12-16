using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DAGM.solver
{
    public class Def
    {
        public const int Gray_Max_Value = 255;
        public const int Gray_Max_Cnt = Gray_Max_Value + 1;
        public const int JAI_AT_200GE_ResolutionX = 1624;
        public const int JAI_AT_200GE_ResolutionY = 1236;

        public string StartupPath { get { return System.AppDomain.CurrentDomain.BaseDirectory; } }
        public string SettingPath { get { return StartupPath + "Settings\\"; } }
        public string MainInspectSettingPath { get { return SettingPath + "_Main\\"; } }
        public string HueSettingPath { get { return SettingPath + "HueSettings\\"; } }
        public string SegmentSettingPath { get { return SettingPath + "SegmentSettings\\"; } }
        public string ScatterSettingPath { get { return SettingPath + "ScatterSettings\\"; } }
        public string BasicSettingPath { get { return SettingPath + "BasicSettings\\"; } }
        public string SVMSettingPath { get { return SettingPath + "SVMSettings\\"; } }
        public string CNNSettingPath { get { return SettingPath + "CNNSettings\\"; } }
        public string DAGMSettingPath { get { return SettingPath + "DAGMSettings\\"; } }
        public string EnvironmentSettingPath { get { return SettingPath + "EnvironmentSettings\\EnvironmentSetting.xml"; } }

        public string CSV_Delimiter { get { return ","; } }
        public string OutputCSVFileName { get { return "_Output.csv"; } }
        public string OutputCSVFIleHeader
        { 
            get
            {
                return "Color FileName" + CSV_Delimiter
                    + "Laser FileName" + CSV_Delimiter
                    + "Delta E" + CSV_Delimiter
                    + "Delta Ec" + CSV_Delimiter
                    + "Delta L" + CSV_Delimiter
                    + "Scatter" + CSV_Delimiter
                    + "CIE L*" + CSV_Delimiter
                    + "CIE a*" + CSV_Delimiter
                    + "CIE b*" + CSV_Delimiter
                    + "Date/Time" + CSV_Delimiter
                    + Environment.NewLine;
            }
        }
        public const string SavedSettingExtention = "*.XML";

        public const byte MSG_LASER_ON = 0x01;
        public const byte MSG_LASER_OFF = 0x00;

        public const string AT_200GE = "AT-200GE";  // "ModelName"
        public const string GO_5101C_PGE = "GO-5101C-PGE";

        public string Success { get { return "Success"; } }
        public string Fail { get { return "Fail"; } }
    }
}