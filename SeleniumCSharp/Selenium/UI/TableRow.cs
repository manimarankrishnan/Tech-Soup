using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Utils.Core;

namespace SeleniumCSharp.Selenium.UI
{

    public class TableRow : Component
    {


        /// <summary>
        /// Table Data Element default: By.TagName("td")
        /// </summary>
        public virtual By ByTableData { get { return By.TagName("td"); } }
        
        /// <summary>
        /// A function to get the column list 
        /// Will be set by the containing table while its initialisation
        /// </summary>
        public Func<ReadOnlyCollection<string>> GetColumnlistFunc { get; set; }

        private List<string> _valueList;

        /// <summary>
        /// List of Text from the list of Table data elements (td) present in the row
        /// </summary>
        public List<string> ValueList
        {
            get
            {
                if(_valueList == null){
                List<string> valueList = new List<string>();
                foreach (var element in DataList)
                {
                    valueList.Add(element.Text.Trim());
                }
                _valueList= valueList;
                }
                return _valueList;
            }
        }

        /// <summary>
        /// List of Columns in the containing Table
        /// </summary>
        public virtual ReadOnlyCollection<string> ColumnList { get { return GetColumnlistFunc.Invoke(); } }

        /// <summary>
        /// List of td elements
        /// </summary>
        private ReadOnlyCollection<IWebElement> _dataList;

        /// <summary>
        /// Collection of Table Data elements
        /// </summary>
        public ReadOnlyCollection<IWebElement> DataList
        {
            get
            {
                _dataList = _dataList ?? WrappedElement.FindElements(ByTableData);
                return _dataList;
            }
            set { _dataList = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TableRow()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Table Row element</param>
        public TableRow(IWebElement element):base(element)
        {

        }

        /// <summary>
        /// Empty method
        /// </summary>
        public override void InitElements()
        {
        }

        /// <summary>
        /// get the text inside a data element
        /// </summary>
        /// <param name="dataIndex">index of the element</param>
        /// <returns>Text inside the td element</returns>
        public String GetText(int dataIndex)
        {
            if (!DataList.Any())
            {
                throw new Exception(String.Format("There are no data elements, but required data index is {0}", dataIndex));
            }

            if (DataList.Count <= dataIndex)
            {
                throw new Exception(String.Format("There are only {0} data elements, but required data index is {1}", DataList.Count, dataIndex));
            }
            return DataList[dataIndex].Text;
        }

        /// <summary>
        /// Get the index of the table element with particular text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int GetIndex(String text)
        {
            if (!DataList.Any())
            {
                throw new Exception(String.Format("There are no data elements."));
            }
            return ValueList.IndexOf(text.Trim());
        }

        /// <summary>
        /// Get the Table data element corresponding to particular column name in containing table
        /// </summary>
        /// <param name="ColumnName">Column name</param>
        /// <returns> Table Data element WebElementWrapper</returns>
        public WebElementWrapper GetTableDataElement(String ColumnName)
        {
            return this[ColumnName];
        }

        /// <summary>
        /// Get the Table data element in the particular index
        /// </summary>
        /// <param name="index">0 based index of the table data element</param>
        /// <returns></returns>
        public WebElementWrapper GetTableDataElement(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Get the Table data element in the particular index
        /// </summary>
        /// <param name="index">0 based index of the table data element</param>
        /// <returns></returns>
        public WebElementWrapper this[int index]
        {
            get
            {

                if (ColumnList.Count > index)
                {
                    return new WebElementWrapper(DataList[index]);
                }
                else
                {
                    var e = new Exception("The Column index" + index + " is greater than the available no. of columns" + ColumnList.Count);
                    Logger.Error(e);
                    throw e;

                }

            }
        }
       
        /// <summary>
        /// Get the Table data element corresponding to particular column name in containing table
        /// </summary>
        /// <param name="ColumnName">Column name</param>
        /// <returns> Table Data element WebElementWrapper</returns>
        public WebElementWrapper this[String ColumnName]
        {
            get
            {

                if (ColumnList.Contains(ColumnName))
                {
                    return new WebElementWrapper(DataList[ColumnList.IndexOf(ColumnName)]);
                }
                else
                {
                    var e = new KeyNotFoundException("The Column " + ColumnName + " is not found in the list of columns" + ColumnList.ToString());
                    Logger.Error(e);
                    throw e;

                }

            }

        }




    }


}
