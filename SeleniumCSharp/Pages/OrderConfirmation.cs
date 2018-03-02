using System;
using System.Collections.Generic;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace SeleniumCSharp.Pages
{
    public class OrderConfirmation : Page
    {
        public OrderConfirmation(DriverWrapper Driver) : base(Driver)
        {


        }

        public String getRequestNumberInOrderConfirmationPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("h2>em.emphasized")));
            return Driver.FindElement(By.CssSelector("h2>em.emphasized")).Text;
        }

        public void clickDonationHistoryLinkInOrderConfirmationPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("a.donation-req")));
            Driver.FindElement(By.CssSelector("a.donation-req")).Click();
        }

        public List<String> getConfirmationEmailValueInOrderConfirmationPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("p>em.emphasized")));
            IList<IWebElement> text = Driver.FindElements(By.CssSelector("p>em.emphasized"));
            List<String> Email = new List<String>();
            for (int i = 0; i < text.Count; i++)
            {
                Email.Add(text[i].Text);
            }
            return Email;
        }

        public void clickProdDonationFAQLinkInOrderConfirmationPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("a[href*='faq']")));
            Driver.FindElement(By.CssSelector("a[href*='faq']")).Click();
        }

        public String getTotalDueAmountInOrderConfirmationPage()
        {
            Double totDue;
            Double.TryParse(Driver.FindElement(By.CssSelector("#GrandTotalAmount")).Text.Trim('$'), out totDue);
            return totDue.ToString();
        }

        public List<String> getRequestDetailsInOrderConfirmationPage()
        {
            List<String> reqDetails = new List<String>();
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(2)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(4)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(6)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(8)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(10)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(12)")).Text);
            reqDetails.Add(Driver.FindElement(By.CssSelector("dl[class='dl-horizontal']>dd:nth-child(14)")).Text);
            return reqDetails;
        }
    }
}
