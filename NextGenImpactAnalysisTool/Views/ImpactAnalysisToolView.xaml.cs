using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using NextGenImpactAnalysisTool.Engine;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Xml;
using NextGenImpactAnalysisTool.ViewModels;
using NextGenImpactAnalysisTool.Model;
using System.Deployment.Application;
using System.Xml.Linq;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Microsoft.Win32;
namespace NextGenImpactAnalysisTool.Views
{

    /// <summary>
    /// Interaction logic for ImpactAnalysisToolView.xaml
    /// </summary>
    ///  
    public partial class ImpactAnalysisToolView 
    {
        string _treepath;
        ConfigModel _config;
        ProcessFiles process;
        List<SearchList> lstExportListitems;

        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Influence\");

        public ImpactAnalysisToolView(ConfigModel Config)
        {
            InitializeComponent();
            _config = Config;
            ChangeTitle();
            process = new ProcessFiles(Config);
            lstExportListitems = new List<SearchList>();
            LoadXmldatareader();
            GetLatestXMLFile();
            CheckLatestXMLVersion();
            //txtVersion.Text = "Version : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                txtVersion.Text = string.Format("Version : v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            }
            txtSearch.Focus();
        }

        #region Private Methods

        private void LoadXmldatareader()
        {
            var xmldata1 = this.FindResource("xmldata") as XmlDataProvider;
            xmldata1.Source = new System.Uri(_config.LocalFilename);
            xmldata1.XPath = "/Nextgen";
            grd.DataContext = xmldata1;
        }

