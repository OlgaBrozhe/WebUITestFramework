//<author>Olga Brozhe.</author>
//<summary>Excel reader.</summary>

using System;
using System.Collections.Generic;

namespace TestWebUI.Helpers
{
    public class ExcelReader
    {
        List<List<string>> rows;
        bool containsHeader;

        public List<string> Header { get; private set; }

        public int RowCount
        {
            get { return rows.Count; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReader"/> class.
        /// </summary>
        public ExcelReader()
        {
            Header = new List<string>();
            rows = new List<List<string>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReader"/> class.
        /// </summary>
        /// <param name="filepath">Path to a spreadsheet containing the data to import.</param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        public ExcelReader(string filepath, bool containsHeader)
        {
            Header = new List<string>();
            rows = new List<List<string>>();

            // Since a filepath was supplied, call method to import data.
            ImportData(filepath, containsHeader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReader"/> class.
        /// </summary>
        /// <param name="filepath">Path to a spreadsheet containing the data to import.</param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        /// <param name="workSheetName">Number of the worksheet being imported.  (Sheet1=1, Sheet2=2, Sheet3=3...)</param>
        public ExcelReader(string filepath, bool containsHeader, string workSheetName)
        {
            Header = new List<string>();
            rows = new List<List<string>>();

            // Since a filepath was supplied, call method to import data.  Also pass the worksheet number.
            ImportData(filepath, containsHeader, workSheetName);
        }

        /// <summary>
        /// Imports data from a spreadsheet.
        /// </summary>
        /// <param name="filepath">Path to a spreadsheet containing the data to import.</param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        /// <returns>Value that indicates if the import was successful.</returns>
        public bool ImportData(string filepath, bool containsHeader)

        {
            // Simply call the version with all parameters in the signature.
            // Since no worksheet number was passed in, 1 is assumed.
            return ImportData(filepath, containsHeader, 1);
        }

        /// <summary>
        /// Imports data from a spreadsheet.
        /// </summary>
        /// <param name="filepath">Path to a spreadsheet containing the data to import.</param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        /// <param name="worksheetName">Number of the worksheet being imported.  (Sheet1=1, Sheet2=2, Sheet3=3...)</param>
        /// <returns>Value that indicates if the import was successful.</returns>
        public bool ImportData(string filepath, bool containsHeader, string worksheetName)
        {
            // Set the value of the member variable that tracks header info.
            this.containsHeader = containsHeader;

            // Now hydrate the object.
            return HydrateObjectFromSpreadsheet(filepath, containsHeader, worksheetName);
        }

        /// <summary>
        /// Imports data from a spreadsheet.
        /// </summary>
        /// <param name="filepath">Path to a spreadsheet containing the data to import.</param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        /// <param name="worksheetNum">Number of the worksheet being imported.  (Sheet1=1, Sheet2=2, Sheet3=3...)</param>
        /// <returns>Value that indicates if the import was successful.</returns>
        public bool ImportData(string filepath, bool containsHeader, int worksheetNum)
        {
            // Set the value of the member variable that tracks header info.
            this.containsHeader = containsHeader;

            // Now hydrate the object.
            return HydrateObjectFromSpreadsheet(filepath, containsHeader, worksheetNum);
        }

        /// <summary>
        /// Retrieves a value from the test data based on the supplied parameters.
        /// </summary>
        /// <param name="rowNum">Index number of the Row. Use GetRowNumber() method to find rowNum.</param>
        /// <param name="headerVal">Value of the Column header.</param>
        /// <returns>A value from the test data.</returns>
        public string GetValue(int rowNum, string headerVal)
        {
            try
            {
                // Retrieve the colNum of the headerVal.
                int colNum = RetrieveHeaderPosition(headerVal);
                if (colNum > -1)
                {
                    return rows[rowNum][colNum];
                }
                else return null;
            }
            catch (Exception ex)
            {
                // This method is responsible for returning something no matter what
                // if there is an error for any reason, return a null.
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Retrieves the row number of the test data row that matches the supplied parameters.
        /// </summary>
        /// <param name="colNum">Index number of the Column. Zero based. Use headerVal to make this easier.</param>
        /// <param name="value">Value to search for.</param>
        /// <returns>Number identifying the first row that satisfies the criteria. Returns -1 if nothing is found.</returns>
        public int GetRowNumber(int colNum, string value)
        {
            int retVal = -1;
            try
            {
                for (int pos = 0; pos < rows.Count; pos++)
                {
                    // Look at the current row, in the selected column, for the given value.
                    // If it is a match, return immediately.
                    if (rows[pos][colNum] == value)
                    {
                        retVal = pos;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Again, this method has a responsibility to return a value.
                // If there is an error, we don't really care what the error was... just
                // set a -1 to indicate that there was a problem to the calling method.
                Console.WriteLine(ex.ToString());
                retVal = -1;
            }

            // Return the retVal.
            return retVal;
        }

        /// <summary>
        /// Returns a full row of test data based on the row index. Note: Use GetRowNumber functions to find the correct row.
        /// </summary>
        /// <param name="rowNum">Index value of the row.</param>
        /// <returns>A List object that holds all of the test data for that row.</returns>
        public List<string> GetRow(int rowNum)
        {
            if ((rowNum > -1) && (rowNum <= rows.Count - 1)) return rows[rowNum];
            else return null;
        }

        /// <summary>
        /// Retrieves the position of a header within the Header object if it exists.
        /// </summary>
        /// <param name="headerValue">Value of the Column header to search for.</param>
        /// <returns>An index that refers to position within the Header object. -1 if nothing is found.</returns>
        private int RetrieveHeaderPosition(string headerValue)
        {
            int retVal = -1;
            try
            {
                // Loop through the list looking for a match.
                // Note: If there is more than one match, it will only return the first one.
                for (int pos = 0; pos < Header.Count; pos++)
                {
                    // Compare the value at this position with the value being searched for.
                    // If they are equal, return immediately.
                    if (Header[pos] == headerValue)
                    {
                        retVal = pos;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // If there is an error, just return -1 to indicate as much to the calling method.
                Console.WriteLine(ex.ToString());
                retVal = -1;
            }

            // Return the retVal variable.
            return retVal;
        }

        private bool HydrateObjectFromSpreadsheet(string filepath, bool containsHeader, string worksheetName)
        {
            // Define the necessary variables.
            bool blnSuccess = true;
            try
            {
                NPOI.XSSF.UserModel.XSSFWorkbook wb = new NPOI.XSSF.UserModel.XSSFWorkbook(filepath);
                var ws = wb.GetSheet(worksheetName);
                HydrateObjectFromSpreadsheet(ws, containsHeader);
            }
            catch (Exception ex)
            {
                // Since this will be running within a .DLL, probably just need to
                // swallow this error and return a value that indicates that loading
                // was NOT successful.
                Console.WriteLine(ex.ToString());
                blnSuccess = false;
            }

            return blnSuccess;
        }

        private bool HydrateObjectFromSpreadsheet(string filepath, bool containsHeader, int workSheetNum)
        {
            // Define the necessary variables.
            bool blnSuccess = true;
            try
            {
                NPOI.XSSF.UserModel.XSSFWorkbook wb = new NPOI.XSSF.UserModel.XSSFWorkbook(filepath);
                var ws = wb.GetSheetAt(workSheetNum);
                HydrateObjectFromSpreadsheet(ws, containsHeader);
            }
            catch (Exception ex)
            {
                // Since this will be running within a .DLL, probably just need to
                // swallow this error and return a value that indicates that loading
                // was NOT successful.
                Console.WriteLine(ex.ToString());
                blnSuccess = false;
            }

            return blnSuccess;
        }

        /// <summary>
        /// Private method that hydrates the TestData object with information from an Excel spreadsheet.
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="containsHeader">Value that indicates if the spreadsheet has a header row.</param>
        private void HydrateObjectFromSpreadsheet(NPOI.SS.UserModel.ISheet ws, bool containsHeader)
        {
            bool needToSetHeader = containsHeader;
            foreach (NPOI.XSSF.UserModel.XSSFRow row in ws)
            {
                List<string> newRow = new List<string>();
                foreach (var col in row)
                {
                    newRow.Add(col.ToString());
                }

                // Now, if Header was indicated, and this was the first row, assign.
                // Otherwise, this should get added to the rows object.
                if (needToSetHeader)
                {
                    Header = newRow;
                    needToSetHeader = false;
                }
                else rows.Add(newRow);
            }
        }

        /// <summary>
        /// Gets Test Data from the Excel worksheet.
        /// </summary>
        /// <param name="excelFolderPath"></param>
        /// <param name="workSheet"></param>
        /// <returns></returns>
        public static Dictionary<int, List<string>> GetExcelTestData(string excelFolderPath, string workSheet)
        {
            ExcelReader reader = new ExcelReader(excelFolderPath, true, workSheet);
            var testData = new Dictionary<int, List<string>>(); // Creating row count results set into Dictionary
            var rowCount = reader.RowCount; // Getting all reader Count from excel
            var counter = 0;
            for (int a = 0; a < rowCount; a++)
            {
                testData.Add(counter, reader.GetRow(a));
                counter++;
            }

            return testData;
        }
    }
}
