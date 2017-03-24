using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NextGenImpactAnalysisTool.Model;
using NextGenImpactAnalysisTool.Engine;
using System.Xml;
using System.Windows.Controls.DataVisualization.Charting;
namespace NextGenImpactAnalysisTool.Views
{
    /// <summary>
    /// Interaction logic for Charts.xaml
    /// </summary>
    public partial class Charts
    {
        ProductDependecyCollection _pdep = new ProductDependecyCollection();
        public Charts()
        {
            InitializeComponent();
        }
        public Charts(ProductDependecyCollection pdep, string strdepprod)
        {
            InitializeComponent();
            //_pdep = pdep;
            populationChart.Title = strdepprod +" Product Dependency Chart" ;
            ParodDepList.Title = strdepprod;
            ParodDepList.ItemsSource = pdep;

            populationPieChart.Title = strdepprod + " Product Dependency Chart";
            ParodDepListpie.Title = strdepprod;
            ParodDepListpie.ItemsSource = pdep;
            
            //ProductDependecyCollection1 = pdep;
        }
    }
}
