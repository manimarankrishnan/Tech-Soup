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
       
        protected Dictionary<String, String> StringValues;
        protected Dictionary<String, List<String>> StringLists;
        protected Dictionary<String, Data> DataValues;
      

        /// <summary>
        /// Instantiates empty Data class
        /// </summary>
        public Data()
        {
            initialiseDictionaries();
        }

        /// <summary>
        /// Loads value using the dataIdentifier
        /// </summary>
        /// <param name="dataIdentifier">excel Data identifier</param>
        /// <param name="type"></param>
        public Data(String dataIdentifier)
        {
            initialiseDictionaries();
            LoadValues(dataIdentifier);
        }

        private void initialiseDictionaries()
        {
            StringValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            StringLists = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            DataValues = new Dictionary<string, Data>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns a single value for a particular key
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
        /// Returns a list of values for a particular key
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
        /// Adds the values given in the list to the List item in the dictionary
        /// </summary>
        /// <param name="Listkey">key for the list</param>
        /// <param name="ValuesToAddToList">List of values to be added to the list</param>
        public void AddValuesToList(String Listkey, List<String> ValuesToAddToList)
        {
            if (StringValues.ContainsKey(Listkey))
            {
                StringLists[Listkey].AddRange(ValuesToAddToList);
            }
            else
            {
                StringLists.Add(Listkey, ValuesToAddToList);
            }
            Logger.Info("Set Key: {0}; Value:{1}", Listkey, StringLists[Listkey]);

        }


        /// <summary>
        /// Adds the value given in the list to the List item in the dictionary
        /// </summary>
        /// <param name="Listkey">key for the list</param>
        /// <param name="valueToAddToList">Value to be added to the list in the dictionary</param>
        public void AddValuesToList(String Listkey, String valueToAddToList)
        {
            if (StringValues.ContainsKey(Listkey))
            {
                StringLists[Listkey].Add(valueToAddToList);
            }
            else
            {
                StringLists.Add(Listkey, new List<String>() { valueToAddToList });
            }
            Logger.Info("Set Key: {0}; Value:{1}", Listkey, StringLists[Listkey]);

        }

        /// <summary>
        /// Sets the List value of a partical key (override if present already)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void SetValueList(String Listkey, List<String> ListValue)
        {
            if (StringValues.ContainsKey(Listkey))
            {
                StringLists[Listkey] = ListValue;
            }
            else
            {
                StringLists.Add(Listkey, ListValue);
            }
            Logger.Info("Set Key: {0}; Value:{1}", Listkey, StringLists[Listkey]);

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
        /// Returns true if the key is present in the list of key-value pairs
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsValueKeyPresent(String key)
        {
            return StringValues.ContainsKey(key);
        }

        /// <summary>
        /// Returns true if the key is present in the dictionary of List objects
        /// </summary>
        /// <param name="listKey"></param>
        /// <returns></returns>
        public bool IsListValueKeyPresent(String listKey)
        {
            return StringLists.ContainsKey(listKey);
        }

        /// <summary>
        /// Returns true if the key is present in the dictionary of data objects
        /// </summary>
        /// <param name="dataKey">key</param>
        /// <returns></returns>
        public bool IsDataKeyPresent(String dataKey)
        {
            return DataValues.ContainsKey(dataKey) ;
        }


        /// <summary>
        /// Returns true if the key is present in the list of key-value pairs  and populates value with value of the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryGetValue(String key,out String value)
        {
            return StringValues.TryGetValue(key,out value);
        }

        /// <summary>
        /// Returns true if the key is present in the dictionary of List objects and populates valueList with value of the key
        /// </summary>
        /// <param name="listKey"></param>
        /// <returns></returns>
        public bool TryGetListValue(String listKey, out List<String> valueList)
        {
            return StringLists.TryGetValue(listKey,out valueList);
        }

        /// <summary>
        /// Returns true if the key is present in the dictionary of data objects  and populates value with value of the key
        /// </summary>
        /// <param name="dataKey">key</param>
        /// <returns></returns>
        public bool TryGetData(String dataKey,out Data value)
        {
            return DataValues.TryGetValue(dataKey, out value);
        }
        /// <summary>
        /// Loads the values from the DataIdentifier (Override the exising values)
        /// </summary>
        public void LoadValues()
        {
            if (DataIdentifier == null)
            {
                Exception e = new Exception("Set DataIdentifier before loading values");
                Logger.Error(e);
                throw e;
            }

            String[] identifierParts = DataIdentifier.Split('_');
            String[] HeaderValues = Utils.GetDataFromExcel(identifierParts[0] + "_" + identifierParts[1]).First();
            String[] DataValues = Utils.GetDataFromExcel(DataIdentifier).First();
            initialiseDictionaries();
            int index = 0;
            foreach (String val in DataValues)
            {
                SetValue(HeaderValues[index], val);
                index++;
            }

        }

        /// <summary>
        /// When implemented this method should load the values using the dataIdentifier
        /// Should update the DataIdentifier and Type
        /// </summary>
        /// <param name="dataIdentifier"></param>
        public void LoadValues(String dataIdentifier)
        {
            this.DataIdentifier = dataIdentifier;
            LoadValues();
        }


    }

}
