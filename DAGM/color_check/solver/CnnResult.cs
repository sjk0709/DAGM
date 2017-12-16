using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAGM.solver
{
    class CnnResult
    {
        private int _classNo;
        private int _nDefect;
        private float[] _results;

        public CnnResult(int classNo, int nDefect)
        {
            _classNo = classNo;
            _nDefect = nDefect;
        }

        public CnnResult(float[] results, int nAreas)
        {
            _results = new float[nAreas];
            _results = results;
        }

        public int ClassNo
        {
            get
            {
                return _classNo;
            }
            set
            {
                _classNo = value;
            }
        }

        public int nDefect
        {
            get
            {
                return _nDefect;
            }
            set
            {
                _nDefect = value;
            }
        }

        // Declare a Name property of type string:
        public float[] Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
            }
        }

    }
}
