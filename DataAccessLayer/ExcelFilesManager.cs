using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;

namespace DataAccessLayer
{
    public class ExcelFilesManager : IExcelFilesManager
    {
        private readonly List<Contact> _contacts;
        private SpreadsheetDocument _document;
        private WorkbookPart _wbPart;

        public ExcelFilesManager()
        {
            _contacts = new List<Contact>();
        }

        public bool SetSourceFile(string path)
        {
            if (File.Exists(path))
            {
                _document = SpreadsheetDocument.Open(path, false);
                _wbPart = _document.WorkbookPart;
                return true;
            }

            return false;
        }


        // Only for 3 column excel file. Have to modify GetContact method to separate the string separation logic 
        public List<Contact> GetContacts()
        {
            if (_document == null) throw new FileNotFoundException();
            // Get the 'first' sheet
            var theSheet = _wbPart.Workbook.Sheets.GetFirstChild<Sheet>();
            // Retrieve a reference to the worksheet part.
            var wsPart = (_wbPart.GetPartById(theSheet.Id.Value) as WorksheetPart).Worksheet;
            //Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == name).FirstOrDefault();
            var rows = wsPart.GetFirstChild<SheetData>().Descendants<Row>();
            foreach (var row in rows)
            {
                var _names = "";
                var _mail = "";
                var count = 0;
                if (row.RowIndex != 1)
                {
                    foreach (var cell in row.Descendants<Cell>())
                    {
                        if (count != 2)
                            _names = count == 0
                                ? _names = GetCellValue(cell)
                                : _names = $"{_names} {GetCellValue(cell)}";
                        else _mail = GetCellValue(cell);
                        count++;
                    }

                    _contacts.Add(new Contact {mail = _mail, names = _names});
                }
            }

            return _contacts;
        }

        // Reference : https://gist.github.com/kzelda/2facdff2d924349fe96c37eab0e9ee47
        private string GetCellValue(Cell cell)
        {
            var value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                return _wbPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value))
                    .InnerText;
            return value;
        }
    }
}