using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Tests.Main;
using SeleniumCSharp.Tests.Main.Components;
using Utils.Core;
using OpenQA.Selenium;
using SeleniumCSharp.Selenium.UI;
namespace SeleniumCSharp.Tests
{


    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TableTests
    {
        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
            Logger.Info("Setup completed");
        }

        [Test]
        public void TableGettingRowsWithSpecificValue()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                //Magazine end
                driver.Navigate().GoToUrl("https://datatables.net/examples/basic_init/hidden_columns.html");
                //Initialising the table
                TableWithHeaders dataTableEx = new TableWithHeaders(driver, By.Id("example"));

                //Getting rows where the value in the 'Position' Column equals 'Software Engineer'
                List<TableRow> rows = dataTableEx.GetRowsContainingValue("Position", "Software Engineer");

                //iterating for each row
                foreach (var row in rows)
                {

                    /* Using the name of the column */

                    //In each row getting the td element corresponding to the 'Position' column
                    IWebElement TdElementCorrespondingToPosition = row["Position"];
                    Assert.AreEqual(TdElementCorrespondingToPosition.Text, "Software Engineer");


                    //Same td element can be got by passing the column name to the GetTableDataElement() method
                    TdElementCorrespondingToPosition = row.GetTableDataElement("Position");
                    Assert.AreEqual(TdElementCorrespondingToPosition.Text, "Software Engineer");

                    /* Using the index of the column */

                    //Same td element can be got using the index of the column (0 based index)
                    TdElementCorrespondingToPosition = row[1];
                    Assert.AreEqual(TdElementCorrespondingToPosition.Text, "Software Engineer");

                    //Same td element can be got by passing the index of the column (0 based index) to the GetTableDataElement() method
                    TdElementCorrespondingToPosition = row.GetTableDataElement(1);
                    Assert.AreEqual(TdElementCorrespondingToPosition.Text, "Software Engineer");

                }



            }
            catch (Exception)
            {

                throw;
            }

        }

        [Test]
        public void GetFirstRowWithTextEqualsAValue()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                //Magazine end
                driver.Navigate().GoToUrl("https://datatables.net/examples/basic_init/hidden_columns.html");
                //Initialising the table
                TableWithHeaders dataTableEx = new TableWithHeaders(driver, By.Id("example"));

                //Getting first row where the value in the 'Position' Column equals 'Software Engineer'
                TableRow row = dataTableEx["Position", "Software Engineer"];
                //In each row getting the td element corresponding to the 'Position' column
                IWebElement TdElementCorrespondingToPosition = row["Position"];
                Assert.AreEqual(TdElementCorrespondingToPosition.Text, "Software Engineer");

            }
            catch (Exception)
            {

                throw;
            }
        }


        [Test]
        public void SelectingParticularRows()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                TableWithHeaders Table;

                #region Check/UnCheck a checkbox in a row

                driver.Navigate().GoToUrl("http://yuilibrary.com/yui/docs/datatable/datatable-chkboxselect.html");
                Table = new TableWithHeaders(driver, By.CssSelector("table.yui3-datatable-table"), new List<string>()
                {
                    "",
                    "Port No.",
                    "Protocol",
                    "Common Name"

                });

                var Port995 = Table.GetRowsContainingValue("Port No.", "995");
                foreach (var row in Port995)
                {

                    //To click a Check box, row[""] returns a WebElementWrapper , 
                    //we can use the DescendantCheckbox property to get the checkbox inside the td element
                    //Number of handy properties are present inside the WebElementWrapper class
                    row[""].DescendantCheckbox.UnCheck();
                    Assert.IsFalse(row[""].DescendantCheckbox.Selected);
                }
                #endregion

                #region Custom Column Name

                //If the Column Name is empty, in the above example we are using ""(empty string) as column name
                //We can give Custom Names to this column by passing the columns in the particular order when initialising the table
                driver.Navigate().GoToUrl("http://yuilibrary.com/yui/docs/datatable/datatable-chkboxselect.html");
                Table = new TableWithHeaders(driver, By.CssSelector("table.yui3-datatable-table"), new List<string>()
                {
                    "Select",
                    "Port No.",
                    "Protocol",
                    "Common Name"

                });

                Port995 = Table.GetRowsContainingValue("Port No.", "995");
                foreach (var row in Port995)
                {
                    //Now we can use the custom column name 'Select' to select the particular td element
                    row["Select"].DescendantCheckbox.UnCheck();
                    Assert.IsFalse(row["Select"].DescendantCheckbox.Selected);
                }
                #endregion

                #region Radio Grid Selecting a radio button

                driver.Navigate().GoToUrl("https://css-tricks.com/examples/RadioGrid/");
                Table = new TableWithHeaders(driver, By.CssSelector("#page-wrap>table"));

                var Snickers = Table.GetRowsContainingValue("", "Snickers");

                Snickers[0].GetTableDataElement("1").DescendantRadioButton.Check();
                foreach (var row in Snickers)
                {
                    row["1"].DescendantRadioButton.Check();
                    //Using the DescendantRadioButton property to get the radio button inside the td
                    Assert.IsTrue(row["1"].DescendantRadioButton.Selected);
                }

                Table["", "Snickers"]["1"].DescendantRadioButton.Check();
                #endregion

                #region Check without checkbox

                driver.Navigate().GoToUrl("https://datatables.net/extensions/select/examples/initialisation/checkbox.html");
                Table = new TableWithHeaders(driver, By.Id("example"));
                var softwareEng = Table.GetRowsContainingValue("Position", "Software Engineer");
                foreach (var row in softwareEng)
                {
                    row[""].Click();
                }
                #endregion
            }
            catch (Exception e)
            {
                driver.SaveScreenshotAndPageSource(); ;
                throw;
            }


        }

        public void AdvancedUseOfGenericTypes()
        {

        }

        [TearDown]
        public void CleanUp()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            Logger.Debug("Test Result : " + testStatus);
            if (!testStatus.ToString().Equals("Passed", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Error("{0}\nStactrace:\n{1}", TestContext.CurrentContext.Result.Message, TestContext.CurrentContext.Result.StackTrace);
            }
            DriverUtils.QuitDrivers();
        }

    }
}
