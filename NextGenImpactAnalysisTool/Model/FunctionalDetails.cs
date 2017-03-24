using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class FunctionalDetails
    {
        public FunctionalDetails() { }

        public string ImpactDescription
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public string ModuleName
        {
            get;
            set;
        }

        public string Complexity
        {
            get;
            set;
        }

        public LinkModel strLink
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
    }
    public class FunctionalDetailss : List<FunctionalDetails>
    {
        public FunctionalDetailss()
        {
            this.Add(new FunctionalDetails());
        }
    }
}
