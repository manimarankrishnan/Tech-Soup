using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Xml;
namespace WebServiceCSharp.Core
{
    public class Utils
    {

        static Dictionary<String, List<String[]>> ExcelData = new Dictionary<string, List<string[]>>();
    
        /// <summary>
        /// Get the data from Cached Excel Data
        /// If not already cached the data will be cached for further use
        /// </summary>
        /// <param name="excelFilePath">Full excel file path</param>
        /// <param name="sheetName">Worksheet name</param>
        /// <returns></returns>
        public static List<String[]> GetDataFromExcelData(String excelFilePath, String sheetName)
        {
            if (ExcelData.Keys.Contains(excelFilePath + sheetName))
            {
                return ExcelData[excelFilePath + sheetName];
            }
            else
            {
                try
                {
                    var data = GetDataFromExcel(excelFilePath, sheetName);
                    ExcelData.Add(excelFilePath + sheetName, data);
                    return ExcelData[excelFilePath + sheetName];
                }
                catch (Exception e)
                {
                    Logger.Error("{0}\n{1}\n{2}", e.Message, e.InnerException, e.StackTrace);
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the data from excel workbook sheet
        /// </summary>
        /// <param name="excelFilePath">Full excel file path</param>
        /// <param name="sheetName">Worksheet name</param>
        /// <param name="noOfColumns">no of columns needed from the sheet</param>
        /// <returns>List of rows of data</returns>
        public static List<String[]> GetDataFromExcel(String excelFilePath, String sheetName, int noOfColumns = -1)
        {
            Application excelApp;
            _Workbook workBook;
            _Worksheet workSheet;
            List<String[]> requiredData;

            excelFilePath = getFullFilepath(excelFilePath);
            Logger.Info("Accessing file :" + excelFilePath);
            //Creating Excel Object
            excelApp = new Application();
            excelApp.Visible = false;

            //Opening the workbook
            workBook = excelApp.Workbooks.Open(excelFilePath);
            Logger.Info("Opened the excel file " + excelFilePath);
            try
            {
                //Selecting the sheet
                try
                {
                    workSheet = workBook.Sheets[sheetName];
                    Logger.Info(sheetName + " found");
                }
                catch (Exception e)
                {
                    Logger.Error("Sheet: {0} is not found in {1} \n {2}", sheetName,excelFilePath, e);
                    throw new Exception("Sheet: " + sheetName + " is not found", e);
                }
                
                //Getting the used Range
                Range range = workSheet.UsedRange;
                Logger.Info("{0}Rows \n {1}Columns", range.Rows.Count, range.Columns.Count);

                if (noOfColumns <= 0)
                    noOfColumns = range.Columns.Count;

                //Checking whether the number of columns not greater than the available columns
                if (noOfColumns > range.Columns.Count)
                {
                    Exception e =new Exception(String.Format("The number of columns requested: {0} is greater than the available no of Columns: {1}", noOfColumns, range.Columns.Count));
                    Logger.Error(e);
                    throw e;
                }

                //Adding the Data
                requiredData = new List<string[]>();
                for (int row = 1; row <= range.Rows.Count; row++)
                {
                    String[] rowValues = new String[noOfColumns];
                    for (int column = 0; column < noOfColumns; column++)
                    {
                        try
                        {
                            rowValues[column] = (String)range.Cells[row, column + 1].Text;
                        }
                        catch (Exception) { }

                    }
                    requiredData.Add(rowValues);
                }

                //releasing the worksheet
                Marshal.ReleaseComObject(workSheet);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }

            finally
            {
                //closing workbook
                workBook.Close();
                Logger.Info("Closed workbook {0}", excelFilePath);
                excelApp.Quit();
                //releasing the objects
                Marshal.ReleaseComObject(excelApp);
                Marshal.ReleaseComObject(workBook);

            }
            return requiredData;
        }

        /// <summary>
        /// Gets the Data from excel with the given Id in the first column
        /// </summary>
        /// <param name="Identifier">Full Identifier of data Format: WorkbookName_SheetName_Id</param>
        /// <param name="noOfColumns">No Of columns of data need excluding the Id coloumn</param>
        /// <returns>List of rows of required data</returns>
        public static List<String[]> GetDataFromExcel(String Identifier, int noOfColumns = -1)
        {
            String[] identifierParts = Identifier.Split(new Char[] { '_' });

            String fileName = identifierParts[0];
            String sheetName = identifierParts[1];

            //Identifier in the first column of sheet
            String ID = Identifier.Replace(String.Format("{0}_{1}", fileName, sheetName), "");
            ID = ID.StartsWith("_") ? ID.Substring(1) : ID;
            //Finding the full file path
            String excelFilePath = Path.Combine(Config.ResourcesBaseDirectory, fileName + ".xlsx");

            //Getting the data from all the rows in the excel sheet
            List<String[]> rawDataFromExcel = GetDataFromExcelData(excelFilePath, sheetName);

            if (String.IsNullOrEmpty(ID))
            {
                rawDataFromExcel.RemoveAt(0);
            }

            List<String[]> requiredData = new List<string[]>();
            foreach (String[] rowData in rawDataFromExcel)
            {
                //When Unique ID is empty returning all the data
                if (String.IsNullOrEmpty(ID)
                    || ID.ToUpper().Equals(rowData[0].ToUpper()))
                {
                    //Removing the Id column data
                    List<String> requiredColumnList = rowData.ToList();
                    int startingIndex = String.IsNullOrEmpty(ID) ? 0 : 1;
                    requiredColumnList = requiredColumnList.GetRange(startingIndex, noOfColumns > -1 ? noOfColumns : requiredColumnList.Count - startingIndex);
                    //Adding the result to the required Data
                    requiredData.Add(requiredColumnList.ToArray());
                }
            }

            //Throwing exception if no rows are found
            if (requiredData.Count == 0)
            { 
                Exception e =new Exception("No data found for the Identifier:" + Identifier);
                Logger.Error(e);
                throw e;
            }

            return requiredData;
        }

        /// <summary>
        /// Get a file as a string
        /// </summary>
        /// <param name="fileIdentifier">Relative path of the file from resource directory</param>
        /// <returns></returns>
        public static string GetFileAsString(String fileIdentifier)
        {
            String data;
            String filePath = getFullFilepath(fileIdentifier);
            try
            {
                StreamReader reader = new StreamReader(filePath);
                data = reader.ReadToEnd();
                reader.Close();
                return data;
            }
            catch ( Exception e){
                Logger.Error(e);
                throw e;
            }

        }

        /// <summary>
        /// Get a file as a XMLDocument
        /// </summary>
        /// <param name="fileIdentifier">Relative path of the file from resource directory</param>
        /// <returns>XMLDocument</returns>
        public static XmlDocument GetFileAsXMLDocument(String fileIdentifier)
        {
            String data;
            String filePath = getFullFilepath(fileIdentifier);
            try
            {
                StreamReader reader = new StreamReader(filePath);
                data = reader.ReadToEnd();
                reader.Close();
                XmlDocument result = new XmlDocument();
                result.LoadXml(data.Trim());
                Logger.Info("File {0} : {1} {2}", fileIdentifier, data, result);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }

        }

        /// <summary>
        /// De serialize an XML file to specific type
        /// </summary>
        /// <param name="fileName">Relative path of the file from resources directory</param>
        /// <param name="type">The type of object</param>
        /// <returns></returns>
        public static object DeserializeXMLFile(String fileName, Type type)
        {
            String filePath = getFullFilepath(fileName);
            object result;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var serializer = new XmlSerializer(type);
                    result = serializer.Deserialize(reader);
                }
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }
        /// <summary>
        /// De-serialize json file into an object
        /// </summary>
        /// <param name="fileName">Relative path of the file from resources directory</param>
        /// <param name="type">The type of object</param>
        /// <returns>Object</returns>
        public static object DeserializeJSONFile(String fileName, Type type)
        {
            String filePath = getFullFilepath(fileName);
            object result;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    result = JsonConvert.DeserializeObject(reader.ReadToEnd(), type);
                }
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
         
        }
        /// <summary>
        /// De-serialize Json string into an object
        /// </summary>
        /// <param name="jsonString">jSON string</param>
        /// <param name="type">The type of object to de-serialise as</param>
        /// <returns></returns>
        public static object DeserializeJSON(String jsonString, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonString, type);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
           
        }

