using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class SearchList
    {
        public SearchList() { }

        public SearchList(bool _Select, string _Searchitem, System.Xml.XmlNode _xmlNode)
        {
            Select = _Select;
            NodeName = _Searchitem;
            TreeViewPath = _xmlNode;
        }

        public bool Select
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }
        public System.Xml.XmlNode TreeViewPath
        {
            get;
            set;
        }
    }
    public class SearchLists : List<SearchList>
    {
        public SearchLists()
        {
            this.Add(new SearchList());
        }
    }
}

