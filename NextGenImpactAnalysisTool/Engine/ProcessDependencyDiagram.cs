using System;
using System.Windows.Data;
using System.Windows.Input;
using NextGenImpactAnalysisTool.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Xml;

namespace NextGenImpactAnalysisTool.Engine
{
    public class ProcessDependencyDiagram
    {
        public void ConstructProductList(System.Xml.XmlNode node, Products _product)
        {
            
            foreach (XmlNode xmlchildnode in node.ChildNodes)
            {
                Product p = new Product();
                _product.Add(p);

                LoadModulesDetails(xmlchildnode, p);
            }
        }

        private void LoadModulesDetails (System.Xml.XmlNode node, Product _product)
        {
            foreach (XmlNode xmlchildnode in node.ChildNodes)
            {
                Modules modules = new Modules();

                modules.ModuleName = xmlchildnode.Attributes["name"].Value;

                LoadModuleDetails(xmlchildnode, modules);

                _product.ProductName = node.Attributes["name"].Value;
                if(modules.interprodexist)
                    _product.lstModules.Add(modules);
            }
        }
        private void LoadModuleDetails(System.Xml.XmlNode node, Modules _modules)
        {
            if (!node.HasChildNodes)
            {
                if (node.Name == "Functional")
                {
                    if (node.Attributes["Module"].Value.Length > 0)
                    {
                        _modules.interprodexist = true;
                    }
                    Module module = new Module(node.Attributes["Impact"].Value, node.Attributes["Product"].Value, node.Attributes["Module"].Value);
                    _modules.Add(module);
                }
            }
            else
            {
                if (node.Name == "Functional")
                {
                    Module module = new Module(node.Attributes["Impact"].Value, node.Attributes["Product"].Value, node.Attributes["Module"].Value);
                    _modules.Add(module);
                }


                foreach (XmlNode xmlchildnode in node.ChildNodes)
                {
                    LoadModuleDetails(xmlchildnode, _modules);
                }
            }
        }
    }
}
