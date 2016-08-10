using NUnit.Framework;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using WebServiceCSharp.Core;
using System;
using OpenQA.Selenium;
namespace SeleniumCSharp.Tests
{

    public class DriverCommandsTests
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
        public void DriverCommandTest()
        {
            DriverWrapper wrapperObject = DriverUtils.GetDriver();
            wrapperObject.Navigate().GoToUrl("http://www.bing.com");
            string title = wrapperObject.Title;
            Assert.AreEqual(title, "Bing");

            var driverCommandObject = new DriverCommands(wrapperObject);
            driverCommandObject.SavePageSource();
            driverCommandObject.SaveScreenshot();
            
        }


        [TearDown]
        public void CleanUp()
        {
            Logger.Info("Entering Cleanup");
            DriverUtils.QuitDrivers();
            Logger.Info("Completed Cleanup");

        }
    }
}
