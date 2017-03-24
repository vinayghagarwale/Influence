using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class NodeModel
    {
        public string NodeName;
        public List<FunctionalDetails> lstFuncDetail = new List<FunctionalDetails>();
        public List<DataBaseDetails> lstDbDetail = new List<DataBaseDetails>();
        public List<OtherDetails> lstOthDetail = new List<OtherDetails>();
    }
    public class ExportModel
    {
        public List<NodeModel> lstNodeModel = new List<NodeModel>();
    }
}
