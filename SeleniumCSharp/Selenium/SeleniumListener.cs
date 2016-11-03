using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
namespace SeleniumCSharp.Selenium
{
    class SeleniumListener 
    {

        public static void Main()
        { 
            IWebDriver driver=null;
            OpenQA.Selenium.Support.PageObjects.RetryingElementLocator ele = new OpenQA.Selenium.Support.PageObjects.RetryingElementLocator(driver);
        }
    }
}
