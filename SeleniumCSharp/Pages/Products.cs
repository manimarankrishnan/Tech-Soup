using System;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.Pages
{
    public class Products : Page


    {
        public Products(DriverWrapper Driver) : base(Driver)
        {

        }

        public string SelectProductFromSearchResults(string proType)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            if(proType.Equals("1"))
            {
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                               ("img[src*='prod-adobe-creative-cloud-photography-logo_Medium.png']")));
                string productName = Driver.FindElement(By.CssSelector
                   ("img[src*='prod-adobe-creative-cloud-photography-logo_Medium.png']")).GetAttribute("alt");
                Driver.FindElement(By.CssSelector
                    ("img[src*='prod-adobe-creative-cloud-photography-logo_Medium.png']")).Click();
                Console.WriteLine("Selected Adobe Product");
                return productName;
            }
            else
            {
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector
               ("img[src*='prod-adobe-elements-photoshop-premiere-15_Medium.png']")));
                string productName = Driver.FindElement(By.CssSelector
                   ("img[src*='prod-adobe-elements-photoshop-premiere-15_Medium.png']")).GetAttribute("alt");
                Driver.FindElement(By.CssSelector
                    ("img[src*='prod-adobe-elements-photoshop-premiere-15_Medium.png']")).Click();
                Console.WriteLine("Selected PhotoShop Product");
                return productName;
            }
           
            

        }

        public void EnterProductQuantity(String qty)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_QuantityBox']")));
            Driver.FindElement(By.CssSelector
               ("input[id*='_QuantityBox']")).Clear();
            Driver.FindElement(By.CssSelector
              ("input[id*='_QuantityBox']")).SendKeys(qty);
        }

        public void ClickAddToCartButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_AddToCartButton']")));
            Driver.FindElement(By.CssSelector
               ("input[id*='_AddToCartButton']")).Click();
            Console.WriteLine("Clicked on Add To Cart button");

        }
    }

}
