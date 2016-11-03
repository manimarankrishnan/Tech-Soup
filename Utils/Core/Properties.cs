using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Core
{
    public class Properties : Data
    {
        /// <summary>
        /// Instantiates empty Data class
        /// </summary>
        public Properties():base()
        {
           
        }

        /// <summary>
        /// Loads value using the dataIdentifier
        /// </summary>
        /// <param name="dataIdentifier">excel Data identifier</param>
        /// <param name="type"></param>
        public Properties(String dataIdentifier):base(dataIdentifier)
        {
        }

        /// <summary>
        /// Loads the values from the DataIdentifier (Override the exising values)
        /// </summary>
        public override void LoadValues()
        {
            if (DataIdentifier == null)
            {
                Exception e = new Exception("Set DataIdentifier before loading values");
                Logger.Error(e);
                throw e;
            }

            initialiseDictionaries();
            String textValue = GeneralUtils.GetFileAsString(DataIdentifier);

            var lines = textValue.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var row = line.Trim();
                StringValues.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }


        }
    }
}