        public void DeleteNode()
        {
            try
            {
                if (process.FileVersioncheckmessage == "Server not Connected")
                {
                    MessageBox.Show("Cannot Delete node, because server is not connected", "Warning");
                    return;
                }


                XmlNode selectedItem = (XmlNode)dirTree.SelectedItem;
                if (selectedItem.Attributes["Lock"].Value == "Yes")
                {
                    MessageBox.Show("The Selected node is locked", "Locked");
                }
                else
                {
                    MessageBoxResult Result = MessageBox.Show("Do you want to delete Node - " + selectedItem.Attributes[0].Value + " ? ", "Delete", MessageBoxButton.YesNo);
                    if (Result == MessageBoxResult.Yes)
                    {
                        selectedItem.ParentNode.RemoveChild(selectedItem);
                        selectedItem.OwnerDocument.Save(_config.Serverfilename);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void GetLatestXMLFile()
        {
            if (process.CheckLatestVersion())
            {
                MessageBoxResult Result = MessageBoxResult.No;// MessageBox.Show("Latest XML available in server, do you want to update?", "Latest Files", MessageBoxButton.YesNo);
                if (Result == MessageBoxResult.Yes)
                {
                    process.UpdateLocalXMLFile();
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                CheckLatestXMLVersion();
                LoadXmldatareader();
            }
        }

        private void CheckLatestXMLVersion()
        {
            if (process.CheckLatestVersion())
            {
                txtStatus.Text = process.FileVersioncheckmessage;
                txtStatusColor.Background = System.Windows.Media.Brushes.Red;
                btnDownload.Visibility = Visibility.Visible;
            }
            else
            {
                txtStatus.Text = process.FileVersioncheckmessage;
                txtStatusColor.Background = System.Windows.Media.Brushes.Green;
                btnDownload.Visibility = Visibility.Hidden ;
            }
        }


        private void searchResults()
        {
            if (txtSearch.Text.Length > 0)
            {
                XmlNode xmlnode = (XmlNode)dirTree.Items[0];
                List<SearchList> lstitems = new List<SearchList>();
                SearchItemsintreeview(xmlnode, lstitems, txtSearch.Text);
                lstsearchresult.ItemsSource = lstitems;
                TabResult.Focus();
            }
        }

        private void SearchItemsintreeview(System.Xml.XmlNode node, List<SearchList> lstitems, string searchtext)
        {

            try
            {

                if (node.ChildNodes.Count == 0)
                {
                    string strNodevalue = node.Attributes[0].Value.ToUpper();
                    if (strNodevalue.Contains(searchtext.ToUpper()))
                        lstitems.Add(new SearchList(false, node.Attributes[0].Value, node));
                   
                }
                else
                {
                    if (node.Attributes.Count > 0)
                    {
                        string strNodevalue = node.Attributes[0].Value.ToUpper();
                        if (strNodevalue.Contains(searchtext.ToUpper()))
                            lstitems.Add(new SearchList(false, node.Attributes[0].Value, node));
                    }
                    foreach (XmlNode xmlchildnode in node.ChildNodes)
                    {
                        SearchItemsintreeview(xmlchildnode, lstitems, searchtext);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

        private void ConstructNodeTree(System.Xml.XmlNode node)
        {
            try
            {

                _treepath = "";

                if (node.ParentNode.Name == "#document")
                {
                    _treepath = @"  >>  " + node.Name;
                }
                else
                {
                    ConstructNodeTree(((System.Xml.XmlElement)node).ParentNode);

                    _treepath = _treepath + @"  >>  " + node.Attributes[0].Value;
                }
            }
            catch (Exception e)
            {

            }
        }

        private async void OpenAddNodeUI()
        {
            try
            {
                if (process.FileVersioncheckmessage == "Server not Connected")
                {
                    MessageBox.Show("Cannot add node, because server is not connected", "Warning");
                    return;
                }
                XmlNode selectedItem = (XmlNode)dirTree.SelectedItem;
                if (selectedItem.Name == "Nextgen")
                {
                    MessageBox.Show("need admin rights to add Products", "Warning");
                    return;
                }
                if (selectedItem.Attributes["name"].Value == "")
                {
                    return;
                }
                if (IsLocked())
                {
                    return;
                }
                MetroWindow metroWindow = Application.Current.Windows[0] as MetroWindow;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme

                await metroWindow.ShowOverlayAsync();
                

                //process.UpdateLocalXMLFile();
                LoadXmldatareader();

                if (LockXMLFile())
                {
                    InsertNodeView sdd = new InsertNodeView((XmlDataProvider)this.FindResource("xmldata"), selectedItem, true, _config) { DataContext = new InsertNodeViewModel() };

                    if (sdd.CanOpen)
                    {
                        sdd.Width = this.Width;
                        sdd.Height = 400;
                        sdd.ShowDialog();
                        UnLockXMLFile();
                    }
                }
                await metroWindow.HideOverlayAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source.ToString());
            }
        }

        private async void OpenModifyNodeUI()
        {
            try
            {
                XmlNode selectedItem = (XmlNode)dirTree.SelectedItem;

                if (process.FileVersioncheckmessage == "Server not Connected")
                {
                    MessageBox.Show("Cannot Modify node, because server is not connected");
                    return;
                }

                if (IsLocked())
                {
                    return;
                }
                if (selectedItem.Name == "Nextgen")
                {
                    MessageBox.Show("Cannot Modify selected node");
                    return;
                }
                if (selectedItem.ParentNode.Name == "Nextgen")
                {
                    MessageBox.Show("Cannot Modify selected node");
                    return;
                }
                if (selectedItem.ParentNode.ParentNode.Name == "Nextgen")
                {
                    MessageBox.Show("Cannot Modify selected node");
                    return;
                }
                if (selectedItem.Attributes.Count > 0 && selectedItem.Attributes["name"].Value == "")
                {
                    return;
                }
                MetroWindow metroWindow = Application.Current.Windows[0] as MetroWindow;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme

                await metroWindow.ShowOverlayAsync();

                //process.UpdateLocalXMLFile();

                LoadXmldatareader();

                if (LockXMLFile())
                {
                    InsertNodeView sdd = new InsertNodeView((XmlDataProvider)this.FindResource("xmldata"), selectedItem, false, _config);
                    if (sdd.CanOpen)
                    {
                        sdd.Width = this.Width;
                        sdd.Height = 400;
                        sdd.ShowDialog();
                        UnLockXMLFile();
                    }
                }
                await metroWindow.HideOverlayAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private async void OpenChartUI(ProductDependecyCollection lstproduct, string strdepprod)
        {
            try
            {
                //if (process.FileVersioncheckmessage == "Server not Connected")
                //{
                //    MessageBox.Show("Cannot add node, because server is not connected", "Warning");
                //    return;
                //}
                MetroWindow metroWindow = Application.Current.Windows[0] as MetroWindow;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme

                await metroWindow.ShowOverlayAsync();
                XmlNode selectedItem = (XmlNode)dirTree.SelectedItem;

                //process.UpdateLocalXMLFile();
                //LoadXmldatareader();

                Charts sdd = new Charts(lstproduct, strdepprod);

                sdd.Width = this.Width;
                sdd.Height = 600;
                sdd.ShowDialog();

                await metroWindow.HideOverlayAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source.ToString());
            }
        }
        private async void OpenDependencyDiagUI()
        {
            try
            {
                //if (process.FileVersioncheckmessage == "Server not Connected")
                //{
                //    MessageBox.Show("Cannot add node, because server is not connected", "Warning");
                //    return;
                //}
                if(dirTree.SelectedItem == null)
                {
                    return;
                }
                MetroWindow metroWindow = Application.Current.Windows[0] as MetroWindow;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme

                await metroWindow.ShowOverlayAsync();
                XmlNode selectedItem = (XmlNode)dirTree.SelectedItem;

                //process.UpdateLocalXMLFile();
                //LoadXmldatareader();
                Products pp = new Products();
                DependencyDiagram sdd = new DependencyDiagram(selectedItem, pp);

                sdd.Width = this.Width;
                sdd.Height = 700;
                sdd.ShowDialog();

                await metroWindow.HideOverlayAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source.ToString());
            }
        }
        #endregion

        #region Event methods
        private void XmlDataProvider_DataChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                XmlDataProvider oProv = this.DataContext as XmlDataProvider;
                oProv.Refresh();
            }));
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchResults();
            }
         }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteNode();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenModifyNodeUI();
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            GetLatestXMLFile();
           // Modifying();
        }

        private async void Tile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MetroWindow metroWindow = Application.Current.Windows[0] as MetroWindow;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented; // set the theme

                await metroWindow.ShowOverlayAsync();

                ConfigurationView sdd = new ConfigurationView();
                sdd.Width = this.Width;
                sdd.Height = 400;
                sdd.ShowDialog();
                await metroWindow.HideOverlayAsync();
                //DownloadFromServer();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void dirTree_MouseUp(object sender, MouseButtonEventArgs e)
        {

            dgridEmp.DataContext = dirTree.SelectedItem;
            dgridDatabase.DataContext = dirTree.SelectedItem;
            dgridOthers.DataContext = dirTree.SelectedItem;

            ConstructTreeViewSeletedNode((System.Xml.XmlElement)dirTree.SelectedItem);
            if (dirTree.Items.Count > 0)
            {
               // dirTree.Items[0] = true;
            }
        }

        private void ConstructTreeViewSeletedNode(System.Xml.XmlElement SelectedItem )
        {
            ConstructNodeTree(SelectedItem);
            txtTreeViewPath.Text = _treepath;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            OpenAddNodeUI();

        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            searchResults();
        }

        private void lstsearchresult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lstsearchresult.SelectedItem != null)
            {
                dgridEmp.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridDatabase.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridOthers.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstsearchresult.SelectedItem).TreeViewPath);
            }
        }
        private void lstExportList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lstExportList.SelectedItem != null)
            {
                dgridEmp.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                dgridDatabase.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                dgridOthers.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstExportList.SelectedItem).TreeViewPath);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                txtSearch.Text = "";
                lstsearchresult.ItemsSource = null;
                TabList.Focus();
                dgridEmp.DataContext = null;
                dgridDatabase.DataContext = null;
                dgridOthers.DataContext = null;
                txtSearch.Focus();
            }
        }

        #endregion

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ExportFromTreeView();
        }
        private void ExportFromTreeView()
        {
            ExportModel exportModel = new ExportModel();
            List<NodeModel> lstnodes = new List<NodeModel>();
            BuildExportModel(lstnodes);
            exportModel.lstNodeModel = lstnodes;
            ExporttoExcel(exportModel);
        }

        private void BuildExportModel(List<NodeModel> lstnodes)
        {
            
            //Create Nodes list
            NodeModel nodmodel = new NodeModel();

            nodmodel.NodeName = txtTreeViewPath.Text;
            //Build Functional Model
            foreach (XmlNode item in dgridEmp.Items)
            {
                FunctionalDetails funModel = new FunctionalDetails();
                funModel.ImpactDescription = item.Attributes["Impact"].Value;
                funModel.ProductName = item.Attributes["Product"].Value;
                funModel.ModuleName = item.Attributes["Module"].Value;
                funModel.Complexity = item.Attributes["Complexity"].Value;
                nodmodel.lstFuncDetail.Add(funModel);
            }
            //Build Database Model
            foreach (XmlNode item in dgridDatabase.Items)
            {
                DataBaseDetails DbModel = new DataBaseDetails();
                DbModel.Typename = item.Attributes[0].Value;
                DbModel.Type = item.Attributes[1].Value;
                DbModel.Description = item.Attributes[2].Value;
                nodmodel.lstDbDetail.Add(DbModel);
            }
            //Build Others Model
            foreach (XmlNode item in dgridOthers.Items)
            {
                OtherDetails OthModel = new OtherDetails();
                OthModel.Description = item.Attributes[0].Value;
                OthModel.Type = item.Attributes[1].Value;
                OthModel.Module = item.Attributes[2].Value;
                OthModel.Complexity = item.Attributes[3].Value;
                                
                nodmodel.lstOthDetail.Add(OthModel);
            }
            
            lstnodes.Add(nodmodel);

        }

        private void ExporttoExcel(ExportModel _exportModel)
        {
            DataGridToExcel.DTtoExcel(@"C:\ImpactAnalysisFile\Excel.xlsx", _exportModel);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (lstsearchresult.SelectedItem != null)
            {
                ExportModel exportModel = new ExportModel();
                List<NodeModel> lstnodes = new List<NodeModel>();
                dgridEmp.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridDatabase.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridOthers.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstsearchresult.SelectedItem).TreeViewPath);
                BuildExportModel(lstnodes);
                exportModel.lstNodeModel = lstnodes;
                ExporttoExcel(exportModel);
            }
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            ExportModel exportModel = new ExportModel();
            List<NodeModel> lstnodes = new List<NodeModel>();
            if (TabList.IsSelected)
            {
                ExportFromTreeView();
            }
            else if (TabExport.IsSelected)
            {
                MultipleExport();
            }
            else if (TabResult.IsSelected)
            {
                dgridEmp.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridDatabase.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                dgridOthers.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstsearchresult.SelectedItem).TreeViewPath);
                BuildExportModel(lstnodes);
                exportModel.lstNodeModel = lstnodes;
                ExporttoExcel(exportModel);
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (lstsearchresult.SelectedItem != null)
            {
                lstExportListitems.Add(new SearchList(true, ((SearchList)lstsearchresult.SelectedItem).NodeName, ((SearchList)lstsearchresult.SelectedItem).TreeViewPath));
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            if (dirTree.SelectedItem != null)
            {
                lstExportListitems.Add(new SearchList(true, ((XmlNode)dirTree.SelectedItem).Attributes[0].Value, ((XmlNode)dirTree.SelectedItem)));
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            MultipleExport();
        }

        private void MultipleExport()
        {
            ExportModel exportModel = new ExportModel();
            List<NodeModel> lstnodes = new List<NodeModel>();
            foreach (SearchList explist in lstExportList.Items)
            {
                if (explist.Select)
                {
                    dgridEmp.DataContext = explist.TreeViewPath;
                    dgridDatabase.DataContext = explist.TreeViewPath;
                    dgridOthers.DataContext = explist.TreeViewPath;
                    ConstructTreeViewSeletedNode((XmlElement)explist.TreeViewPath);
                    BuildExportModel(lstnodes);
                }
            }

            exportModel.lstNodeModel = lstnodes;
            ExporttoExcel(exportModel);
        }


        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            DownloadFromServer();
        }
        private void DownloadFromServer()
        {
            if (process.CheckLatestVersion())
            {
                process.UpdateLocalXMLFile();
                CheckLatestXMLVersion();
                LoadXmldatareader();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            //((XmlElement)dirTree.SelectedNode).IsExpanded = true;

            System.Windows.Controls.TreeView tv = new System.Windows.Controls.TreeView();  //= (System.Windows.Controls.TreeView)dirTree;
            

        }

        private void dirTree_KeyDown(object sender, KeyEventArgs e)
        {

            dgridEmp.DataContext = dirTree.SelectedItem;
            dgridDatabase.DataContext = dirTree.SelectedItem;
            dgridOthers.DataContext = dirTree.SelectedItem;

            ConstructTreeViewSeletedNode((System.Xml.XmlElement)dirTree.SelectedItem);
        }

        private void lstExportList_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void lstsearchresult_MouseEnter(object sender, MouseEventArgs e)
        {
            lstExportList.ItemsSource = null;
        }

        private void lstsearchresult_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.A))
            {
                lstExportListitems.Add(new SearchList(true, ((SearchList)lstsearchresult.SelectedItem).NodeName, ((SearchList)lstsearchresult.SelectedItem).TreeViewPath));
            }
            else
            {
                if (lstsearchresult.SelectedItem != null)
                {
                    dgridEmp.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                    dgridDatabase.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                    dgridOthers.DataContext = ((SearchList)lstsearchresult.SelectedItem).TreeViewPath;
                    ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstsearchresult.SelectedItem).TreeViewPath);
                }
            }

        }

        private void TabExport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lstExportList.ItemsSource = lstExportListitems;
            lstExportList.Focus();
        }

        private void TabList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lstExportList.ItemsSource = null;
        }

        private void lstExportList_KeyUp(object sender, KeyEventArgs e)
        {
            if (lstExportList.SelectedItem != null)
            {
                dgridEmp.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                dgridDatabase.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                dgridOthers.DataContext = ((SearchList)lstExportList.SelectedItem).TreeViewPath;
                ConstructTreeViewSeletedNode((XmlElement)((SearchList)lstExportList.SelectedItem).TreeViewPath);
            }
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            XmlNode xmlnode = (XmlNode)dirTree.Items[0];
            if(dirTree.SelectedItem == null)
            {
                return;
            }
            if (((System.Xml.XmlElement)dirTree.SelectedItem).ParentNode.Name != "Nextgen")
            {
                MessageBox.Show("Select Product from treeview list");
                return;
            }
            string strdepprod = ((System.Xml.XmlElement)dirTree.SelectedItem).Attributes["name"].Value; //"Patient Portal";
            ProcessBarChart pbarch = new ProcessBarChart();
            ProductDependecyCollection lstproduct = new ProductDependecyCollection();
            pbarch.ConstructProductList(xmlnode, lstproduct);
            pbarch.CalculateDependencyCount(xmlnode, lstproduct, strdepprod);
            OpenChartUI(lstproduct, strdepprod);
        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
           // XmlNode xmlnode = (XmlNode)dirTree.Items[0];
           //// string strdepprod = ((System.Xml.XmlElement)dirTree.SelectedItem).Attributes["name"].Value; //"Patient Portal";
           // ProcessBarChart pbarch = new ProcessBarChart();
           // ProductDependecyCollection lstproduct = new ProductDependecyCollection();
           // pbarch.ConstructProductList(xmlnode, lstproduct);
           // pbarch.CalculateDependencyCount(xmlnode, lstproduct, strdepprod);
            OpenDependencyDiagUI();
        }
        private bool IsLocked()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_config.Serverfilename);
            XmlNode node = xmlDoc.LastChild;

            if (node.Attributes["Lock"].Value == "Yes")
            {
                MessageBox.Show("XML file is locked by " + node.Attributes["Lockedby"].Value + " for changes since " + node.Attributes["Lockedat"].Value,"Information",MessageBoxButton.YesNo,MessageBoxImage.Information);
                return true;
            }
            return false;
         }
        private bool LockXMLFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_config.Serverfilename);
            XmlNode node = xmlDoc.LastChild;

            if (node.Attributes["Lock"].Value == "No")
            {
                node.Attributes["Lock"].Value = "Yes";
                node.Attributes["Lockedby"].Value = Environment.UserName;
                node.Attributes["LockedbyMachine"].Value = Environment.MachineName;
                node.Attributes["Lockedat"].Value = DateTime.Now.ToString();
                xmlDoc.Save(_config.Serverfilename);
                return true;
            }
            return false;
         }
        private bool UnLockXMLFile()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_config.Serverfilename);
            XmlNode node = xmlDoc.LastChild;

            if (node.Attributes["Lock"].Value == "Yes")
            {
                node.Attributes["Lock"].Value = "No";
                node.Attributes["Lockedby"].Value = "";
                node.Attributes["LockedbyMachine"].Value = "";
                node.Attributes["Lockedat"].Value = "";
                xmlDoc.Save(_config.Serverfilename);
                return true;
            }
            return false;
        }

        private void Modifying()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_config.Serverfilename);
            XmlNode node = xmlDoc.LastChild;

            if (node.Attributes["Lock"].Value == "Yes")
            {
                 txtCurrent.Text = "XML file is Changing by " + node.Attributes["Lockedby"].Value + " ..... ";
            }
        }

        private void ChangeTitle()
        {
            if(String.IsNullOrEmpty(_config.ProductName))
                this.Title = "Influence";
            else
                this.Title = "Influence - " + _config.ProductName;
        }
    }
}
