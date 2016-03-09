/*
 * Copyright (c) 2013 BTMU
 * $Author: pthyen $
 * $Date: 2013-01-08 20:37:30 +0700 (Mon, 08 Jan 2013) $
 * $Revision: 3978 $ 
 * ========================================================
 * This class is used to provide function to create an excel file
 * for CPA module.

 */
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
//using System.Web.Mvc;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Ivs.Core.Common;

namespace Ivs.Core.Data
{
    /// <summary>
    /// This class define function processing export data to excel file
    /// </summary>
    public class CExcelBase
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        #region Properties
        /// <summary>
        /// Please add this reference to your solution		
        /// Add Microsoft.Office.Interop.Excel reference, its default path is:
        /// Visual Studio Tools for Office\ PIA\Office12\Microsoft.Office.Interop.Excel.dll 
        /// </summary>        
        private Microsoft.Office.Interop.Excel.Application app = null;

        public Microsoft.Office.Interop.Excel.Application App
        {
            get { return app; }
            set { app = value; }
        }

        //Workbooks workbooks;

        private Microsoft.Office.Interop.Excel.Workbook workbook = null;

        public Microsoft.Office.Interop.Excel.Workbook Workbook
        {
            get { return workbook; }
            set { workbook = value; }
        }

        Sheets worksheets;

        public Sheets Worksheets
        {
            get { return worksheets; }
            set { worksheets = value; }
        }

        private Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

        public Microsoft.Office.Interop.Excel.Worksheet Worksheet
        {
            get { return worksheet; }
            set { worksheet = value; }
        }

        private string excelFilePath;

