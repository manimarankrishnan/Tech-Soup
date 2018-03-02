using NUnit.Framework;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using Utils.Core;
using System;
using OpenQA.Selenium;
namespace SeleniumCSharp.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class DriverWrapperTests
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
