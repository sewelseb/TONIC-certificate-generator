using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Models;

namespace DataAccessLayer
{
    public class ExcelFilesManager : IExcelFilesManager
    {
        private string _path;
        private SpreadsheetDocument _document;
        private WorkbookPart _wbPart;
        private List<Contact> _contacts;

        public ExcelFilesManager(string filePath)
        {
            _path = filePath;
            _document = SpreadsheetDocument.Open(_path, false);
            _wbPart = _document.WorkbookPart;
            _contacts = new List<Contact>();
        }

        // Only for 3 column excel file. Have to modify GetContact method to separate the string separation logic 
        public List<Contact> GetContacts()
        {
            // Get the 'first' sheet
            Sheet theSheet = _wbPart.Workbook.Sheets.GetFirstChild<Sheet>();
            // Retrieve a reference to the worksheet part.
            Worksheet wsPart = (_wbPart.GetPartById(theSheet.Id.Value) as WorksheetPart).Worksheet;
            //Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == name).FirstOrDefault();
            IEnumerable<Row> rows = wsPart.GetFirstChild<SheetData>().Descendants<Row>();
            foreach (Row row in rows)
            {
                var _names = "";
                string _mail = "";
                int count = 0;
                if (row.RowIndex != 1)
                {
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        if (count != 2) _names = (count == 0 ? _names = GetCellValue(cell) : _names = $"{_names} {GetCellValue(cell)}");
                        else _mail = GetCellValue(cell);
                        count++;
                    }
                    _contacts.Add(new Contact { mail = _mail, names = _names});
                }
            }
            return _contacts;
        }

        // Reference : https://gist.github.com/kzelda/2facdff2d924349fe96c37eab0e9ee47
        private string GetCellValue(Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return _wbPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }


    }
}