using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using SeleniumCSharp.Selenium.UI;
namespace SeleniumCSharp.Selenium
{

    public class Table<TTableRow> : Component where TTableRow : TableRow, new()
    {

        public virtual By ByTableRow { get { return By.XPath("./tbody/tr[count(./td)>0] | ./tr[count(./td)>0] "); } }
        public virtual By ByTableHeaderRow { get { return By.XPath(" ./thead/tr | .//tr[count(./th)>0]"); } }
        public virtual By ByTableFooterRow { get { return By.XPath(".//tfoot/tr"); } }

        public ReadOnlyCollection<String> ColumnList { get
        {
            return HeaderRow.ColumnList;         
        }
        
        }
        public ReadOnlyCollection<TTableRow> TableRows { get; set; }
        public TableHeaderRow HeaderRow { get; set; }
        public TableHeaderRow FooterRow { get; set; }

        public Table(IWebDriver driver, By by)
        {
            RootElement = new WebElementWrapper(driver, by);
            InitElements();
        }

        public Table(IWebElement parentElement, By by)
        {
            RootElement = new WebElementWrapper(parentElement, by);
            InitElements();
        }
        public Table(IWebElement element)
        {
            RootElement = new WebElementWrapper(element);
            InitElements();
        }

        public override void InitElements()
        {
            TableRows = new ComponentCollection<TTableRow>(RootElement, ByTableRow);
            HeaderRow = new ComponentCollection<TableHeaderRow>(RootElement, ByTableHeaderRow).FirstOrDefault();
            FooterRow = new ComponentCollection<TableHeaderRow>(RootElement, ByTableFooterRow).FirstOrDefault();
        }



        public List<TTableRow> GetRowsContainingText(String columnName, String expectedText)
        {
            int dataIndex = ColumnList.IndexOf(columnName);
            List<TTableRow> expectedRows = new List<TTableRow>();
            foreach (var row in TableRows)
            {
                if (row.GetText(dataIndex).Equals(expectedText.Trim()))
                    expectedRows.Add(row);
            }
            return expectedRows;
        }


        public static By GetTableLocatorFromColumnNames(params string[] columnNames)
        {

            StringBuilder builder = new StringBuilder("//tr");
            builder.Append("[count(./th)>0]");      //Contains table header
            builder.Append("[count(./tr)=0]");     //Doesn't have table row inside it

            foreach (string column in columnNames)
                builder.Append(String.Format("[./th[normalize-space()='{0}']]", column));
            builder.Append("/ancestor::table[count(.//table)=0]");
            return By.XPath(builder.ToString());
        }
    }
}
