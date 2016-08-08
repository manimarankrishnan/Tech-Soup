using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Selenium;
using WebServiceCSharp.Core;
namespace SeleniumCSharp.Framework
{
    public abstract class Page
    {
        public String PageName { get; set; }
        public DriverWrapper Driver { get; set; }
        public String ExpectedUrl { get; set; }
        public Form Form { get; set; }
        public Data Data { get; set; }
        public DriverCommands DriverCommands { get; set; }
        public Page(DriverWrapper driver)
        {
            this.Driver = driver;
            this.DriverCommands = new DriverCommands(driver);
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
                
                if(!actualUrl.ToLower().Contains(ExpectedUrl.ToLower())){
                    Exception e = new Exception(String.Format("The expected Url: {0} is not found in actual Url: {1}",ExpectedUrl,actualUrl));
                    Logger.Error(e);
                    throw e;
                }
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
