using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebServiceCSharp.Core
{
    public class Config
    {
        
        private static Dictionary<String, String> _env;
        private static Dictionary<String,String> env
        {
            get
            {
                if(_env==null)
                {
                    _env = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    var envVar = Environment.GetEnvironmentVariables();
                    foreach (var s in envVar.Keys)
                    {
                        _env.Add(s.ToString(), envVar[s].ToString());
                    }
                }
                return _env;
            }
        }
        private static Dictionary<String, String> Values;
        public static String ResourcesBaseDirectory
        {
            get
            {
                String resourcePath = IsConfigValuePresent("ResourceFilesLocation") ? GetConfigValue("ResourceFilesLocation") : "Resources";
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcePath);
            }
            private set
            {

            }
        }

        /// <summary>
        /// Get config value
        /// </summary>
        /// <param name="configName">key for the config</param>
        /// <returns>value for the config</returns>
        public static String GetConfigValue(String configName)
        {
            if (Values == null)
                ReloadConfigFile();

            if (Values.ContainsKey(configName))
                return Values[configName];
            else
            {
                String environmentValue;
                 env.TryGetValue(configName, out environmentValue);
                if (!String.IsNullOrEmpty(environmentValue))
                {
                    return environmentValue;
                }
            }

            throw new Exception("No valuue for the key " + configName + " found from config file or Environment");
        }


        /// <summary>
        /// Set the value for a config
        /// </summary>
        /// <param name="configName">key for the config</param>
        /// <param name="value">value for the config</param>
        public static void SetConfigValue(String configName, String value)
        {
            if (IsConfigValuePresent(configName))
                Values[configName] = value;
            else
                Values.Add(configName, value);

        }

        /// <summary>
        /// Check whether a config is present
        /// </summary>
        /// <param name="configName">key for the config</param>
        /// <returns>true if the config is present, false if not present</returns>
        public static bool IsConfigValuePresent(String configName)
        {
            if (Values == null)
                ReloadConfigFile();
            return Values.ContainsKey(configName) || env.ContainsKey(configName);
        }


        /// <summary>
        /// Load values from a config value
        /// </summary>
        /// <param name="configFile">Path of the config file</param>
        public static void LoadValuesFromConfigFile(String configFile)
        {
            if (!File.Exists(configFile))
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);
            if (!File.Exists(configFile))
            {
                Logger.Debug("No file found : {0}", configFile);
                return;
            }
            StreamReader reader = new StreamReader(configFile);
            Logger.Info("Reading config file" + configFile);
            configuration configDetails = (configuration)Utils.DeserializeXML(reader.ReadToEnd(), typeof(configuration));
            foreach (var parameter in configDetails.parameter)
            {
                String value;
                 env.TryGetValue(parameter.name,out value);
                value = String.IsNullOrEmpty(value) ? parameter.value : value;
                if (parameter.name.Equals("ConfigFile"))
                    LoadValuesFromConfigFile(value);
                Values.Add(parameter.name, value);
                Logger.Info("Added config '{0}' : '{1}'", parameter.name, value);
            }
        }

        /// <summary>
        /// Reload values from test.config file
        /// </summary>
        public static void ReloadConfigFile()
        {
            if (Values != null)
                Logger.Info("Reloading values from config file");
            if (Values == null)
                Logger.Info("Loading values from config file");
            Values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                LoadValuesFromConfigFile("test.config");
            }
            catch (Exception e)
            {
                Logger.Error("test.config file is not present in the project" + e.Message);
            }
        }


        #region Configuration Fille XML classes

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class configuration
        {

            private configurationParameter[] parameterField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("parameter")]
            public configurationParameter[] parameter
            {
                get
                {
                    return this.parameterField;
                }
                set
                {
                    this.parameterField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class configurationParameter
        {

            private string nameField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }
        #endregion

    }


}




