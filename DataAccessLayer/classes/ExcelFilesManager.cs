using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using Serilog;

namespace DataAccessLayer
{
    public class ExcelFilesManager : IExcelFilesManager
    {
        private SpreadsheetDocument _document;
        private WorkbookPart _workbookPart;
        private readonly ILogger _logger;

        public ExcelFilesManager(ILogger logger)
        {
            _logger = logger;
        }

        public void SetSourceFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            _document = SpreadsheetDocument.Open(path, false);
            _workbookPart = _document.WorkbookPart;
        }


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
            rows.ToList();
            foreach (var row in rows)
            {
                var cellList = row.Descendants<Cell>().ToList();
                var totalColumns = cellList.Count;
                var name = totalColumns == 2 ? GetCellValue(cellList[0]) : $"{GetCellValue(cellList[0])} {GetCellValue(cellList[1])}";
                var mail = totalColumns == 2 ? GetCellValue(cellList[1]) : $"{GetCellValue(cellList[2])}";
                if (IsValidEmail(mail))
                    contacts.Add(new Contact { Name = name, Mail = mail });
                else _logger.Error($"Email is incorrect for {name} : {mail}");
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

        private bool IsValidEmail(string mail)
        {
            var pattern = @"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(mail);
        }
    }
}