using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace NextGenImpactAnalysisTool.Model
{
    public class Module
    {
        public Module() { }
        public Module(string _fieldname, string _product, string _dependentmodule)
        {
            ModuleName = _fieldname;
            product = _product;
            dependentmodule = _dependentmodule;
        }
        public string ModuleName { get; set; }
        public string product { get; set; }
        public string dependentmodule { get; set; }       
        public Point coordinate { get; set; }
    }

    public class Modules : List<Module>
    {
        public string ModuleName;
        public bool interprodexist;
        public Modules()
        {
            // this.Add(new Module());
        }
    }
    public class Product
    {
        public string ProductName;
        public List<Modules> lstModules = new List<Modules>();
    }
    public class Products : List<Product>
    {
        public Products()
        {
            this.Add(new Product());
        }
    }
}
