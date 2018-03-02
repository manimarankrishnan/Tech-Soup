using System;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.Selenium;
using System.Collections.Generic;


namespace SeleniumCSharp.Pages
{
    public class Shipping : Page
    {
        public Shipping(DriverWrapper Driver) : base(Driver)
        {


        }

        public List<String> GetRecepientFieldsValuesInShippingPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id='txtFirstNameAjs']")));
            List<String> recepientFields = new List<string>();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            recepientFields.Add(js.ExecuteScript("return $(\"input[id='txtFirstNameAjs']\").val();").ToString());
            recepientFields.Add(js.ExecuteScript("return $(\"input[id='txtLastNameAjs']\").val();").ToString());
            return recepientFields;
        }

        public bool IsShippingSpeedCheckBoxSelected()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id='userCheck0']")));
            if (Driver.FindElement(By.CssSelector("input[id='userCheck0']")).GetAttribute("checked") == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
               
        }

        public List<String> GetShippingAddressValues()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("em.emphasized.ng-binding.ng-scope")));
            List<String> shippingAddressValues = new List<string>();
            shippingAddressValues.Add(Driver.FindElement(By.Id("AddrLine1")).GetAttribute("innerHTML"));
            shippingAddressValues.Add(Driver.FindElement(By.CssSelector("#AddrLine2")).GetAttribute("innerHTML"));
            shippingAddressValues.Add(Driver.FindElement(By.CssSelector("#AddrCountry")).GetAttribute("innerHTML"));
            shippingAddressValues.Add(Driver.FindElement(By.CssSelector("#AddrStateProvince")).GetAttribute("innerHTML"));
            shippingAddressValues.Add(Driver.FindElement(By.CssSelector("#AddrCity")).GetAttribute("innerHTML"));
            shippingAddressValues.Add(Driver.FindElement(By.CssSelector("#AddrZipPostalCode")).GetAttribute("innerHTML"));
            return shippingAddressValues;

        }

        public String GetOrgEmailAddressValue()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("em[class='emphasized ng-binding']")));
            return Driver.FindElement(By.CssSelector("em[class='emphasized ng-binding']")).Text;
        }

        public void ClickChangeEmailButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("a.btn.btn-white")));
            Driver.FindElement(By.CssSelector("a.btn.btn-white")).Click();
        }

        public void ClickContinueButtonInShippingPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id='PassDataToBill']")));
            Driver.FindElement(By.CssSelector("input[id='PassDataToBill']")).Click();
        }

        public void ClickGoBackButtonInShippingPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_butPrevious']")));
            Driver.FindElement(By.CssSelector("input[id*='_butPrevious']")).Click();
        }

    }
}
