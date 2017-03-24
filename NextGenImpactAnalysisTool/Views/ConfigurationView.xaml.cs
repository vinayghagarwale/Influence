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
using System.IO;
using Microsoft.Win32;

namespace NextGenImpactAnalysisTool.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView 
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Influence\");

        private readonly string IATServerPath = @"\\INDFS1\Public\Public_files_all\ImpactAnalysisFile\Products\";
        public ConfigurationView()
        {
            InitializeComponent();

            List<string> str = BuildDirectoryList();

            string strproduct = Convert.ToString(key.GetValue("ProductName"));
            
            NgProduct.ItemsSource = str;

            if (string.IsNullOrEmpty(strproduct))
            {
                NgProduct.SelectedIndex = 0;
            }
            else
            {
                NgProduct.SelectedValue = strproduct;
            }
            UpdateServerPath();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            key.SetValue("Product", NgProduct.SelectedIndex.ToString());
            key.SetValue("ServerPath", txtServerXmlPath.Text);
            key.SetValue("ProductName", NgProduct.SelectedItem.ToString());

            this.Close();
        }

        private void UpdateServerPath()
        {
            txtServerXmlPath.Text = @"\\INDFS1\Public\Public_files_all\ImpactAnalysisFile\Products\" + NgProduct.SelectedItem.ToString();
        }
        private void NgProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServerPath();
        }

        private List<string> BuildDirectoryList()
        {
            var directories = Directory.GetDirectories(IATServerPath);
            List<string> listBox1 = new List<string>();
            foreach (string item2 in directories)
            {
                FileInfo f = new FileInfo(item2);

                listBox1.Add(f.Name);

            }
            return listBox1;
        }
    }
}
