using System;
using System.Windows;
using System.Xml;
using System.IO;
using System.Windows.Data;
using NextGenImpactAnalysisTool.Model;
using NextGenImpactAnalysisTool.Engine;

namespace NextGenImpactAnalysisTool.Views
{
    /// <summary>
    /// Interaction logic for InsertNode.xaml
    /// </summary>
    public partial class InsertNodeView
    {
        XmlDataProvider _xmlData;
        XmlNode _nodePosition;
        bool _isNew;
        ConfigModel _config;
        FunctionalDetailss lstFunctionalDetails = new FunctionalDetailss();
        DatabaseDetailsList lstDatabaseDetails = new DatabaseDetailsList();
        OtherDetailsList lstOtherDetails = new OtherDetailsList();
        // FileStream file   
        string file;



        public InsertNodeView(XmlDataProvider xmlData, XmlNode NodePosition, bool isNew, ConfigModel Config)
        {
            InitializeComponent();
            _xmlData = xmlData;
            _nodePosition = NodePosition;
            _isNew = isNew;
            _config = Config;
            txtNodeName.Focus();
            lstFunctionalDetails.Clear();
            lstDatabaseDetails.Clear();
            lstOtherDetails.Clear();
            file = _config.Serverfilename; // new FileStream(_config.Serverfilename, FileMode.Create, FileAccess.Write, FileShare.None);

            if (_isNew)
            {
                this.Title = "Insert Mode";
                CanOpen = true;
            }
            else
            {
                this.Title = "Modify Mode";
                txtNodeName.Text = _nodePosition.Attributes["name"].Value;
                txtNodeName.IsEnabled = false;
                if (_nodePosition.Attributes["Lock"].Value == "Yes")
                {
                    MessageBox.Show("The Selected node '" + txtNodeName.Text + @"' has been open by another user","Locked");
                    CanOpen = false;
                }
                else
                {
                    //_nodePosition.Attributes["Lock"].Value = "Yes";
                    //_nodePosition.OwnerDocument.Save(_config.Serverfilename);
                    LoadGrid();
                    CanOpen = true;
                }
            }
            Sampledatagrid.ItemsSource = lstFunctionalDetails;
            GridDatabaseDetails.ItemsSource = lstDatabaseDetails;
            GridOtherDetails.ItemsSource = lstOtherDetails;
        }

        public bool CanOpen { get; set; }
        private void LoadGrid()
        {
            try
            {
                lstFunctionalDetails.Clear();
                lstDatabaseDetails.Clear();
                lstOtherDetails.Clear();
                foreach (XmlNode xmln in _nodePosition.ChildNodes)
                {
                    if (xmln.Name == "Functional")
                    {
                        FunctionalDetails f = new FunctionalDetails();
                        f.ImpactDescription = xmln.Attributes["Impact"].Value;
                        f.ProductName = xmln.Attributes["Product"].Value;
                        f.ModuleName = xmln.Attributes["Module"].Value;
                        f.Complexity = xmln.Attributes["Complexity"].Value;
                        lstFunctionalDetails.Add(f);
                    }
                    else if (xmln.Name == "Database")
                    {
                        DataBaseDetails f = new DataBaseDetails();
                        f.Description = xmln.Attributes["Description"].Value;
                        f.Type = xmln.Attributes["Type"].Value;
                        f.Typename = xmln.Attributes["Typename"].Value;
                        lstDatabaseDetails.Add(f);
                    }
                    else if (xmln.Name == "Others")
                    {
                        OtherDetails f = new OtherDetails();
                        f.Description = xmln.Attributes["Description"].Value;
                        f.Module = xmln.Attributes["Module"].Value;
                        f.Complexity = xmln.Attributes["Complexity"].Value;
                        f.Type = xmln.Attributes["Type"].Value;
                        lstOtherDetails.Add(f);
                    }
                }
            }
            catch (Exception ex) { }

        }

