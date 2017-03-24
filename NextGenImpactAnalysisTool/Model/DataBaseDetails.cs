using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class DataBaseDetails
    {
        public DataBaseDetails() { }

        public string Typename {get;set;}
        public string Type { get; set; }
        public string Description { get; set; }
    }
    public class DatabaseDetailsList : List<DataBaseDetails>
    {
        public DatabaseDetailsList()
        {
            this.Add(new DataBaseDetails());
        }
    }
}
