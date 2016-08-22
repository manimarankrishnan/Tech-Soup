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
        /// <summary>
        /// Default Timeout value in seconds
        /// </summary>
        public static long DefaultTimeOut = 60;
        /// <summary>
        /// Implicit Timeout in seconds
        /// </summary>
        public static long ImplicitTimeout { get; set; }
        /// <summary>
        /// Page load Timeout in seconds
        /// </summary>
        public static long PageLoadTimeout { get; set; }
        /// <summary>
        /// Javascript Timeout in seconds
        /// </summary>
        public static long JavascriptTimeout { get; set; }
        /// <summary>
        ///Element Wait Timeout in seconds
        /// </summary>
        public static long ElementWaitTimeout { get; set; }
        /// <summary>
        /// Polling Interval in Milliseconds
        /// </summary>
        public static long PollingInterVal { get; set; }

        //Webdriver Configuration
        public String Browser { get; set; }
        public String BrowserVersion { get; set; }
        public String Platform { get; set; }
        public String ChromeDriverFileLocation { get; set; }
        public String IEDriverFileLocation { get; set; }
        public String EdgeDriverLocation { get; set; }
        //Grid2 Config
        public String GridType { get; set; }


        public String BuildName
        {
            get
            {
                return JobName + "__" + BuildNumber;
            }
        }
        public String JobName { get; set; }
        public String BuildNumber { get; set; }
        

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

        static TestConfiguration()
        {
            ImplicitTimeout = 5;
            PageLoadTimeout = DefaultTimeOut;
            JavascriptTimeout = DefaultTimeOut;
            ElementWaitTimeout = DefaultTimeOut;
            PollingInterVal = 300;

            if (Config.IsConfigValuePresent("ImplicitTimeout"))
                ImplicitTimeout = long.Parse(Config.GetConfigValue("ImplicitTimeout"));
            if (Config.IsConfigValuePresent("PageLoadTimeout"))
                PageLoadTimeout = long.Parse(Config.GetConfigValue("PageLoadTimeout"));
            if (Config.IsConfigValuePresent("JavascriptTimeout"))
                JavascriptTimeout = long.Parse(Config.GetConfigValue("JavascriptTimeout"));
            if (Config.IsConfigValuePresent("ElementWaitTimeout"))
                ElementWaitTimeout = long.Parse(Config.GetConfigValue("ElementWaitTimeout"));
            if (Config.IsConfigValuePresent("PollingInterVal"))
                PollingInterVal = long.Parse(Config.GetConfigValue("PollingInterVal"));
        }

        public TestConfiguration()
        {
            SetDefaults();
        }


        public void SetDefaults()
        { 
            Browser = "firefox";
            BrowserVersion = "";
            Platform = "";
            ChromeDriverFileLocation = Config.IsConfigValuePresent("chromeDriverDirectory") ? Config.GetConfigValue("chromeDriverDirectory") : "";
            IEDriverFileLocation = Config.IsConfigValuePresent("IEDriverServerDirectory") ? Config.GetConfigValue("IEDriverServerDirectory") : "";
            EdgeDriverLocation = Config.IsConfigValuePresent("edgeDriverDirectory") ? Config.GetConfigValue("edgeDriverDirectory") : "";
            GridType = "local";
            HubHost = "localhost";
            HubPort = "4444";
            Config.SetConfigValue("HubURL", HubURL);
            SauceLabsUserName = "";
            SauceLabsAccessKey = "";
            Config.SetConfigValue("SauceURL", SauceURL);
            BuildNumber = "";
            JobName = "";
            Config.SetConfigValue("BuildName", BuildName);

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
            if (Config.IsConfigValuePresent("SELENIUM_HOST"))
                testconfig.HubHost = Config.GetConfigValue("SELENIUM_HOST");
            if (Config.IsConfigValuePresent("SELENIUM_PORT"))
                testconfig.HubPort = Config.GetConfigValue("SELENIUM_PORT");
            if (Config.IsConfigValuePresent("SAUCE_USERNAME"))
                testconfig.SauceLabsUserName = Config.GetConfigValue("SAUCE_USERNAME");
            if (Config.IsConfigValuePresent("SAUCE_ACCESS_KEY"))
                testconfig.SauceLabsAccessKey = Config.GetConfigValue("SAUCE_ACCESS_KEY");
            if (Config.IsConfigValuePresent("GRID"))
                testconfig.GridType = Config.GetConfigValue("GRID");
            if (Config.IsConfigValuePresent("JOB_NAME"))
                testconfig.JobName = Config.GetConfigValue("JOB_NAME");
            if (Config.IsConfigValuePresent("BUILD_NUMBER"))
                testconfig.BuildNumber = Config.GetConfigValue("BUILD_NUMBER");

            Logger.Debug("SELENIUM_BROWSER : {0} \n"+
                            "SELENIUM_VERSION : {1} \n"+
                            "SELENIUM_PLATFORM : {2} \n"+
                            "SELENIUM_HOST : {3} \n" +
                            "SELENIUM_PORT : {4} \n" +
                            "SAUCE_USERNAME : {5} \n"+
                            "SAUCE_ACCESS_KEY : {6} \n"+
                            "GRID : {7} \n"+
                            "JOB_NAME : {8} \n" +
                            "BUILD_NUMBER : {9} \n", testconfig.Browser, testconfig.BrowserVersion, testconfig.Platform,testconfig.HubHost,testconfig.HubPort, testconfig.SauceLabsUserName, testconfig.SauceLabsAccessKey, testconfig.GridType, testconfig.JobName, testconfig.BuildNumber);

            return testconfig;
        }
    }
}
