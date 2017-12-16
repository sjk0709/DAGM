using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAGM.solver;

namespace DAGM.solver
{
    public class DAGMsetting
    {
        private ModelSetting _modelSetting;
        
        public DAGMsetting()
        {
            _modelSetting = new ModelSetting();            
        }

        public ModelSetting ModelSetting
        {
            get { return _modelSetting; }
            set { _modelSetting = value; }
        }
    }
}
