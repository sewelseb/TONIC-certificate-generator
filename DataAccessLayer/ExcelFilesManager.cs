using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DataAccessLayer
{
    public class ExcelFilesManager : IExcelFilesManager
    {
        private string _path;
        private SpreadsheetDocument _document;
        private WorkbookPart _wbPart;
        private DataColumn column;
        private DataRow row;

        public ExcelFilesManager(string filePath)
        {
            _path = filePath;
            _document = SpreadsheetDocument.Open(_path, false);
            _wbPart = _document.WorkbookPart;
        }

        public string GetClients()
        {
            throw new NotImplementedException();
        }

        // Test Only !!
        public string GetCellValue(string name)
        {
            // Get the 'first' sheet
            Sheet theSheet = _wbPart.Workbook.Sheets.GetFirstChild<Sheet>();
            // Retrieve a reference to the worksheet part.
            Worksheet wsPart = (_wbPart.GetPartById(theSheet.Id.Value) as WorksheetPart).Worksheet;
            //Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == name).FirstOrDefault();
            IEnumerable<Row> rows = wsPart.GetFirstChild<SheetData>().Descendants<Row>();
            foreach (Row row in rows)
            {
                var value = "";
                foreach(Cell cell in row.Descendants<Cell>())
                {
                    value = GetCellValue(_document, cell);
                }
            }
            throw new NotImplementedException();
            //return theCell.InnerText;
        }


        private string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        public string GetCellValue()
        {
            throw new NotImplementedException();
        }


    }
}