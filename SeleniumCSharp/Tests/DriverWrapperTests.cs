using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumCSharp.Framework;

namespace SeleniumCSharp.Tests
{
    
    public class DriverWrapperTests
    {

        [Test]
        public void  GotoURLTest(){

            DriverWrapper wrapper = DriverUtils.GetDriver();

            wrapper.Navigate().GoToUrl("http://www.carnaticcorner.com/library.html");


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
