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
        public TimeSpan ImplicitTimout = DefaultTimeOut;
        public TimeSpan PageLoadTimeOut = DefaultTimeOut;
        public TimeSpan JavascriptTimeout = DefaultTimeOut;
        public TimeSpan PollingInterVal = TimeSpan.FromMilliseconds(300);

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
           DesiredCapabilities capab= new DesiredCapabilities();
            
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
