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
        public void TestTable()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage loginPage = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));
                loginPage.Data = new Data("TestCaseData_Authentication_DistrictAdmin");
                loginPage.Form.InputFormFields().SubmitForm();


                //Schooolnet 
                driver.Navigate().GoToUrl("https://team-automation-st.sndev.net/Assess/TestCentralHome.aspx");

                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                Table<TestCentralRow,TableHeaderRow> testCentralTable = new Table<TestCentralRow,TableHeaderRow>(driver, By.Id("ctl00_MainContent_TestSearchResults1_TestFinderResults1_gridResults"));
                watch.Stop();
                var ss = watch.ElapsedMilliseconds;


                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, testCentralTable.TableRows.Count);
                watch.Reset();
                watch.Start();
                dynamic rows = testCentralTable.GetRowsContainingValue("Test Name", "schedulefuture 12128");
                watch.Stop();
                var ssd = watch.ElapsedMilliseconds;

                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, testCentralTable.TableRows.Count);

                //Schoolnet end

                //Magazine start

                driver.Navigate().GoToUrl("https://www.smashingmagazine.com/2008/08/top-10-css-table-designs/");

                watch.Reset();
                watch.Start();
                Table<MagagzineRow,TableHeaderRow> magaZineTable = new Table<MagagzineRow,TableHeaderRow>(driver, By.Id("newspaper-b"));
                watch.Stop();
                ss = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, magaZineTable.TableRows.Count);
                watch.Reset();

                //Get Row
                watch.Start();
                rows = magaZineTable.GetRowsContainingValue("Q2", "30.2");
                watch.Stop();
                ssd = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, magaZineTable.TableRows.Count);
                watch.Reset();

                //Table from columns
                watch.Start();
                magaZineTable = new Table<MagagzineRow,TableHeaderRow>(driver, Table<MagagzineRow>.GetTableLocatorFromColumnNames(MagagzineRow.ExpectedColumnList.ToArray()));
                watch.Stop();
                ss = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, magaZineTable.TableRows.Count);
                watch.Reset();
                watch.Start();
                rows = magaZineTable.GetRowsContainingValue("Q2", "30.2");
                watch.Stop();
                ssd = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, magaZineTable.TableRows.Count);

                //Magazine end
                driver.Navigate().GoToUrl("https://datatables.net/examples/basic_init/hidden_columns.html");

                watch.Reset();
                watch.Start();
                Table<DataTableExample,TableHeaderRow> dataTableEx = new Table<DataTableExample,TableHeaderRow>(driver, By.Id("example"));
                watch.Stop();
                ss = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, dataTableEx.TableRows.Count);
                watch.Reset();

                //Get Row
                watch.Start();
                rows = dataTableEx.GetRowsContainingValue("Position", "Software Engineer");
                watch.Stop();
                ssd = watch.ElapsedMilliseconds;
                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, dataTableEx.TableRows.Count);
                watch.Reset();



            }
            catch (Exception e)
            {
                driver.SaveScreenshotAndPageSource(); ;
                throw;
            }


        }



        [Test]
        public void ItemPropertiesPageTables()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage loginPage = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));
                loginPage.Data = new Data("TestCaseData_Authentication_DistrictAdmin");
                loginPage.Form.InputFormFields().SubmitForm();


                
                driver.Navigate().GoToUrl("https://team-automation-st.sndev.net/admin/?&admin_current_app_tab_id=5e10edea-eb8b-4000-b8cd-91a9ec533c89&admin_current_control_src=~/Assess/AdminControls/ManageItemCategories.ascx&snidx=5");
                
                //Categories Table - 1st implementation with footer - takes more time to initialize
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                CategoriesTable CategoriesTable = new CategoriesTable(driver);
                
                watch.Stop();
                var ss = watch.ElapsedMilliseconds;


                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, CategoriesTable.TableRows.Count);
                watch.Reset();
                watch.Start();
                dynamic rows = CategoriesTable.GetRowsContainingValue("Category Name", "Curriculum 3");
                watch.Stop();
                var ssd = watch.ElapsedMilliseconds;

                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, CategoriesTable.TableRows.Count);

                //Categories Table - 1st implementation with footer - takes more time to initialize
               
                watch.Start();
                CategoriesTable = new CategoriesTable (driver);
                watch.Stop();
                 ss = watch.ElapsedMilliseconds;


                Logger.Info("Took {0} milliseconds to initialize table with {1} rows", ss, CategoriesTable.TableRows.Count);
                watch.Reset();
                watch.Start();
                 rows = CategoriesTable.GetRowsContainingValue("Category Name", "Curriculum 3");
                watch.Stop();
                 ssd = watch.ElapsedMilliseconds;

                Logger.Info("Took {0} milliseconds to get desired value from table with {1} rows", ssd, CategoriesTable.TableRows.Count);

                
                //Schoolnet end



            }
            catch (Exception e)
            {
                driver.SaveScreenshotAndPageSource(); ;
                throw;
            }


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
