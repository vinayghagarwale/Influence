using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class OtherDetails
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public string Module { get; set; }
        public string Complexity { get; set; }
    }
    public class OtherDetailsList : List<OtherDetails>
    {
        public OtherDetailsList()
        {
            this.Add(new OtherDetails());
        }
    }
}
