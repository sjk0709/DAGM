using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAGM.solver
{    
    public class ModelSetting
    {
        private string modelName = "DAGM32x32";
        private string featureWidth = "32";
        private string featureHeight = "32";
        private string labelSize = "12";

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public string FeatureWidth
        {
            get { return featureWidth; }
            set { featureWidth = value; }
        }

        public string FeatureHeight
        {
            get { return featureHeight; }
            set { featureHeight = value; }
        }

        public string LabelSize
        {
            get { return labelSize; }
            set { labelSize = value; }
        }


    }
}

