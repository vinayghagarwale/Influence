
using System;
using NextGenImpactAnalysisTool.Model;
using NextGenImpactAnalysisTool.Views;
using NextGenImpactAnalysisTool.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Microsoft.Win32;
namespace NextGenImpactAnalysisTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Influence\");

            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 2)
            {
                MessageBox.Show("Multiple instances are not allowed");
                this.Close();
                return;
            }

            string _localfilename = @"C:\ImpactAnalysisFile\NextGenImpact.XML";
            //string _serverfilename = @"C:\ImpactAnalysisFile1\NextGenImpact.XML";
            //string _serverfilename = @"\\indfs1\Public\Public_files_all\ImpactAnalysisFile\NextGenImpact.XML";
            string _serverfilename = Convert.ToString(key.GetValue("ServerPath"));

            _serverfilename = _serverfilename + @"\NextGenImpact.XML";
            ConfigModel Config = new ConfigModel() { LocalFilename = _localfilename, Serverfilename = _serverfilename };

            Config.ProductName = Convert.ToString(key.GetValue("ProductName"));
            //Launch First UI
            var win = new ImpactAnalysisToolView(Config) { DataContext = new ImpactAnalysisToolViewModel() };
            win.Show();
            this.Close();
        }
    }
}
