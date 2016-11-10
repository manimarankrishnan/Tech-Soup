using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using SeleniumCSharp.Selenium.UI;
namespace SeleniumCSharp.Selenium.UI
{
    /// <summary>
    /// Generic Table with only rows
    /// </summary>
    /// <typeparam name="TTableRow">Class that inherits TableRow, and has constructor with no arguments</typeparam>
    public class Table<TTableRow> : Component
        where TTableRow : TableRow, new()
    {

        public virtual By ByTableRow { get { return By.XPath("./tbody/tr[count(./td)>0] | ./tr[count(./td)>0] "); } }

        public ReadOnlyCollection<TTableRow> TableRows { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="by">By</param>
        public Table(IWebDriver driver, By by)
        {
            WrappedElement = new WebElementWrapper(driver, by);
            InitElements();
        }
        /// <summary>
        /// Table element inside another element
        /// </summary>
        /// <param name="parentElement">Parent Element containing the tale element</param>
        /// <param name="by">locator</param>
        public Table(IWebElement parentElement, By by)
        {
            WrappedElement = new WebElementWrapper(parentElement, by);
            InitElements();
        }
        /// <summary>
        /// Table constructor
        /// </summary>
        /// <param name="element">Table WebElement</param>
        public Table(IWebElement element)
        {
            WrappedElement = new WebElementWrapper(element);
            InitElements();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Table()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Initialise Table Rows 
        /// </summary>
        public override void InitElements()
        {
            TableRows = new ComponentCollection<TTableRow>(WrappedElement, ByTableRow);
            foreach (var row in TableRows)
            {
                row.GetColumnlistFunc = delegate() { return ColumnList; };

            }
        }

        /// <summary>
        /// Collection of Column Names
        /// </summary>
        public virtual ReadOnlyCollection<String> ColumnList { get; set; }

        /// <summary>
        /// Get Rows That contains the expected text
        /// </summary>
        /// <param name="expectedText">expected text to be contained in the rows</param>
        /// <returns></returns>
        public virtual List<TTableRow> GetRowsContainingText(String expectedText)
        {

            return TableRows.Where(tr => tr.WrappedElement.Text.Contains(expectedText.Trim())).ToList();
            List<TTableRow> expectedRows = new List<TTableRow>();
            foreach (var row in TableRows)
            {
                if (row.WrappedElement.Text.Contains(expectedText.Trim()))
                    expectedRows.Add(row);
            }
            return expectedRows;
        }

       /// <summary>
       /// Constructs a XPath locator based on the column names to find the Table
       /// This locator is very slow, and its advised not to be used
       /// </summary>
       /// <param name="columnNames">List of column Names</param>
       /// <returns></returns>
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

    /// <summary>
    /// Generic Table with Data Rows and Headers
    /// </summary>
    /// <typeparam name="TTableRow">Class that inherits TableRow, and has constructor with no arguments</typeparam>
    /// <typeparam name="TTableHeader">Class that inherits TableHeaderRow, and has constructor with no arguments</typeparam>
    public class Table<TTableRow, TTableHeader> : Table<TTableRow>
        where TTableRow : TableRow, new()
        where TTableHeader : TableHeaderRow, new()
    {


        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="by">locator</param>
        /// <param name="columnList">List of Custom column names in order </param>
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

        /// <summary>
        /// Get rows that contain the expected value in the  table data corresponding to a column
        /// </summary>
        /// <param name="columnName">Column Name</param>
        /// <param name="expectedValue"> expected value</param>
        /// <returns></returns>
        public virtual List<TTableRow> GetRowsContainingValue(String columnName, String expectedValue)
        {


            return TableRows.Where(tr=> tr.GetText(ColumnList.IndexOf(columnName)).Equals(expectedValue.Trim())).ToList();
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
        /// Returns the first Table row where the text of the Column value is equal to the expected
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

    }

    /// <summary>
    /// Generic Table with Data Rows, Headers and Footers
    /// </summary>
    /// <typeparam name="TTableRow">Class that inherits TableRow, and has constructor with no arguments</typeparam>
    /// <typeparam name="TTableHeader">Class that inherits TableHeaderRow, and has constructor with no arguments</typeparam>
    /// <typeparam name="TTableFooter">Class that inherits TableFooterRow, and has constructor with no arguments</typeparam>
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
