using NUnit.Framework;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using WebServiceCSharp.Core;
using System;
using OpenQA.Selenium;
namespace SeleniumCSharp.Tests
{

    public class DriverWrapperTests
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
        public void GotoURLTest()
        {
            Logger.Info("Starting Test");
           
            DriverWrapper wrapper = DriverUtils.GetDriver();

            wrapper.Navigate().GoToUrl("http://www.carnaticcorner.com/library.html");
            wrapper.SaveScreenshotAndPageSource();

            DriverWrapper wrapper1 = DriverUtils.GetDriver();

            wrapper1.Navigate().GoToUrl("http://www.carnaticcorner.com/library.html");
            Logger.Info("Finished test");

        }



        [Test]
        public void CreateDriver()
        {

            DriverWrapper wrapper1 = DriverUtils.GetDriver();

            wrapper1.Navigate().GoToUrl("http://www.carnaticcorner.com/library.html");
        }


        [Test]
        public void CloseDriver()
        {
            DriverWrapper wrapperObject = DriverUtils.GetDriver();
            wrapperObject.Navigate().GoToUrl("http://www.google.co.in");
            string sessionID = wrapperObject.GetSessionId();
            wrapperObject.Close();
            string sessionIDNew = wrapperObject.GetSessionId();
            Assert.IsNotNull(sessionIDNew,"sessionID must be not null because driver instance is closed not quitted");
        }


        [Test]
        public void QuitDriver()
        {
            DriverWrapper wrapperObject = DriverUtils.GetDriver();
            wrapperObject.Navigate().GoToUrl("http://www.google.co.in");
            string sessionID = wrapperObject.GetSessionId();
            wrapperObject.Quit();
            try
            {
                string url = wrapperObject.Url;
            }
            catch (WebDriverException e)
            {
                e.Message.Contains("A exception with a null response was thrown sending an HTTP request to the remote WebDriver server for URL");    
            }
        }


        [Test]
        public void DriverWrapperTest()
        {
            DriverWrapper wrapperObject = DriverUtils.GetDriver();
            wrapperObject.Navigate().GoToUrl("http://www.google.co.in");
            string title = wrapperObject.Title;
            Assert.AreEqual(title, "Google");
            Type driverType = wrapperObject.GetType();
            Assert.AreEqual(driverType.Name, "DriverWrapper");
            string url = wrapperObject.Url;
            url.Contains("google");

            //Open another instance
            DriverWrapper wrapperObjectNew = DriverUtils.GetDriver();
            wrapperObjectNew.Navigate().GoToUrl("https://www.bing.com/");
            string title1 = wrapperObjectNew.Title;
            Assert.AreEqual(title1, "Bing");
            Type driverType1 = wrapperObjectNew.GetType();
            Assert.AreEqual(driverType1.Name, "DriverWrapper");
            string url1 = wrapperObjectNew.Url;
            url1.Contains("bing");
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
