using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumCSharp.Selenium.UI
{

    public abstract class TableRow : Component
    {
        public virtual By ByData { get { return By.TagName("td"); } }

        public abstract ReadOnlyCollection<String> ColumnList { get; }

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

        private ReadOnlyCollection<IWebElement> _dataList;

        public ReadOnlyCollection<IWebElement> DataList
        {
            get
            {
                return _dataList ?? RootElement.FindElements(ByData);
            }
            set { _dataList = value; }
        }

        public TableRow()
        {

        }

        public TableRow(IWebElement element)
        {
            RootElement = element;
        }

        public override void InitElements()
        {
            new WebElementWrapper(RootElement, By.Id(""));
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


    }
}
