using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium.Firefox;
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
        }

        [Test]
        public void GotoURLTest()
        {
            Logger.Info("Starting Test");
            DriverWrapper wrapper = DriverUtils.GetDriver();

            wrapper.Navigate().GoToUrl("http://www.carnaticcorner.com/library.html");

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
            DriverUtils.QuitDrivers();
        }
    }
}