        public void SaveNode()
        {
            if (txtNodeName.Text.Length <= 0)
            {
                MessageBox.Show("Enter Node Name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (lstFunctionalDetails.Count <= 0)
            {
                MessageBox.Show("Minimum one functional detail require", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_isNew)
            {
                XmlElement elmnt = _xmlData.Document.CreateElement("Node");
                elmnt.SetAttribute("name", txtNodeName.Text);
                elmnt.SetAttribute("Lock", "");
                elmnt.SetAttribute("Machinename", Environment.MachineName);
                elmnt.SetAttribute("Modifyby", Environment.UserName);
                elmnt.SetAttribute("ModifyDtTm", DateTime.Now.ToString());
                _nodePosition.AppendChild(elmnt);
                _nodePosition.OwnerDocument.Save(file);
                SaveFunctionalDetails(_nodePosition.LastChild);
                SaveDatabaseDetails(_nodePosition.LastChild);
                SaveOtherDetails(_nodePosition.LastChild);
            }
            else
            {
               _nodePosition.Attributes["name"].Value = txtNodeName.Text;
                int count = _nodePosition.ChildNodes.Count - 1;

                XmlNode todelete = _nodePosition.FirstChild;
                while (todelete != null)
                {
                    _nodePosition.RemoveChild(_nodePosition.FirstChild);
                    todelete = _nodePosition.FirstChild;
                }
                _nodePosition.OwnerDocument.Save(file);
                SaveFunctionalDetails(_nodePosition);
                SaveDatabaseDetails(_nodePosition);
                SaveOtherDetails(_nodePosition);
            }
        }

        private void SaveFunctionalDetails(XmlNode _nodeChild)
        {
            foreach (FunctionalDetails litem in lstFunctionalDetails)
            {
                    XmlElement elmnt = _xmlData.Document.CreateElement("Functional");
                    elmnt.SetAttribute("Child", "No Child");
                    elmnt.SetAttribute("Impact", litem.ImpactDescription);
                    elmnt.SetAttribute("Product", litem.ProductName);
                    elmnt.SetAttribute("Module", litem.ModuleName);
                    elmnt.SetAttribute("Complexity", litem.Complexity);
                    elmnt.SetAttribute("Machinename", Environment.MachineName);
                    elmnt.SetAttribute("Modifyby", Environment.UserName);
                    elmnt.SetAttribute("ModifyDtTm", DateTime.Now.ToString());
                    //elmnt.SetAttribute("Link", litem.strLink);
                    _nodeChild.AppendChild(elmnt);
                    _nodeChild.OwnerDocument.Save(file);
            }
        }

        private void SaveDatabaseDetails(XmlNode _nodeChild)
        {
            foreach (DataBaseDetails litem in lstDatabaseDetails)
            {
                    XmlElement elmnt = _xmlData.Document.CreateElement("Database");
                    elmnt.SetAttribute("Description", litem.Description);
                    elmnt.SetAttribute("Type", litem.Type);
                    elmnt.SetAttribute("Typename", litem.Typename);
                    elmnt.SetAttribute("Machinename", Environment.MachineName);
                    elmnt.SetAttribute("Modifyby", Environment.UserName);
                    elmnt.SetAttribute("ModifyDtTm", DateTime.Now.ToString());
                    _nodeChild.AppendChild(elmnt);
                    _nodeChild.OwnerDocument.Save(file);
            }
        }

        private void SaveOtherDetails(XmlNode _nodeChild)
        {
            foreach (OtherDetails litem in lstOtherDetails)
            {
                    XmlElement elmnt = _xmlData.Document.CreateElement("Others");
                    elmnt.SetAttribute("Description", litem.Description);
                    elmnt.SetAttribute("Type", litem.Type);
                    elmnt.SetAttribute("Module", litem.Module);
                    elmnt.SetAttribute("Complexity", litem.Complexity);
                    elmnt.SetAttribute("Machinename", Environment.MachineName);
                    elmnt.SetAttribute("Modifyby", Environment.UserName);
                    elmnt.SetAttribute("ModifyDtTm", DateTime.Now.ToString());
                    _nodeChild.AppendChild(elmnt);
                    _nodeChild.OwnerDocument.Save(file);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (!_isNew)
            //{
            //    _nodePosition.Attributes["Lock"].Value = "";
            //    _nodePosition.OwnerDocument.Save(_config.Serverfilename);
            //}
            //file.Close();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveNode();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //dgFunData.Items.Add(new FunctionalDetails());

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExcelUtility excel = new ExcelUtility();
            excel.PasteFunctionalClipboard(Sampledatagrid, lstFunctionalDetails);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ExcelUtility excel = new ExcelUtility();
            excel.PasteDatabaseClipboard(GridDatabaseDetails, lstDatabaseDetails);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ExcelUtility excel = new ExcelUtility();
            excel.PasteOtherClipboard(GridOtherDetails, lstOtherDetails);
        }
    }

}
