using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceCSharp.Core
{
    public class Data
    {
        public String DataIdentifier { get; set; }
        public DataSourceType Type { get; set; }

        protected Dictionary<String, String> StringValues
        {
            get
            {
                if (StringValues == null)
                {
                    Exception e = new Exception("Please call LoadValues(); before accessing values ");
                    Logger.Error(e);
                    throw e;
                }
                return StringValues;
            }
            set
            {
                StringValues = value;
            }
        }
        protected Dictionary<String, List<String>> StringLists
        {
            get
            {
                if (StringLists == null)
                {
                    Exception e = new Exception("Please call LoadValues(); before accessing value lists ");
                    Logger.Error(e);
                    throw e;
                }
                return StringLists;
            }
            set
            {
                StringLists = value;
            }
        }

        protected Dictionary<String, Data> DataValues
        {
            get
            {
                if (DataValues == null)
                {
                    Exception e = new Exception("Please call LoadValues(); before accessing data objects ");
                    Logger.Error(e);
                    throw e;
                }
                return DataValues;
            }
            set
            {
                DataValues = value;
            }
        }

        /// <summary>
        /// Instantiates empty Data class
        /// </summary>
        public Data()
        {
            StringValues = new Dictionary<string, string>();
            StringLists = new Dictionary<string, List<string>>();
            DataValues = new Dictionary<string, Data>();
        }

        /// <summary>
        /// Loads value using the dataIdentifier
        /// </summary>
        /// <param name="dataIdentifier"></param>
        /// <param name="type"></param>
        public Data(String dataIdentifier, DataSourceType type)
        {
            StringValues = new Dictionary<string, string>();
            StringLists = new Dictionary<string, List<string>>();
            DataValues = new Dictionary<string, Data>();
            LoadValues(dataIdentifier, type);
        }

        /// <summary>
        /// Returns a single value for a particula key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Single Strng value</returns>
        public String GetValue(String key)
        {
            if (StringValues.ContainsKey(key))
            {
                Logger.Info("Returned Key: {0}; Value:{1}", key, StringValues[key]);
                return StringValues[key];
            }
            else
            {
                Exception e = new KeyNotFoundException("Key: " + key + " not found in set of values");
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Returns a list of values for a particula key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<String> GetValueList(String key)
        {
            if (StringLists.ContainsKey(key))
            {
                Logger.Info("Returned Key: {0}; Value:{1}", key, StringLists[key]);
                return StringLists[key];
            }
            else
            {
                Exception e = new KeyNotFoundException("Key: " + key + " not found in set of lists");
                Logger.Error(e);
                throw e;
            }
        }


        /// <summary>
        /// Returns a 'Data' object for a particular key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Data GetDataObject(String key)
        {
            if (DataValues.ContainsKey(key))
            {
                return DataValues[key];
            }
            else
            {
                Exception e = new KeyNotFoundException("Key: " + key + " not found in set of Data objects present");
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Returns a dictionary of all the key-value pairs stored
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> GetAllValues()
        {
            return StringValues;
        }

        /// <summary>
        /// Returns a dictionary of key-List stored
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, List<String>> GetAllValueLists()
        {
            return StringLists;
        }

        /// <summary>
        /// Returns a dictionary of key-Data objects stored
        /// </summary>
        public Dictionary<String, Data> GetAllDataObjects()
        {
            return DataValues;
        }

        /// <summary>
        /// Sets the value of a partical key (override if present already)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetValue(String key, String value)
        {
            if (StringValues.ContainsKey(key))
            {
                StringValues[key] = value;
            }
            else
            {
                StringValues.Add(key, value);
            }
            Logger.Info("Set Key: {0}; Value:{1}", key, StringValues[key]);

        }

        /// <summary>
        /// Adds the key-value pairs in the dictionary to the existing values, override if present already
        /// </summary>
        /// <param name="dictionary"></param>
        public void AddValues(Dictionary<String, String> dictionary)
        {
            foreach (String key in dictionary.Keys)
            {
                SetValue(key, dictionary[key]);
            }
        }

        /// <summary>
        /// Sets the List value of a partical key (override if present already)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetValueList(String key, List<String> ListValue)
        {
            if (StringValues.ContainsKey(key))
            {
                StringLists[key] = ListValue;
            }
            else
            {
                StringLists.Add(key, ListValue);
            }
            Logger.Info("Set Key: {0}; Value:{1}", key, StringLists[key]);

        }

        /// <summary>
        /// Adds the key-List value pairs in the dictionary to the existing values, override if present already
        /// </summary>
        /// <param name="dictionary"></param>
        public void AddValueLists(Dictionary<String, List< String>> Listdictionary)
        {
            foreach (String key in Listdictionary.Keys)
            {
                SetValueList(key, Listdictionary[key]);
            }
        }
        
        /// <summary>
        /// Sets the data object of a partical key (override if present already)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetDataObject(String key, Data data)
        {
            if (DataValues.ContainsKey(key))
            {
                DataValues[key] = data;
            }
            else
            {
                DataValues.Add(key, data);
            }

        }

        /// <summary>
        /// When implemented this method should load the values from the DataIdentifier (Override the exising values)
        /// </summary>
        public virtual void LoadValues()
        {
            if (DataIdentifier == null)
            {
                Exception e = new Exception("Set DataIdentifier before loading values");
                Logger.Error(e);
                throw e;
            }
            if (Type == DataSourceType.None)
            {
                Exception e = new Exception("DataSource type is 'None', can't load when none");
                Logger.Error(e);
                throw e;
            }
            if (Type == DataSourceType.ExcelFile)
            {
                
            }
            if (Type == DataSourceType.ConfigFile)
            {
            }


        }

        /// <summary>
        /// When implemented this method should load the values using the dataIdentifier
        /// Should update the DataIdentifier and Type
        /// </summary>
        /// <param name="dataIdentifier"></param>
        /// <param name="type"></param>
        public void LoadValues(String dataIdentifier, DataSourceType type)
        {
            this.DataIdentifier = dataIdentifier;
            this.Type = type;
            LoadValues();
        }


    }

    public enum DataSourceType
    {
        ExcelFile,
        ConfigFile,
        None

    }
}
