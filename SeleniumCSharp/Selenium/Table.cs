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

    public class Table<TTableRow> : Component
        where TTableRow : TableRow, new()
    {

        public virtual By ByTableRow { get { return By.XPath("./tbody/tr[count(./td)>0] | ./tr[count(./td)>0] "); } }


        public ReadOnlyCollection<TTableRow> TableRows { get; set; }



        public Table(IWebDriver driver, By by)
        {
            WrappedElement = new WebElementWrapper(driver, by);
            InitElements();
        }

        public Table(IWebElement parentElement, By by)
        {
            WrappedElement = new WebElementWrapper(parentElement, by);
            InitElements();
        }
        public Table(IWebElement element)
        {
            WrappedElement = new WebElementWrapper(element);
            InitElements();
        }

        public Table()
        {
            // TODO: Complete member initialization
        }

        public override void InitElements()
        {
            TableRows = new ComponentCollection<TTableRow>(WrappedElement, ByTableRow);
            foreach (var row in TableRows)
            {
                row.GetColumnlistFunc = delegate() { return ColumnList; };

            }
        }
        public virtual ReadOnlyCollection<String> ColumnList { get; set; }

        public virtual List<TTableRow> GetRowsContainingText(String expectedText)
        {
            List<TTableRow> expectedRows = new List<TTableRow>();
            foreach (var row in TableRows)
            {
                if (row.WrappedElement.Text.Contains(expectedText.Trim()))
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


    public class Table<TTableRow, TTableHeader> : Table<TTableRow>
        where TTableRow : TableRow, new()
        where TTableHeader : TableHeaderRow, new()
    {


        #region Constructors
        public Table(IWebDriver driver, By by, List<string> columnList = null)
            : base(driver, by)
        {
            if (columnList != null)
                this.ColumnList = new ReadOnlyCollection<string>(columnList);
        }

        public Table(IWebElement parentElement, By by, List<string> columnList = null)
            : base(parentElement, by)
        {
            if (columnList != null)
                this.ColumnList = new ReadOnlyCollection<string>(columnList);
        }
        public Table(IWebElement element, List<string> columnList = null)
            : base(element)
        {
            if (columnList != null)
                this.ColumnList = new ReadOnlyCollection<string>(columnList);
        }

        public Table()
        {
            // TODO: Complete member initialization
        }

        #endregion


        public virtual By ByTableHeaderRow { get { return By.XPath("./thead/tr | .//tr[count(./th)>0]"); } }
        public TTableHeader HeaderRow { get; set; }
        private ReadOnlyCollection<String> _columnList;
        public override ReadOnlyCollection<String> ColumnList
        {
            get
            {
                return _columnList ?? HeaderRow.ColumnList;
            }
            set
            {
                _columnList = value;
            }
        }
        public override void InitElements()
        {
            base.InitElements();
            HeaderRow = new ComponentCollection<TTableHeader>(WrappedElement, ByTableHeaderRow).FirstOrDefault();
        }

        public virtual List<TTableRow> GetRowsContainingValue(String columnName, String expectedValue)
        {
            int dataIndex = ColumnList.IndexOf(columnName);
            List<TTableRow> expectedRows = new List<TTableRow>();
            foreach (var row in TableRows)
            {
                if (row.GetText(dataIndex).Equals(expectedValue.Trim()))
                    expectedRows.Add(row);
            }
            return expectedRows;
        }

        /// <summary>
        /// Returns a list of Table rows where the text of the Column value is equal to the expected
        /// </summary>
        /// <param name="columnName">Column Name</param>
        /// <param name="expectedValue">Expected Value </param>
        /// <returns></returns>
        public TTableRow this[string columnName, string expectedValue]
        {
            get
            {
                return GetRowsContainingValue(columnName, expectedValue).FirstOrDefault();
            }
        }

        public String this[int index]
        {
            get
            {
                return "HI";
            }
        }

    }


    public class Table<TTableRow, TTableHeader, TTableFooter> : Table<TTableRow, TTableHeader>
        where TTableRow : TableRow, new()
        where TTableHeader : TableHeaderRow, new()
        where TTableFooter : TableFooterRow, new()
    {


        #region Constructors
        public Table(IWebDriver driver, By by)
            : base(driver, by)
        {

        }

        public Table(IWebElement parentElement, By by)
            : base(parentElement, by)
        {

        }
        public Table(IWebElement element)
            : base(element)
        {

        }

        public Table()
        {
            // TODO: Complete member initialization
        }

        #endregion

        public virtual By ByTableFooterRow { get { return By.XPath(".//tfoot/tr"); } }

        public TTableFooter FooterRow { get; set; }

        public ReadOnlyCollection<String> ColumnList
        {
            get
            {
                return HeaderRow.ColumnList;
            }
        }
        public override void InitElements()
        {
            base.InitElements();
            FooterRow = new ComponentCollection<TTableFooter>(WrappedElement, ByTableFooterRow).FirstOrDefault();

        }


    }



    public class TableWithHeaders : Table<TableRow, TableHeaderRow>
    {

        public TableWithHeaders(IWebDriver driver, By by, List<string> columnList = null)
            : base(new WebElementWrapper(driver, by), columnList)
        {

        }

        public TableWithHeaders(IWebElement tableElement, List<string> columnList = null)
            : base(new WebElementWrapper(tableElement), columnList)
        {

        }

        public TableWithHeaders(IWebElement parentElement, By by, List<string> columnList = null)
            : base(new WebElementWrapper(parentElement, by), columnList)
        {

        }

    }


}
