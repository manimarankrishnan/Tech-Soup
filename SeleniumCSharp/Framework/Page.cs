using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Selenium;
using Utils.Core;
using OpenQA.Selenium.Support.UI;
namespace SeleniumCSharp.Framework
{
    public abstract class Page
    {
        public String PageName { get; set; }
        public DriverWrapper Driver { get; set; }
        public String ExpectedUrl { get; set; }
        public Form Form { get; set; }
        public Data Data { get; set; }
       
        public Page(DriverWrapper driver)
        {
            this.Driver = driver;
           
        }
        public virtual void InitElements()
        {
        }
        public void GoToUrl(string url)
        {
            this.Driver.Navigate().GoToUrl(url);
        }
        public void VerifyCurrentUrl()
        {
            try
            {
                string actualUrl = Driver.Url;
                var wait = new DefaultWait<DriverWrapper>(Driver);
                wait.Timeout = TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout);
                wait.PollingInterval = TimeSpan.FromMilliseconds(TestConfiguration.PollingInterVal); ;
                wait.Message = "The actual page URL: '" + actualUrl + "' does not contain the expected page URL: '" + this.ExpectedUrl + "'.";
                wait.Until((d) => { return d.Url.ToLower().Contains(ExpectedUrl.ToLower()); });

                Logger.Info("Verifed the actual URL: {0} contains the expected URL: {1}.",actualUrl,ExpectedUrl);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }


    }
}
