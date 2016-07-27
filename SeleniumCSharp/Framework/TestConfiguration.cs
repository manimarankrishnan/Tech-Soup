using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using WebServiceCSharp.Core;
namespace SeleniumCSharp.Framework
{
    public class TestConfiguration
    {
        //Time interval
        public static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);
        public TimeSpan ImplicitTimout { get; set; }
        public TimeSpan PageLoadTimeOut { get; set; }
        public TimeSpan JavascriptTimeout { get; set; }
        public TimeSpan ElementWaitTimeout { get; set; }
        public TimeSpan PollingInterVal { get; set; }

        //Webdriver Configuration
        public String Browser { get; set; }
        public String BrowserVersion { get; set; }
        public String Platform { get; set; }
        public String ChromeDriverFileLocation { get; set; }
        public String IEDriverFileLocation { get; set; }
        public String EdgeDriverLocation { get; set; }
        //Grid2 Config
        public String GridType { get; set; }

        //Remote WebDriver
        public String HubURL
        {
            get
            {
                return String.Format("http://{0}:{1}/wd/hub", HubHost, HubPort);
            }
        }
        public String HubHost { get; set; }
        public String HubPort { get; set; }

        //External Tools
        public String SauceLabsUserName { get; set; }
        public String SauceLabsAccessKey { get; set; }
        public String SauceURL
        {
            get
            {
                return String.Format("http://{0}:{1}@ondemand.saucelabs.com:80/wd/hub", SauceLabsUserName, SauceLabsAccessKey);
            }
        }

        public TestConfiguration()
        {
            SetDefaults();
        }


        public void SetDefaults()
        {
            ImplicitTimout = DefaultTimeOut;
            PageLoadTimeOut = DefaultTimeOut;
            JavascriptTimeout = DefaultTimeOut;
            ElementWaitTimeout = DefaultTimeOut;
            PollingInterVal = TimeSpan.FromMilliseconds(300);
            Browser = "firefox";
            BrowserVersion = "";
            Platform = "";
            ChromeDriverFileLocation = Config.IsConfigValuePresent("chromeDriverDirectory") ? Config.GetConfigValue("chromeDriverDirectory") : "";
            IEDriverFileLocation = Config.IsConfigValuePresent("IEDriverServerDirectory") ? Config.GetConfigValue("IEDriverServerDirectory") : "";
            EdgeDriverLocation = Config.IsConfigValuePresent("edgeDriverDirectory") ? Config.GetConfigValue("edgeDriverDirectory") : "";
            GridType = "local";
            HubHost = "localhost";
            HubPort = "4444";
            SauceLabsUserName = null;
            SauceLabsAccessKey = null;
        }

        public static TestConfiguration GetCurrentConfiguration()
        {
            TestConfiguration testconfig = new TestConfiguration();
            if (Config.IsConfigValuePresent("SELENIUM_BROWSER"))
                testconfig.Browser = Config.GetConfigValue("SELENIUM_BROWSER");
            if (Config.IsConfigValuePresent("SELENIUM_VERSION"))
                testconfig.BrowserVersion = Config.GetConfigValue("SELENIUM_VERSION");
            if (Config.IsConfigValuePresent("SELENIUM_PLATFORM"))
                testconfig.Platform = Config.GetConfigValue("SELENIUM_PLATFORM");
            if (Config.IsConfigValuePresent("SAUCE_USERNAME"))
                testconfig.SauceLabsUserName = Config.GetConfigValue("SAUCE_USERNAME");
            if (Config.IsConfigValuePresent("SAUCE_ACCESS_KEY"))
                testconfig.SauceLabsAccessKey = Config.GetConfigValue("SAUCE_ACCESS_KEY");
            if (Config.IsConfigValuePresent("GRID"))
                testconfig.SauceLabsAccessKey = Config.GetConfigValue("GRID");
            return testconfig;
        }
    }
}
