using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml;
using NextGenImpactAnalysisTool.Engine;
using NextGenImpactAnalysisTool.ViewModels;
using NextGenImpactAnalysisTool.Model;

namespace NextGenImpactAnalysisTool.Model
{
    public class ProductDependecy
    {
        public ProductDependecy () { }
        public ProductDependecy(string productName, long dependencyCount)
        {
            this.ProductName = productName;
            this.DependencyCount = dependencyCount;
        }
        public string ProductName
        {
            get;
            set;
        }
        public long DependencyCount
        {
            get;
            set;
        }
    }
    public class ProductDependecyCollection : Collection<ProductDependecy>
    {
        public  ProductDependecyCollection()
        {
          //  Add(new ProductDependecy("Pateint Portal", 20));
            //Add(new ProductDependecy("EHR", 30));
            //Add(new ProductDependecy("Optik", 40));
            //Add(new ProductDependecy("FM", 30));
            //Add(new ProductDependecy("Rosetta", 20));
            //Add(new ProductDependecy("Mirth", 20));
             this.Add(new ProductDependecy());

            //ProcessBarChart pbarch = new ProcessBarChart();
            //ProductDependecyCollection lstproduct = new ProductDependecyCollection(xmlnode);
            //pbarch.ConstructProductList(xmlnode, lstproduct);
            //pbarch.CalculateDependencyCount(xmlnode, lstproduct, "EHR");
        }
    }
}