        /// <summary>
        /// Deserialize XML stream into an object
        /// </summary>
        /// <param name="stream">XML stream</param>
        /// <param name="type">type of object</param>
        /// <returns></returns>
        public static object DeserializeXML(Stream stream, Type type)
        {
            try
            {
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(stream);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// De-serialize XML string
        /// </summary>
        /// <param name="xmlString">xml string</param>
        /// <param name="type">type of object</param>
        /// <returns></returns>
        public static object DeserializeXML(String xmlString, Type type)
        {
            try
            {
                using (StringReader reader = new StringReader(xmlString))
                {
                    var serializer = new XmlSerializer(type);
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Serialise an object
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static string XMLSerializeObject(Object obj)
        {
            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                var stream = new StringWriter();
                serializer.Serialize(stream, obj);

                return stream.ToString();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
           
        }

        /// <summary>
        /// Serialize an object into JSON
        /// </summary>
        /// <param name="obj">object to be serialized</param>
        /// <returns></returns>
        public static string JSONSerializeObject(Object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Get the full path of a file 
        /// </summary>
        /// <param name="fileIdentifier">Relative file location from the resource directory</param>
        /// <returns></returns>
        private static string getFullFilepath(String fileIdentifier)
        {
            if (File.Exists(fileIdentifier))
                return fileIdentifier;
            Logger.Debug(fileIdentifier + " file is not found. Checking in Resource Directory");
            String filePath = Path.Combine(Config.ResourcesBaseDirectory, fileIdentifier);
            if (!File.Exists(filePath))
            {
                FileNotFoundException e = new FileNotFoundException("File " + filePath + "doesn't exist");
                Logger.Error(e);
                throw e;
            }
            return filePath;
        }

        public static String FormatJsonString(String text)
        {
            try
            {
                return JsonConvert.DeserializeObject(text).ToString(); 
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }
    }
}
