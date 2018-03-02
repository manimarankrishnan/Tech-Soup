using System;
using SeleniumCSharp.Selenium;
using SeleniumCSharp.Framework;
using OpenQA.Selenium;
using System.Threading;
using ReportAddin;

namespace SeleniumCSharp.Tests
{
    public class LoginPage : Page
    {
        public LoginPage(DriverWrapper driver) : base(driver)
        {

        }

        public void Login(string uName, string pwd)
        {

          //  Driver.Manage().Window.Maximize();
            Driver.FindElement(By.Id("SignInID")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.CssSelector("input[id*='_UserName']")).SendKeys(uName);
            Driver.FindElement(By.CssSelector("input[id*='_Password']")).SendKeys(pwd);
            Report.LogPass("Entered user name and password");
            Driver.FindElement(By.CssSelector("input[id*='_login_Login']")).Click();
            Console.WriteLine(Driver.Title);
        }
    }
}
