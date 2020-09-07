using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;

namespace DataAccessLayer
{
    public class ExcelFilesManager : IExcelFilesManager
    {
        private SpreadsheetDocument _document;
        private WorkbookPart _workbookPart;

        public void SetSourceFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            _document = SpreadsheetDocument.Open(path, false);
            _workbookPart = _document.WorkbookPart;
        }


        // Only for 3 column excel file. Have to modify GetContact method to separate the string separation logic 
        public List<Contact> GetContacts()
        {
            if (_document == null) throw new FileNotFoundException();

            var contacts = new List<Contact>();
            var sheet = GetSheetFromWorkbook();
            var worksheet = GetWorksheetFromSheet(sheet);

            var rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
            rows = rows.Where(row => row.RowIndex != 1);

            ExtractData(rows, contacts);

            return contacts;
        }

        private Sheet GetSheetFromWorkbook()
        {
            return _workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
        }

        private Worksheet GetWorksheetFromSheet(Sheet theSheet)
        {
            return (_workbookPart.GetPartById(theSheet.Id.Value) as WorksheetPart).Worksheet;
        }

        private void ExtractData(IEnumerable<Row> rows, List<Contact> contacts)
        {
            foreach (var row in rows)
            {
                var cellList = row.Descendants<Cell>().ToList();
                var name = $"{GetCellValue(cellList[0])} {GetCellValue(cellList[1])}";
                var mail = $"{GetCellValue(cellList[2])}";
                contacts.Add(new Contact {Name = name, Mail = mail});
            }
        }

        // Reference : https://gist.github.com/kzelda/2facdff2d924349fe96c37eab0e9ee47
        private string GetCellValue(Cell cell)
        {
            var value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                return _workbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value))
                    .InnerText;
            return value;
        }
    }
}