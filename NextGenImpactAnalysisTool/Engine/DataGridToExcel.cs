using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using NextGenImpactAnalysisTool.Model;
using System.Diagnostics;
using System.IO;
namespace NextGenImpactAnalysisTool.Engine
{
    public class DataGridToExcel
    {
        public static void DTtoExcel(string fileName, ExportModel listExportModel)
        {
            try
            {
                var xlApp = new Excel.Application();
                var xlWorkBook = xlApp.Workbooks.Add();
                
                
                ExcelFillExportModel(xlWorkBook, listExportModel);
                if(File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                xlWorkBook.SaveAs(fileName);
                xlWorkBook.Close();
                xlApp.Quit();

                ExecuteCommandShow(fileName, "");
            }
            catch (AccessViolationException)
            {
                System.Windows.Forms.MessageBox.Show(
                     "Have encountered access violation. This could be issue with Excel 2000 if that is only version installed on computer",
                     "Access Violation");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Unknown error","Unknown error");
            }
        }

        private static void ExcelFillExportModel(Excel.Workbook xlWorkBook, ExportModel listExportModel)
        {

            int  rowOthers = 1, rowDB = 1, rowFunctional = 1;
            var sheetOtherinfo = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            sheetOtherinfo.Name = "Other Details";
            ExcelOthersDetailsTitleRow(ref rowOthers, sheetOtherinfo);

            var sheetDbinfo = (Excel.Worksheet)xlWorkBook.Worksheets.Add(((Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1)), Type.Missing, Type.Missing, Type.Missing);
            sheetDbinfo.Name = "Database Details";
            ExcelDBDetailsTitleRow(ref rowDB, sheetDbinfo);

            var sheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(((Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1)), Type.Missing, Type.Missing, Type.Missing);
            sheet.Name = "Functional Impact Analysis";
            ExcelFunDetailsTitleRow(ref rowFunctional, sheet);

            foreach (NodeModel Nodelmodel in listExportModel.lstNodeModel)
            {

                //Database Other Analysis

                int slno = 1;

                rowOthers = rowOthers + 1;
                foreach (OtherDetails Othersdetail in Nodelmodel.lstOthDetail)
                {
                    ExcelFillOthersDetailsRow(slno++, Othersdetail, rowOthers++, sheetOtherinfo, Nodelmodel.NodeName);
                }
                ((Range)sheetOtherinfo.Columns[1]).ColumnWidth = 8;
                ((Range)sheetOtherinfo.Columns[3]).ColumnWidth = 40;
                ((Range)sheetOtherinfo.Columns[4]).ColumnWidth = 50;
                ((Range)sheetOtherinfo.Columns[5]).ColumnWidth = 15;
                ((Range)sheetOtherinfo.Columns[6]).ColumnWidth = 15;
                ((Range)sheetOtherinfo.Columns[7]).ColumnWidth = 20;
                ((Range)sheetOtherinfo.Columns[8]).ColumnWidth = 20;

                //Database Impact Analysis

                slno = 1;

                rowDB = rowDB + 1;
                foreach (DataBaseDetails databasedetail in Nodelmodel.lstDbDetail)
                {
                    ExcelFillDatabaseDetailsRow(slno++, databasedetail, rowDB++, sheetDbinfo, Nodelmodel.NodeName);
                }

                ((Range)sheetDbinfo.Columns[1]).ColumnWidth = 8;
                ((Range)sheetDbinfo.Columns[3]).ColumnWidth = 40;
                ((Range)sheetDbinfo.Columns[4]).ColumnWidth = 50;
                ((Range)sheetDbinfo.Columns[5]).ColumnWidth = 15;
                ((Range)sheetDbinfo.Columns[6]).ColumnWidth = 30;
                ((Range)sheetDbinfo.Columns[7]).ColumnWidth = 15;

                //Functional Impact Analysis

                slno = 1;

                rowFunctional = rowFunctional + 1;
                foreach (FunctionalDetails fundetail in Nodelmodel.lstFuncDetail)
                {
                    ExcelFillFunctionalDetailsRow(slno++, fundetail, rowFunctional++, sheet, Nodelmodel.NodeName);
                }
                ((Range)sheet.Columns[1]).ColumnWidth = 8;
                ((Range)sheet.Columns[3]).ColumnWidth = 40;
                ((Range)sheet.Columns[4]).ColumnWidth = 50;
                ((Range)sheet.Columns[5]).ColumnWidth = 15;
                ((Range)sheet.Columns[6]).ColumnWidth = 15;
                ((Range)sheet.Columns[7]).ColumnWidth = 15;
                ((Range)sheet.Columns[8]).ColumnWidth = 15;

                Excel.Range formatRange;
                string strb = "H" + rowFunctional;
                formatRange = sheet.get_Range("B2", strb);
                formatRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

            }


        }

        private static void ExcelFillFunctionalDetailsRow(int slno, FunctionalDetails item, int row, Excel.Worksheet sheet, string nodename)
        {
            int col = 2;
            sheet.Cells[row, col] = slno;
            if(slno == 1) sheet.Cells[row, col + 1] = nodename;
            sheet.Cells[row, col + 2] = item.ImpactDescription;
            sheet.Cells[row, col+ 3] = item.ProductName;
            sheet.Cells[row, col + 4] = item.ModuleName;
            sheet.Cells[row, col+ 5] = item.Complexity ;
        }

        private static void ExcelFunDetailsTitleRow(ref int row, Excel.Worksheet sheet)
        {

            sheet.get_Range("b2", "h2").Merge(false);
            sheet.Cells[2, 2] = "Check List for Functional Impact analysis";
            sheet.Cells[2, 2].Font.Bold = true;
            sheet.get_Range("b2", "h2").HorizontalAlignment = 3;
            sheet.get_Range("b2", "h2").VerticalAlignment = 3;
            int col = 2;
            row = row + 2;
            sheet.Cells[row, col] = "Sl No";
            sheet.Cells[row, col + 1] = "Node";
            sheet.Cells[row, col + 2] = "Impact Description";
            sheet.Cells[row, col + 3] = "Product";
            sheet.Cells[row, col + 4] = "Module";
            sheet.Cells[row, col + 5] = "Complexity";
            sheet.Cells[row, col + 6] = "Checked (Y/N)";

            Excel.Range formatRange;
            formatRange = sheet.get_Range("B3", "h3");
            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

           // formatRange = sheet.get_Range("B2", "G13");
           // formatRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);


            sheet.Cells[row, col].Font.Bold = true;
            sheet.Cells[row, col+1].Font.Bold = true;
            sheet.Cells[row, col+2].Font.Bold = true;
            sheet.Cells[row, col+3].Font.Bold = true;
            sheet.Cells[row, col+4].Font.Bold = true;
            sheet.Cells[row, col+5].Font.Bold = true;
            sheet.Cells[row, col+6].Font.Bold = true;
        }

        private static void ExcelFillDatabaseDetailsRow(int slno, DataBaseDetails item, int row, Excel.Worksheet sheet, string nodename)
        {
            int col = 2;
            sheet.Cells[row, col] = slno;
            if (slno == 1) sheet.Cells[row, col + 1] = nodename;
            sheet.Cells[row, col + 2] = item.Typename;
            sheet.Cells[row, col + 3] = item.Type;
            sheet.Cells[row, col + 4] = item.Description;
        }

        private static void ExcelDBDetailsTitleRow(ref int row, Excel.Worksheet sheet)
        {
            sheet.get_Range("b2", "g2").Merge(false);
            sheet.Cells[2, 2] = "Check List for Database Impact analysis";
            sheet.Cells[2, 2].Font.Bold = true;
            sheet.get_Range("b2", "g2").HorizontalAlignment = 3;
            sheet.get_Range("b2", "g2").VerticalAlignment = 3;
            int col = 2;
            row = row + 2;
            sheet.Cells[row, col] = "Sl No";
            sheet.Cells[row, col + 1] = "Node";
            sheet.Cells[row, col + 2] = "Type Name";
            sheet.Cells[row, col + 3] = "Type";
            sheet.Cells[row, col + 4] = "Description";
            sheet.Cells[row, col + 5] = "Checked (Y/N)";
            Excel.Range formatRange;
            formatRange = sheet.get_Range("B3", "G3");
            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            formatRange = sheet.get_Range("B2", "G13");
            formatRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);


            sheet.Cells[row, col].Font.Bold = true;
            sheet.Cells[row, col + 1].Font.Bold = true;
            sheet.Cells[row, col + 2].Font.Bold = true;
            sheet.Cells[row, col + 3].Font.Bold = true;
            sheet.Cells[row, col + 4].Font.Bold = true;
            sheet.Cells[row, col + 5].Font.Bold = true;
        }

