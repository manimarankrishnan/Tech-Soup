﻿using NUnit.Framework;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using WebServiceCSharp.Core;
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
            var dc = new DriverCommands(wrapper);
            dc.SavePageSource();
            dc.SaveScreenshot();


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

        [TearDown]
        public void CleanUp()
        {
            Logger.Info("Entering Cleanup");
            DriverUtils.QuitDrivers();
            Logger.Info("Completed Cleanup");

        }
    }
}
