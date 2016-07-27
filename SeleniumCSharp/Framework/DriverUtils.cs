using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium.Remote;
using WebServiceCSharp.Core;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;

namespace SeleniumCSharp.Framework
{
    public class DriverUtils
    {
        [ThreadStatic]
        private static List<DriverWrapper> _drivers;

        public static TestConfiguration currentTestConfiguration = TestConfiguration.GetCurrentConfiguration();

        public static DriverWrapper GetDriver()
        {
            return GetDriver(GetCapability(currentTestConfiguration));
        }

        public static DriverWrapper GetDriver(DesiredCapabilities capabilites)
        {
            if (_drivers == null)
                _drivers = new List<DriverWrapper>();

            DriverWrapper driver = CreateDriver(currentTestConfiguration, capabilites);
            _drivers.Add(driver);
            return driver;
        }

        public static DesiredCapabilities GetCapability(TestConfiguration configuration)
        {
            DesiredCapabilities capab = null;

            switch (configuration.Browser.ToUpper())
            {
                case "FIREFOX":
                    capab = DesiredCapabilities.Firefox();
                    break;
                case "CHROME":
                    capab = DesiredCapabilities.Chrome();
                    break;
                case "IEXPLORER":
                    capab = DesiredCapabilities.InternetExplorer();
                    break;
                case "EDGE":
                    capab = DesiredCapabilities.Edge();
                    break;
                case "SAFARI":
                    capab = DesiredCapabilities.Safari();
                    break;
                case "ANDROID":
                    capab = DesiredCapabilities.Android();
                    break;
                case "IPAD":
                    capab = DesiredCapabilities.IPad();
                    break;
                case "IPHONE":
                    capab = DesiredCapabilities.IPhone();
                    break;
                default:
                    capab = DesiredCapabilities.Firefox();
                    break;
            }

            capab.IsJavaScriptEnabled = true;
            capab.SetCapability("Version", configuration.BrowserVersion);
            capab.SetCapability("Platform", configuration.Platform);
            return capab;
        }

        private static DriverWrapper CreateDriver(TestConfiguration configuration, DesiredCapabilities capab = null)
        {
            IWebDriver driver;

            if (capab == null)
                capab = GetCapability(configuration);

            if (configuration.GridType.Equals("grid2", StringComparison.OrdinalIgnoreCase))
                driver = new RemoteWebDriver(new Uri(configuration.HubURL), capab);
            else if (configuration.GridType.Equals("saucelabs", StringComparison.OrdinalIgnoreCase))
                driver = new RemoteWebDriver(new Uri(configuration.SauceURL), capab);
            else if (configuration.GridType.Equals("local", StringComparison.OrdinalIgnoreCase))
            {
                driver = GetLocalDriver(configuration.Browser).WrappedDriver;
            }
            else
                driver = new DriverWrapper(new FirefoxDriver());
            return new DriverWrapper(driver);
        }


        private static DriverWrapper GetLocalDriver(String browser)
        {
            IWebDriver driver;
            switch (browser.ToUpper())
            {
                case "FIREFOX":
                    driver = new FirefoxDriver();
                    break;
                case "CHROME":
                    driver = new ChromeDriver(currentTestConfiguration.ChromeDriverFileLocation);
                    break;
                case "IEXPLORER":
                    driver = new InternetExplorerDriver(currentTestConfiguration.IEDriverFileLocation);
                    break;
                case "EDGE":
                    driver = new EdgeDriver(currentTestConfiguration.EdgeDriverLocation);
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            return new DriverWrapper(driver);
        }

        public static void QuitDrivers()
        {

            foreach (DriverWrapper driver in _drivers)
            {
                try
                {
                    driver.Close();
                    driver.Quit();
                }
                catch (Exception e)
                {
                    Logger.Debug(e);
                }
            }
            _drivers = null;
        }

    }
}
