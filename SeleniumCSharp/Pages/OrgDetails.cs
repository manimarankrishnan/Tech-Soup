using System;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.Selenium;
using System.Collections.Generic;

namespace SeleniumCSharp.Pages
{
    public class OrgDetails : Page
    {
        public OrgDetails(DriverWrapper Driver) : base(Driver)
        {


        }

        public void ClickCancelButtonInOrgDetailsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_CancelButton']")));
            Driver.FindElement(By.CssSelector("input[id*='_CancelButton']")).Click();
        }
        public List<String> getOrgDetailsInOrgDetailsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_Email']")));
            List<String> orgDetails = new List<string>();
            orgDetails.Add(Driver.FindElement(By.CssSelector("input[id*='_Email']")).GetAttribute("value"));
            orgDetails.Add(Driver.FindElement(By.CssSelector("input[id*='_Address1']")).GetAttribute("value"));
            orgDetails.Add(Driver.FindElement(By.CssSelector("input[id*='_Address2']")).GetAttribute("value"));
            orgDetails.Add(Driver.FindElement(By.CssSelector("input[id*='_City']")).GetAttribute("value"));
            orgDetails.Add(Driver.FindElement(By.CssSelector("input[id*='_PostalCode']")).GetAttribute("value"));
            return orgDetails;
        }

    }
}