        private static void ExcelFillOthersDetailsRow(int slno, OtherDetails item, int row, Excel.Worksheet sheet, string nodename)
        {
            int col = 2;
            sheet.Cells[row, col] = slno;
            if (slno == 1) sheet.Cells[row, col + 1] = nodename;
            sheet.Cells[row, col + 2] = item.Description;
            sheet.Cells[row, col + 3] = item.Type;
            sheet.Cells[row, col + 4] = item.Module;
            sheet.Cells[row, col + 5] = item.Complexity;
        }

        private static void ExcelOthersDetailsTitleRow(ref int row, Excel.Worksheet sheet)
        {
            sheet.get_Range("b2", "h2").Merge(false);
            sheet.Cells[2, 2] = "Check List for Other Impact analysis";
            sheet.Cells[2, 2].Font.Bold = true;
            sheet.get_Range("b2", "h2").HorizontalAlignment = 3;
            sheet.get_Range("b2", "h2").VerticalAlignment = 3;
            int col = 2;
            row = row + 2;
            sheet.Cells[row, col] = "Sl No";
            sheet.Cells[row, col + 1] = "Node";
            sheet.Cells[row, col + 2] = "Description";
            sheet.Cells[row, col + 3] = "Type";
            sheet.Cells[row, col + 4] = "Module";
            sheet.Cells[row, col + 5] = "Complexity";
            sheet.Cells[row, col + 6] = "Checked (Y/N)";
            Excel.Range formatRange;
            formatRange = sheet.get_Range("B3", "h3");
            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            formatRange = sheet.get_Range("B2", "h13");
            formatRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);


            sheet.Cells[row, col].Font.Bold = true;
            sheet.Cells[row, col + 1].Font.Bold = true;
            sheet.Cells[row, col + 2].Font.Bold = true;
            sheet.Cells[row, col + 3].Font.Bold = true;
            sheet.Cells[row, col + 4].Font.Bold = true;
            sheet.Cells[row, col + 5].Font.Bold = true;
            sheet.Cells[row, col + 6].Font.Bold = true;
        }
        public static bool ExecuteCommandShow(string command, string args)
        {
            try
            {
                var installerProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Normal,
                        FileName = command,
                        Arguments = args
                    }
                };

                installerProcess.Start();

                //while (!installerProcess.HasExited)
                //{
                //    //update ui
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return true;
        }
    }
}
