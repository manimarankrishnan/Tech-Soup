using System;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


namespace SeleniumCSharp.Pages
{
    public class Agreements : Page
    {
        public Agreements(DriverWrapper Driver) : base(Driver)
        {

        }

        public void SelectAgreeButtonInAgreementsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#userAgree0")));
            Driver.FindElement(By.CssSelector("label >strong")).Click();

        }

        public void ClickGoBackButtonInAgreementsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_butPrevious']")));
            Driver.FindElement(By.CssSelector
               ("input[id*='_butPrevious']")).Click();

        }

        public void ClickContinueButtonInAgreementsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id='Button1']")));
            Driver.FindElement(By.CssSelector
               ("input[id='Button1']")).Click();

        }

        public void ClickPrintsthisPageLinkInAgreementsPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("a[onclick='javascript: print();']")));
            try
            {
                Driver.FindElement(By.CssSelector
               ("a[onclick='javascript: print();']")).Click();

            }
            catch (Exception e)
            {


            }
        }

        public bool IsPrintsthisPageLinkDsiaplyed()
        {
           if(Driver.FindElement(By.PartialLinkText(" Print this page")).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
       }
    }


}