        /// <summary>
        /// File is exported.
        /// </summary>
        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
        }

        private string templateFilePath;

        /// <summary>
        /// File template
        /// </summary>
        public string TemplateFilePath
        {
            get { return templateFilePath; }
            set { templateFilePath = value; }
        }

        private bool isAutoFixColumn = false;

        public bool IsAutoFixColumn
        {
            get { return isAutoFixColumn; }
            set { isAutoFixColumn = value; }
        }

        private bool isAutoFixRow = false;

        public bool IsAutoFixRow
        {
            get { return isAutoFixRow; }
            set { isAutoFixRow = value; }
        }

        public bool NoBorder { get; set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strFilePath"></param>        
        public CExcelBase(string strTemplateFileName, string strFilePath)
        {
            templateFilePath = strTemplateFileName;
            excelFilePath = strFilePath;

            try
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }

                File.Copy(strTemplateFileName, strFilePath, true);

                CreateExcel();
            }
            catch { }
            System.GC.Collect();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strFilePath"></param>        
        public CExcelBase(string strTemplateFileName, string strFilePath, int sheetNo)
        {
            templateFilePath = strTemplateFileName;
            excelFilePath = strFilePath;

            try
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }

                File.Copy(strTemplateFileName, strFilePath, true);

                CreateExcel(sheetNo);
            }
            catch { }
            System.GC.Collect();
        }


        /// <summary>
        /// Initialize File
        /// </summary>        
        private void CreateExcel(int sheetNo)
        {
            app = new Excel.Application();

            workbook = app.Workbooks.Open(ExcelFilePath, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            worksheets = workbook.Sheets;

            //get first sheet of file excel
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)worksheets.get_Item(sheetNo);
        }

        /// <summary>
        /// Initialize File
        /// </summary>        
        private void CreateExcel()
        {
            app = new Excel.Application();

            workbook = app.Workbooks.Open(ExcelFilePath, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            worksheets = workbook.Sheets;

            //get first sheet of file excel
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)worksheets.get_Item(1);


        }

        public int SaveWorksheet()
        {
            int returnCode = CommonData.IOReturnCode.Succeed;
            try
            {
                if (isAutoFixColumn)
                {
                    worksheet.Columns.AutoFit();
                }
                if (isAutoFixRow)
                {
                    worksheet.Rows.AutoFit();
                }

                worksheet.SaveAs(ExcelFilePath);
            }
            catch
            {
                returnCode = CommonData.IOReturnCode.SystemError;
            }
            finally
            {
                ReleaseObject(worksheet);
            }

            return returnCode;
        }

        /// <summary>
        /// Save the generated excel file
        /// </summary>
        /// <param name="strFilePath"></param>        
        public int SaveFile()
        {
            int returnCode = CommonData.IOReturnCode.Succeed;
            try
            {
                if (isAutoFixColumn)
                {
                    worksheet.Columns.AutoFit();
                }
                if (isAutoFixRow)
                {
                    worksheet.Rows.AutoFit();
                }

                workbook.Save();
            }
            catch
            {
                returnCode = CommonData.IOReturnCode.SystemError;
            }
            finally
            {
                ReleaseExcel();
            }

            return returnCode;
        }

        /// <summary>
        /// Release excel object
        /// </summary>
        public void ReleaseExcel()
        {
            int hWnd = app.Application.Hwnd;
            uint processID;

            ReleaseObject(worksheet);
            ReleaseObject(workbook);

            app.Quit();
            ReleaseObject(app);

            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            Process[] procs = Process.GetProcessesByName("EXCEL");
            foreach (Process p in procs)
            {
                if (p.Id == processID)
                    p.Kill();
            }
        }


        /// <summary>
        /// Release Excel Object
        /// </summary>
        /// <param name="obj"></param>        
        public void ReleaseObject(object obj)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
            GC.Collect();
        }

        /// <summary>
        /// Kill process excel
        /// </summary>
        /// <param name="application"></param>        
        public static void KillProcessExcel(Microsoft.Office.Interop.Excel.Application application)
        {
            application.Quit();

            int hWnd = application.Application.Hwnd;
            uint processID;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            Process[] procs = Process.GetProcessesByName("EXCEL");
            foreach (Process p in procs)
            {
                if (p.Id == processID)
                    p.Kill();
            }
        }

        /// <summary>
        /// Get a group of cell belongs to the index of column(start & end)  and row(start & end)
        /// EX: cellStart = "A1", cellEnd = "E100"
        /// </summary>
        /// <param name="cellStart"></param>
        /// <param name="cellEnd"></param>
        /// <returns></returns>
        public Excel.Range GetRange(int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            Range startCell = (Range)worksheet.Cells[rowStart, columnStart];
            Range endCell = (Range)worksheet.Cells[rowEnd, columnEnd];

            return worksheet.Range[startCell, endCell];
        }

        /// <summary>
        /// Get a group of cell belongs to the index of column(start & end)  and row(start & end)
        /// EX: cellStart = "A1", cellEnd = "E100"
        /// </summary>
        /// <param name="cellStart"></param>
        /// <param name="cellEnd"></param>
        /// <returns></returns>
        public Excel.Range GetRange(string cellStart, string cellEnd)
        {
            Excel.Range excRange = null;
            // get the range to fill.
            excRange = worksheet.get_Range(cellStart, cellEnd);//"A1", "E100");
            return excRange;
        }

        public void Insert(int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            Excel.Range excRange = GetRange(rowStart, columnStart, rowEnd, columnEnd);

            for (int i = rowStart; i <= rowEnd; i++)
            {
                excRange.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromRightOrBelow);
            }
        }
        public void InsertColumn( Excel.Range excRange,int columnStart, int columnEnd)
        {

            for (int i = columnStart; i <= columnEnd; i++)
            {
                excRange.EntireColumn.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
            }
        }
        public void InsertRow(Excel.Range excRange, int rowStart, int rowEnd)
        {
            for (int i = rowStart; i <= rowEnd; i++)
            {
                excRange.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromRightOrBelow);
            }
        }
        public Excel.Range ExportRange(int rowStart, int columnStart, int rowEnd, int columnEnd, object[,] data)
        {
            Excel.Range excRange = GetRange(rowStart, columnStart, rowEnd, columnEnd);

            excRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, (object)data);

            if (!NoBorder)
            {
                Excel.Borders borders = excRange.Borders;

                excRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                if (excRange.Rows.Count > 1)
                {
                    borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                    borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                }
                borders = null;
            }

            return excRange;
        }
        public Excel.Range ExportRange(string address, object[,] data)
        {
            Excel.Range excRange = this.Worksheet.get_Range(address);
            excRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, (object)data);
            if (!NoBorder)
            {
                Excel.Borders borders = excRange.Borders;

                excRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                if (excRange.Rows.Count > 1)
                {
                    borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                    borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                }
                borders = null;
            }

            return excRange;
        }

        public Excel.Range ExportCell(int rowStart, int columnStart, object data)
        {
            Excel.Range excRange = GetRange(rowStart, columnStart, rowStart, columnStart);

            if (data != null)
            {
                excRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, (object)data);
            }

            if (!NoBorder)
            {
                Excel.Borders borders = excRange.Borders;
                excRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                if (excRange.Rows.Count > 1)
                {
                    borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                    borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                }
                borders = null;
            }

            return excRange;
        }
       
        /// <summary>
        /// Export data to a range in excel
        /// EX: cellStart = "A1", cellEnd = "E100"
        /// </summary>
        /// <param name="cellStart"></param>
        /// <param name="cellEnd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Excel.Range ExportRange(string cellStart, string cellEnd, object[,] data)
        {
            Excel.Range excRange = GetRange(cellStart, cellEnd);
            
            excRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, (object)data);

            if (!NoBorder)
            {
                Excel.Borders borders = excRange.Borders;

                excRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                if (excRange.Rows.Count > 1)
                {
                    borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                    borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                }
                borders = null;
            }
            
            return excRange;
        }

        public Excel.Range CopyAndPaste(string sourceCellStart, string souceCellEnd, string destinationCellStart, string destinationCellEnd)
        {
            Excel.Range sourceRange = GetRange(sourceCellStart, souceCellEnd);
            Excel.Range destinationRange = GetRange(destinationCellStart, destinationCellEnd);

            sourceRange.Select();
            //sourceRange.Copy(sourceRange.get_Offset(-3));
            //sourceRange.SpecialCells(Excel.XlCellType.xlCellTypeConstants).Clear();
            sourceRange.Copy(Type.Missing);
            destinationRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteAll, Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

            return destinationRange;
        }

        public Excel.Range CopyAndPaste(int sourceRowStart, int sourceColumnStart, int sourceRowEnd, int sourceColumnEnd
                                , int destinationRowStart, int destinationColumnStart, int destinationRowEnd, int destinationColumnEnd)
        {
            Excel.Range sourceRange = GetRange(sourceRowStart, sourceColumnStart, sourceRowEnd, sourceColumnEnd);
            Excel.Range destinationRange = GetRange(destinationRowStart, destinationColumnStart, destinationRowEnd, destinationColumnEnd);

            sourceRange.Select();
            sourceRange.Copy(Type.Missing);
            destinationRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteAll, Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

            return destinationRange;
        }

        public Excel.Range CopyAndInsert(int sourceRowStart, int sourceColumnStart, int sourceRowEnd, int sourceColumnEnd
                               , int destinationRowStart, int destinationColumnStart, int destinationRowEnd, int destinationColumnEnd)
        {
            Excel.Range sourceRange = GetRange(sourceRowStart, sourceColumnStart, sourceRowEnd, sourceColumnEnd).EntireRow;
            Excel.Range destinationRange = GetRange(destinationRowStart, destinationColumnStart, destinationRowEnd, destinationColumnEnd).EntireRow;

            sourceRange.Select();
            sourceRange.Copy(Type.Missing);
            destinationRange.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, sourceRange.Copy(Type.Missing));

            return destinationRange;
        }

        /// <summary>
        /// Export data to a range in excel
        /// EX: cellStart = "A1", cellEnd = "E100"
        /// </summary>
        /// <param name="cellStart"></param>
        /// <param name="cellEnd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Excel.Range ExportRangeWithInsertion(string columnStart, int rowStart, string columnEnd, int rowEnd, object[,] data)
        {
            Excel.Range excTempRange = GetRange(columnStart + rowStart, columnEnd + rowStart);

            for (int i = rowStart; i < rowEnd; i++)
            {
                excTempRange.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromRightOrBelow);
            }

            Excel.Range excRange = GetRange(columnStart + rowStart, columnEnd + rowEnd);

            excRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, (object)data);

            Excel.Borders borders = excRange.Borders;

            excRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
            if (excRange.Rows.Count > 1)
            {
                borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
            }
            borders = null;
            return excRange;
        }

        /// <summary>
        /// Set object for a cell in excel file
        /// </summary>
        /// <param name="iColStart"></param>
        /// <param name="iRowStart"></param>
        /// <param name="strText"></param>
        public void InsertCellObject(int iRowStart, int iColStart, object obj)
        {
            worksheet.Cells[iRowStart, iColStart] = obj;
        }

        /// <summary>
        /// Delete rows
        /// </summary>
        /// <param name="rowStart"></param>
        /// <param name="columnStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="columnEnd"></param>
        public void DeleteRows(int rowStart, int columnStart, int rowEnd, int columnEnd)
        {
            Excel.Range excRange = GetRange(rowStart, columnStart, rowEnd, columnEnd);

            excRange.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
        }

        public void DeleteRows(string cellStart, string cellEnd)
        {
            Excel.Range excRange = GetRange(cellStart, cellEnd);

            excRange.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
        }

        /// <summary>
        /// Set Picture For Cell
        /// </summary>
        /// <param name="iColStart"></param>
        /// <param name="iRowStart"></param>
        /// <param name="imgObj"></param>        
        [STAThread]
        public void InsertPicture(int iRowStart, int iColStart, string imgPath, double imgWidth, double imgHeight)
        {
            object misValue = System.Reflection.Missing.Value;
            Excel.Range rangeObj = (Excel.Range)worksheet.Cells[iRowStart, iColStart];
            Excel.Pictures imgObjCollection = worksheet.Pictures(misValue) as Excel.Pictures;
            Excel.Picture imgObj = null;
            imgObj = imgObjCollection.Insert(imgPath, misValue);
            imgObj.Left = Convert.ToDouble(rangeObj.Left) + (Convert.ToDouble(rangeObj.Width) - imgWidth) / 2;
            imgObj.Top = Convert.ToDouble(rangeObj.Top);
            imgObj.Width = imgWidth;
            imgObj.Height = imgHeight;
        }

    }
}
