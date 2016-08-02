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
                case "INTERNET EXPLORER":
                case "INTERNET":
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
            capab.SetCapability("version", configuration.BrowserVersion);
            capab.SetCapability("platform", configuration.Platform);
            capab.SetCapability("build", configuration.BuildName);
            return capab;
        }

        private static DriverWrapper CreateDriver(TestConfiguration configuration, DesiredCapabilities capab)
        {
            DriverWrapper driver;
            try
            {
                if (configuration.GridType.Equals("grid2", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Info("Initalising remote driver for the Grid hub {0} \n Capabilities: {1}", configuration.HubURL, capab);
                    driver = new DriverWrapper(new RemoteWebDriver(new Uri(configuration.HubURL), capab));
                }
                else if (configuration.GridType.Equals("saucelabs", StringComparison.OrdinalIgnoreCase))
                {
                    capab.SetCapability("username", configuration.SauceLabsUserName);
                    capab.SetCapability("accessKey", configuration.SauceLabsAccessKey);
                    capab.SetCapability("name", Logger.name);
                    Logger.Info("Initalising remote driver for the sauce {0} \n Capabilities: {1}", configuration.SauceURL, capab);
                    driver = new DriverWrapper(new RemoteWebDriver(new Uri(configuration.SauceURL), capab));
                    Logger.Info("SauceOnDemandSessionID={0} job-name={1}", ((DriverWrapper)driver).GetSessionId(), Logger.name);
                }
                else if (configuration.GridType.Equals("local", StringComparison.OrdinalIgnoreCase))
                {
                    driver = GetLocalDriver(configuration.Browser);
                }
                else
                {
                    Logger.Info("Initalising local Firefox Browser");
                    driver = new DriverWrapper(new FirefoxDriver());
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }

            return driver;
        }


        private static DriverWrapper GetLocalDriver(String browser)
        {
            IWebDriver driver;
            switch (browser.ToUpper())
            {
                case "FIREFOX":
                    Logger.Info("Initalising local Firefox Browser");
                    driver = new FirefoxDriver();
                    break;
                case "CHROME":
                    Logger.Info("Initalising local Chrome Browser");
                    driver = new ChromeDriver(currentTestConfiguration.ChromeDriverFileLocation);
                    break;
                case "IEXPLORER":
                case "INTERNET EXPLORER":
                case "INTERNET":
                    Logger.Info("Initalising local Internet Explorer Browser");
                    driver = new InternetExplorerDriver(currentTestConfiguration.IEDriverFileLocation);
                    break;
                case "EDGE":
                    Logger.Info("Initalising local Edge Browser");
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
            if (_drivers == null)
                return;
            Logger.Info("Number of drivers : {0}", _drivers.Count);
            foreach (DriverWrapper driver in _drivers)
            {
                Logger.Info("Attempting to close Driver number {0}", _drivers.IndexOf(driver) + 1);
                try
                {
                    try
                    {
                        driver.Close();
                    }
                    catch { }                    
                    driver.Quit();
                    Logger.Info("Closed Driver number {0}", _drivers.IndexOf(driver) + 1);
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
