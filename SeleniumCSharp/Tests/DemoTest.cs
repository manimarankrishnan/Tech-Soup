using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Tests.Main;
namespace SeleniumCSharp.Tests
{
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
                new DriverCommands(driver).GetScreenshotAndPageSource();
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
