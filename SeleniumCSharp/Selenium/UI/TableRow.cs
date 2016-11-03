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
        public virtual By ByTableData { get { return By.TagName("td"); } }

        public Func<ReadOnlyCollection<string>> GetColumnlistFunc { get; set; }

        public List<string> ValueList
        {
            get
            {
                List<string> valueList = new List<string>();
                foreach (var element in DataList)
                {
                    valueList.Add(element.Text);
                }
                return valueList;
            }
        }

        public virtual ReadOnlyCollection<string> ColumnList { get { return GetColumnlistFunc.Invoke(); } }

        private ReadOnlyCollection<IWebElement> _dataList;

        public ReadOnlyCollection<IWebElement> DataList
        {
            get
            {
                _dataList = _dataList ?? WrappedElement.FindElements(ByTableData);
                return _dataList;
            }
            set { _dataList = value; }
        }

        public TableRow()
        {

        }

        public TableRow(IWebElement element)
        {
            WrappedElement = element;
        }

        public override void InitElements()
        {

        }

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

        public int GetIndex(String text)
        {
            if (!DataList.Any())
            {
                throw new Exception(String.Format("There are no data elements."));
            }
            foreach (var element in DataList)
            {
                if (element.Text.Trim().Equals(text.Trim()))
                    return DataList.IndexOf(element);
            }

            return -1;
        }


        public WebElementWrapper GetTableDataElement(String ColumnName)
        {
            return this[ColumnName];
        }


        public WebElementWrapper this[int index]
        {
            get
            {
                return new WebElementWrapper(DataList[index]);
            }
        }

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
