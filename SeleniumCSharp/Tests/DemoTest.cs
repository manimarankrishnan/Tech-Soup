using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Utils.Core;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Tests.Main;
using OpenQA.Selenium;

namespace SeleniumCSharp.Tests
{
    [TestFixture]
    public class DemoTest
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
            Logger.Info("Setup completed");
        }

        [Test]
        [Parallelizable(ParallelScope.Fixtures)]
        public void LoginToSchoolnet()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage loginPage = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));
                loginPage.Data = new Data("TestCaseData_Authentication_DistrictAdmin");
                loginPage.Form.InputFormFields().SubmitForm();
            }
            catch (Exception e)
            {
                 driver.SaveScreenshotAndPageSource();;
                throw;
            }
           

        }

        [Test]
        [Parallelizable(ParallelScope.Fixtures)]
        public void TestSelectElementWrapper()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage loginPage = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));
                loginPage.Data = new Data("TestCaseData_Authentication_DistrictAdmin");
                loginPage.Form.InputFormFields().SubmitForm();
                driver.Navigate().GoToUrl("https://team-automation-st.sndev.net/Assess/TestCentralHome.aspx");
                SelectElementWrapper adminYear = new SelectElementWrapper(driver,By.Id("ctl00_MainContent_TestSearchResults1_TestFinderSearch1_schoolYearId"));
                var ele = adminYear.SelectedOption;
                var parentElement = ele.ParentElement;
                Assert.AreEqual(parentElement.TagName, "select", "Wrong Parent element tag name is displayed");
            }
            catch (Exception e)
            {
                 driver.SaveScreenshotAndPageSource();;
                throw;
            }


        }

        [Test]
        [Parallelizable(ParallelScope.Fixtures)]
        public void TestWebElementWrapper()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage page = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));
                page.Data = new Data("TestCaseData_Authentication_Teacher");
                //Test method InputFormFields
                page.Form.InputFormFields();
                //Test method Clear
                page.Form.UserName.Clear();
                page.Form.UserName.Clear();
                //Test method Click
                page.Form.Submit.Click();
                //Test property Displayed
                page.Form.InitElements();
                bool isUserNameDisplayed = page.Form.UserName.Displayed;
                Assert.IsTrue(isUserNameDisplayed,"UserName textbox is displayed");
                //Test property Enabled
                bool isUserNameEnabled = page.Form.UserName.Enabled;
                Assert.IsTrue(isUserNameEnabled, "UserName textbox is Enabled");
                //Test method GetAttribute
                string attribute = page.Form.UserName.GetAttribute("type");
                Assert.AreEqual(attribute, "text", "UserName type is different");
                //test method GetCssValue
                string cssValueHeightOfUserName = page.Form.UserName.GetCssValue("height");
                Assert.AreEqual(cssValueHeightOfUserName, "20px", "UserName height is not 20px");
                //Test property Location
                System.Drawing.Point locationOfUserName = page.Form.UserName.Location;
                //Test property Size
                System.Drawing.Size sizeOfUserName = page.Form.UserName.Size;
                //Test method SendKeys
                page.Form.UserName.SendKeys("vj_teacher");
                page.Form.Password.SendKeys("sch00lnet");
                //Login
                page.Form.Submit.Click();
            }
            catch(Exception e)
            {
                 driver.SaveScreenshotAndPageSource();;
                throw;
            }

        }

        [Test]
        [Parallelizable(ParallelScope.Fixtures)]
        public void TestSelectElementWrapper2()
        {
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                LoginPage page = LoginPage.NavigateToLoginPage(driver, Config.GetConfigValue("StartingUrl"));              
                page.Data = new Data("TestCaseData_Authentication_DistrictAdmin");
                page.Form.InputFormFields().SubmitForm();
                driver.Navigate().GoToUrl("https://team-automation-st.sndev.net/Assess/TestCentralHome.aspx");
                SelectElementWrapper adminYear = new SelectElementWrapper(driver, By.Id("ctl00_MainContent_TestSearchResults1_TestFinderSearch1_schoolYearId"));
                //Test property SelectedOption
                var selectedElement = adminYear.SelectedOption;
                //Test property  Options
                var allOptions = adminYear.Options;
                //Test property AllSelectedOptions
                var allSelected = adminYear.AllSelectedOptions;
                //Test method SelectByValue
                adminYear.SelectByValue("2009");
                //Test property IsMultiple
                bool a =adminYear.IsMultiple;
                //Test property ParentElement

                //adminYear = new SelectElementWrapper(driver, By.Id("ctl00_MainContent_TestSearchResults1_TestFinderSearch1_schoolYearId"));
                //Test method SelectByText
                adminYear.SelectByText("2010-2011");
                //adminYear = new SelectElementWrapper(driver, By.Id("ctl00_MainContent_TestSearchResults1_TestFinderSearch1_schoolYearId"));
                //Test method SelectByIndex
                adminYear.SelectByIndex(2);                

            }
            catch (StaleElementReferenceException see)
            {
                driver.SaveScreenshotAndPageSource(); 
                throw;
            }
            catch (Exception e)
            {
                 driver.SaveScreenshotAndPageSource();;
                throw;
            }
        }


        [Test]
        public void TestElementWait()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            DriverWrapper driver = DriverUtils.GetDriver();
            try
            {
                driver.Navigate().GoToUrl("file:///C:/Users/prasanna.selvaraj/Desktop/index.html");
                WebElementWrapper CreateElement = new WebElementWrapper(driver, By.Id("loadButton"));
                CreateElement.Click();
                sw.Start();
                WebElementWrapper ClickMeElement = new WebElementWrapper(driver, By.Id("btn1"));

                ClickMeElement.Click();
                sw.Stop();

            }
           
            catch (Exception e)
            {
                sw.Stop();
                var ss = sw.Elapsed;
                 driver.SaveScreenshotAndPageSource();;
                throw;
            }
        }


        [TearDown]
        public void CleanUp()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            Logger.Debug("Test Result : " + testStatus);
            if(!testStatus.ToString().Equals("Passed",StringComparison.OrdinalIgnoreCase))
            {
                Logger.Error("{0}\nStactrace:\n{1}", TestContext.CurrentContext.Result.Message, TestContext.CurrentContext.Result.StackTrace);
            }
            DriverUtils.QuitDrivers();
        }
    }
}
