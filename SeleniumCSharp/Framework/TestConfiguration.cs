using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
namespace SeleniumCSharp.Framework
{
    public class TestConfiguration
    {
        //Time interval
        public static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);
        public static TimeSpan ImplicitTimout = DefaultTimeOut;
        public static TimeSpan PageLoadTimeOut = DefaultTimeOut;
        public static TimeSpan JavascriptTimeout = DefaultTimeOut;
        public static TimeSpan ElementWaitTimeout = DefaultTimeOut;
        public static TimeSpan PollingInterVal = TimeSpan.FromMilliseconds(300);

        //Webdriver Configuration
        public Browser Browser= Browser.Firefox;
        public String BrowserVersion = null;
        public PlatformType PlatformType = PlatformType.Windows;
        public String ChromeDriverFileLocation = null;
        public String IEDriverFileLocation = null;

        //Remote WebDriver
        public String HubURL="http://localhost:444/wd/hub";
        public String HubHost = "localhost";
        public String HubPort = "4444";
    
        //External Tools
        public String SauceLabsUserName = null;
        public String SauceLabsAccessKey = null;

        
        public static TestConfiguration GetDefaultTestConfiguration()
        {
            DesiredCapabilities capab = DesiredCapabilities.Firefox();

            
            return new TestConfiguration();
        }

    }


    public enum Browser{
        Firefox,
        Chrome,
        Safari,
        IE,
        Android,
        IOS,
        Edge
    }

    
}
