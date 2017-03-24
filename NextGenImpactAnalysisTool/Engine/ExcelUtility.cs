using System;
using System.Windows.Controls;
using System.Windows.Forms;
using NextGenImpactAnalysisTool.Model;
namespace NextGenImpactAnalysisTool.Engine
{
    public class ExcelUtility
    {
        /// <summary>
        /// Method used to Paste excel clip board
        /// </summary>
        /// <param name="dgData"></param>
        /// <param name="lst"></param>
        public void PasteFunctionalClipboard(System.Windows.Controls.DataGrid dgData, FunctionalDetailss lst)
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                foreach (string line in lines)
                {
                    //MessageBox.Show(line);
                    string[] sCells = line.Split('\t');
                    
                    if (sCells[0].ToString().Length > 0)
                    {
                        FunctionalDetails f = new FunctionalDetails();
                        if (sCells.Length >= 1) f.ImpactDescription = sCells[0];
                        if (sCells.Length >= 2) f.ProductName = sCells[1];
                        if (sCells.Length >= 3) f.ModuleName = sCells[2];
                        if (sCells.Length >= 4) f.Complexity = sCells[3];
                        lst.Add(f);
                    }
                  }
                dgData.ItemsSource = null;
                dgData.ItemsSource = lst;
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
        public void PasteDatabaseClipboard(System.Windows.Controls.DataGrid dgData, DatabaseDetailsList lst)
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                foreach (string line in lines)
                {
                    //MessageBox.Show(line);
                    string[] sCells = line.Split('\t');

                    if (sCells[0].ToString().Length > 0)
                    {
                        DataBaseDetails f = new DataBaseDetails();
                        if (sCells.Length >= 1) f.Typename = sCells[0];
                        if (sCells.Length >= 2) f.Type = sCells[1];                       
                        if (sCells.Length >= 3) f.Description = sCells[2];
                        
                        lst.Add(f);
                    }
                }
                dgData.ItemsSource = null;
                dgData.ItemsSource = lst;
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
        public void PasteOtherClipboard(System.Windows.Controls.DataGrid dgData, OtherDetailsList lst)
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                foreach (string line in lines)
                {
                    //MessageBox.Show(line);
                    string[] sCells = line.Split('\t');

                    if (sCells[0].ToString().Length > 0)
                    {
                        OtherDetails f = new OtherDetails();
                        if (sCells.Length >= 1) f.Description = sCells[0];
                        if (sCells.Length >= 2) f.Type = sCells[1];
                        if (sCells.Length >= 3) f.Module = sCells[2];
                        if (sCells.Length >= 4) f.Complexity = sCells[3];

                        lst.Add(f);
                    }
                }
                dgData.ItemsSource = null;
                dgData.ItemsSource = lst;
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
    }
}
