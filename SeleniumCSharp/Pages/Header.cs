using System;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ReportAddin;

namespace SeleniumCSharp.Tests
{
    public class Header : Page
    {
        public Header(DriverWrapper Driver) : base(Driver)
        {

        }

        // Verify that redirected to the Home Page
        public bool IsProductSearchIconDisplayed()
        {
            if (Driver.FindElement(By.CssSelector("a#navSearch")).Displayed == true)
            {
                return true;
            }
            else
            {
                throw new Exception("Unable to login into the application");
            }
        }

        public void SearchProduct(string prod)
        {
            Driver.FindElement(By.CssSelector("a#navSearch")).Click();
            Driver.FindElement(By.CssSelector("#searchContent")).SendKeys(prod);
            Driver.FindElement(By.CssSelector("button.search-button-rwd")).Click();
            Report.LogPass("Search the product" + prod);
        }

        public String GetNoOfProductsAddedInCart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("span[id='cartNum']>span[class='sr-only']")));
          return Driver.FindElement(By.CssSelector
                ("span[id='cartNum']")).Text.Remove(0, 16);
        }

        public void ClickCartIcon()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("span[id='cartNum']>span[class='sr-only']")));
            Driver.FindElement(By.CssSelector("a[id='navCart']")).Click();
            Console.WriteLine("Clicked on Cart Icon");
        }


    }
}
