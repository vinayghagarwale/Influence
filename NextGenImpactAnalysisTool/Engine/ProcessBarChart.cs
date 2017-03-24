using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using NextGenImpactAnalysisTool.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Xml;
namespace NextGenImpactAnalysisTool.Engine
{
    public class ProcessBarChart
    {
        public void ConstructProductList(System.Xml.XmlNode node, ProductDependecyCollection lstproduct)
        {
            lstproduct.Clear();
            foreach (XmlNode xmlchildnode in node.ChildNodes)
            {
                ProductDependecy p = new ProductDependecy(xmlchildnode.Attributes[0].Value, 0);
                lstproduct.Add(p);
            }
        }

        string strCurrentProduct;
        public void CalculateDependencyCount(System.Xml.XmlNode node, ProductDependecyCollection lstproduct, string searchtext)
        {
            
            try
            {
                
                if(node.ParentNode.Name == "Nextgen")
                {
                    strCurrentProduct = node.Attributes["name"].Value;
                }
                if (node.Name == "Functional")
                {
                    string strNodevalue = node.Attributes["Product"].Value.ToUpper();
                    if (strNodevalue.Contains(searchtext.ToUpper()))
                    {
                        foreach(ProductDependecy pd in lstproduct)
                        {
                            string strprod = pd.ProductName.ToUpper();
                            if (strprod.Contains(strCurrentProduct.ToUpper()))
                            {
                                pd.DependencyCount += 1;
                                break;
                            }
                        }

                    }
                }
                else
                {
                    //if (node.Attributes.Count > 5 && node.Attributes[0].Value != null)
                    //{
                    //    string strNodevalue = node.Attributes["Module"].Value.ToUpper();
                    //    if (strNodevalue.Contains(searchtext.ToUpper()))
                    //    {
                    //        foreach (ProductDependecy pd in lstproduct)
                    //        {
                    //            if (pd.ProductName.Contains(strCurrentProduct.ToUpper()))
                    //            {
                    //                pd.DependencyCount += 1;
                    //                break;
                    //            }
                    //        }

                    //    }
                    //}
                    foreach (XmlNode xmlchildnode in node.ChildNodes)
                    {
                        CalculateDependencyCount(xmlchildnode, lstproduct, searchtext);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

    }
}
