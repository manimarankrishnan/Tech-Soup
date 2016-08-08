using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Selenium;
using WebServiceCSharp.Core;
using OpenQA.Selenium;
namespace SeleniumCSharp.Tests.Main
{
    public class LoginPage : Page
    {
        private Data _dataObject;
        public Data Data
        {
            get{
                return _dataObject;
            }
            set
            {
                _dataObject = value;
                this.Form.Data = _dataObject;
            }
        }

        public LoginForm Form { get; set; }

        public LoginPage(DriverWrapper driver):base(driver)
        {
            PageName = "LoginPage";
            ExpectedUrl = "Authentication.aspx";
            VerifyCurrentUrl();
            Form = new LoginForm(Driver);
        }

        public override void InitElements()
        {
            base.InitElements();
        }

        public static LoginPage NavigateToLoginPage(DriverWrapper driver, String URL)
        {
            driver.Navigate().GoToUrl(URL);
            return new LoginPage(driver);
        }
    }

    public class LoginForm : Form
    {

        public LoginForm(DriverWrapper driver)
            : base(driver)
        {
            InitElements();
        }



        public By ByUserName { get { return By.Id("ctl02_TextBoxUsername");} }
        public By ByPassword { get { return By.Id("ctl02_TextBoxPassword");} }

        public WebElementWrapper UserName { get; protected set; }
        public WebElementWrapper Password { get; protected set; }


        public override Form InitElements()
        {
            UserName = new WebElementWrapper(Driver, ByUserName);
            Password = new WebElementWrapper(Driver, ByPassword);
            return this;
        }

        public override Form InputFormFields()
        {
            String value;
            if (Data.TryGetValue("UserName", out value))
            {
                UserName.SendKeys(value);
            }
            if (Data.TryGetValue("Password", out value))
            {
                Password.SendKeys(value);
            }
            return this;
        }

        public override Form InputFormFields(IList<string> fields)
        {
            throw new NotImplementedException();
        }

        public override Page SubmitReturnPage()
        {
            return null;
        }

        public override Page CancelReturnPage()
        {
            return new LoginPage(Driver);
        }

        public override Form VerifyFieldsExist(IList<string> fields)
        {
            return this;
        }
    }


}
