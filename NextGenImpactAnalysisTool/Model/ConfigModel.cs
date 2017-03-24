using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class ConfigModel
    {
        public ConfigModel() { }
        public string LocalFilename
        {
            get;
            set;
        }
        public string Serverfilename
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
    }
}
