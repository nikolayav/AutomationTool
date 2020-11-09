using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationTool {
    public class OpenXMLEditor {

        public void UpdateExcelSheetData(string filePath, string sheetName, string newValue, string templateString) {
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(filePath, true)) {

                AddUpdateCellValue(spreadSheet, sheetName, newValue, templateString);
                spreadSheet.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                spreadSheet.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;
            }
        }

        public void AddUpdateCellValue(SpreadsheetDocument spreadSheet, string sheetname, string text, string templateString) {
            // Opening document for editing            
            WorksheetPart worksheetPart = RetrieveSheetPartByName(spreadSheet, sheetname);
            if (worksheetPart != null) {
                string[] cellIndex = GetIndexBySearch(templateString).Split(',');
                string col = Convert.ToChar((Convert.ToInt32(cellIndex[1]) + 64)).ToString();
                uint row = Convert.ToUInt32(cellIndex[0]);
                Cell cell = InsertCellInSheet(col, row, worksheetPart);
                cell.CellValue = new CellValue(text);
                //cell datatype            
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                // Save the worksheet.            
                worksheetPart.Worksheet.Save();
            }
        }

        public WorkbookPart ImportExcel(string filePath) {
            try {
                string path = filePath;

                using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    MemoryStream m_ms = new MemoryStream();
                    fs.CopyTo(m_ms);

                    SpreadsheetDocument m_Doc = SpreadsheetDocument.Open(m_ms, false);

                    return m_Doc.WorkbookPart;
                }
            } catch (Exception ex) {
                System.Diagnostics.Trace.TraceError(ex.Message + ex.StackTrace);
            }
            return null;
        }
        public string GetIndexBySearch(string search) {

            WorkbookPart workbookPart = ImportExcel("templates\\template_excel.xlsx");
            var sheets = workbookPart.Workbook.Descendants<Sheet>();
            Sheet sheet = sheets.Where(x => x.Name.Value == "Universal").FirstOrDefault();

            string index = string.Empty;

            if (sheet != null) {
                var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                var rows = worksheetPart.Worksheet.Descendants<Row>().ToList();

                // Remove the header row
                rows.RemoveAt(0);

                foreach (var row in rows) {
                    var cellss = row.Elements<Cell>().ToList();
                    
                    foreach (var cell in cellss) {
                        if (!String.IsNullOrEmpty(cell.InnerText) && int.TryParse(cell.InnerText, out int n)) {
                            var value = cell.InnerText;
                            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                            bool isFound = value.Trim().ToLower().Contains(search.Trim().ToLower());

                            if (isFound) {
                                index = $"{row.RowIndex},{GetColumnIndex(cell.CellReference)}";
                                return index;
                            }
                        }

                    }
                }
            }
            if (String.IsNullOrEmpty(index)) {
                throw new System.InvalidOperationException(String.Format("Value '{0}' is not present in the Excel file.", search));
            }
            return index;
        }

        private static int? GetColumnIndex(string cellReference) {
            if (string.IsNullOrEmpty(cellReference)) {
                return null;
            }

            string columnReference = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);

            int columnNumber = -1;
            int mulitplier = 1;

            foreach (char c in columnReference.ToCharArray().Reverse()) {
                columnNumber += mulitplier * ((int)c - 64);

                mulitplier = mulitplier * 26;
            }

            return columnNumber + 1;
        }

        //retrieve sheetpart            
        public WorksheetPart RetrieveSheetPartByName(SpreadsheetDocument document,
         string sheetName) {
            IEnumerable<Sheet> sheets =
             document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
            Elements<Sheet>().Where(s => s.Name == sheetName);
            if (sheets.Count() == 0)
                return null;

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
            document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }

        //insert cell in sheet based on column and row index            
        public Cell InsertCellInSheet(string columnName, uint rowIndex, WorksheetPart worksheetPart) {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;
            Row row;
            //check whether row exist or not            
            //if row exist            
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            //if row does not exist then it will create new row            
            else {
                row = new Row() {
                    RowIndex = rowIndex
                };
                sheetData.Append(row);
            }
            //check whether cell exist or not            
            //if cell exist            
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            //if cell does not exist            
            else {
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>()) {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0) {
                        refCell = cell;
                        break;
                    }
                }
                Cell newCell = new Cell() {
                    CellReference = cellReference
                };
                row.InsertBefore(newCell, refCell);
                worksheet.Save();
                return newCell;
            }
        }

        // retrieve cell based on column and row index            
        public Cell RetreiveCell(Worksheet worksheet,
         string columnName, uint rowIndex) {
            Row row = RetrieveRow(worksheet, rowIndex);
            var newRow = new Row() {
                RowIndex = (uint)rowIndex + 1
            };
            //adding new row            
            worksheet.InsertAt(newRow, Convert.ToInt32(rowIndex + 1));
            //create cell with value            
            Cell cell = new Cell();
            cell.CellValue = new CellValue("");
            cell.DataType =
             new EnumValue<CellValues>(CellValues.String);
            newRow.AddAnnotation(cell);
            worksheet.Save();

            row = newRow;
            if (row == null)
                return null;
            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName +
         (rowIndex + 1), true) == 0).First();
        }

        // it will return a row based on worksheet and rowindex            
        public Row RetrieveRow(Worksheet worksheet, uint rowIndex) {
            return worksheet.GetFirstChild<SheetData>().
            Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }

    }
}
