using System;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;


namespace SeleniumCSharp.Pages
{
    public class Cart : Page
    {
        public Cart(DriverWrapper Driver) : base(Driver)
        {

        }

        public void ClickClearCartButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input#Clear1")));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("input#Clear1")));
            actions.Perform();
            Driver.FindElement(By.CssSelector
               ("input#Clear1")).Click();
            Console.WriteLine("Clicked on Clear cart button");

        }


        public void ClickProductRemoveLink()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("a#RemoveProductLink")));
            Driver.FindElement(By.CssSelector
               ("a#RemoveProductLink")).Click();
            Console.WriteLine("Removed the product from the cart");

        }


        public void UpdateProductQuantity(string qty)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[name*='_basketControltxtQty1']")));
            Driver.FindElement(By.CssSelector
               ("input[name*='_basketControltxtQty1']")).Clear();
            Driver.FindElement(By.CssSelector
               ("input[name*='_basketControltxtQty1']")).SendKeys(qty);
            Console.WriteLine("Updated product quantity");

        }

        public void ClickUpdateTotalsButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[value='Update Totals']")));
            Driver.FindElement(By.CssSelector
               ("input[value='Update Totals']")).Click();
            Console.WriteLine("Product Total Due is Updated");

        }

        public String ValidateProductLimitExceedMessage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("ul[class='list-unstyled ']>li")));
            return Driver.FindElement(By.CssSelector("ul[class='list-unstyled ']>li")).Text;
        }

        public String GetValidationMsgWhenNoProdIsAdded()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("div[role='alert']>p")));
            return Driver.FindElement(By.CssSelector
                 ("div[role='alert']>p")).Text;
        }

        public void ClickCheckOutButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input#btnchkout")));
            Driver.FindElement(By.CssSelector
               ("input#btnchkout")).Click();
            Console.WriteLine("Clicked On Check Out button");
        }

        public bool IsCheckOutButtonDisabled()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("input#btnchkout")));
            actions.Perform();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input#btnchkout")));
            if (Driver.FindElement(By.CssSelector
                ("input#btnchkout")).GetAttribute("disabled") == "true")
                return true;
            else
                return false;
        }


        public String VerifyNoProductIsAvailableInCart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("div[role='alert']>p")));
            String Alert = Driver.FindElement(By.CssSelector
               ("div[role='alert']>p")).Text;
            return Alert;
        }
    }


}
