using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Core;
using OpenQA.Selenium;
using SeleniumCSharp.Selenium;
namespace SeleniumCSharp.Framework
{
   public  abstract class Form
    {
        public virtual Data DataObject { get; set; }
        public DriverWrapper Driver { get; set; }
        public Form(DriverWrapper driver)
        {
            this.Driver = driver;
        }
        public Data Data { get; set; }
        public virtual By BySubmit { get { return By.CssSelector("[type=submit]"); } }
        public virtual By ByCancel { get { return By.CssSelector("[type=cancel]"); } }
        public WebElementWrapper Submit { get { return new WebElementWrapper(Driver, BySubmit); } }
        public WebElementWrapper Cancel { get { return new WebElementWrapper(Driver, ByCancel); } }
        public Page SubmitForm()
        {
            Submit.Wait(TestConfiguration.ElementWaitTimeout).Click();
            return SubmitLandPage();
        }
        public Page CancelForm()
        {
            Cancel.Wait(TestConfiguration.ElementWaitTimeout).Click();
            return SubmitLandPage();
        }
        public Page EnterAndSubmit()
        {
            EnterFieldValues();
            return SubmitForm();
        }

        public abstract Form Init();
        public abstract Form EnterFieldValues();
        public abstract Form EnterFieldValues(IList<String> fields);
        public abstract Page SubmitLandPage();
        public abstract Page CancelLandPage();
        public abstract Form VerifyFields(IList<String> fields);


    }
}
