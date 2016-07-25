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

namespace SeleniumCSharp.Framework
{
    public class DriverUtils
    {
        [ThreadStatic]
        private static List<DriverWrapper> Drivers;

        public static TestConfiguration configuration = TestConfiguration.GetDefaultTestConfiguration();

        public static DriverWrapper GetDriver(){
            if (Drivers == null)
                Drivers = new List<DriverWrapper>();

            DriverWrapper driver = new DriverWrapper( new FirefoxDriver());
            Drivers.Add(driver);
            return driver;
        }


        public static DriverWrapper GetDriver(DesiredCapabilities capabilites)
        {

            return new DriverWrapper(new FirefoxDriver());
        }

        private static DriverWrapper CreateDriver()
        {
            DesiredCapabilities capab=null;

            switch (configuration.Browser)
            {
                case Browser.Firefox:
                    capab = DesiredCapabilities.Firefox();
                    break;
                case Browser.Chrome:
                    capab = DesiredCapabilities.Chrome();
                    break;
                case Browser.IE:
                    capab = DesiredCapabilities.InternetExplorer();
                    break;
                case Browser.Edge:
                    capab = DesiredCapabilities.Edge();
                    break;
                case Browser.Safari:
                    capab = DesiredCapabilities.Safari();
                    break;
                case Browser.Android:
                    capab = DesiredCapabilities.Android();
                    break;
                case Browser.IOS:
                    capab = DesiredCapabilities.IPad();
                    break;
                default :
                    capab = DesiredCapabilities.Firefox();
                    break;
            }

            capab.IsJavaScriptEnabled= true;
            capab.Platform = new Platform( configuration.PlatformType);
            IWebDriver driver = new RemoteWebDriver(new Uri(configuration.HubURL), capab);

            return new DriverWrapper(driver);
        }

        public static void QuitDrivers(){

            foreach (DriverWrapper driver in Drivers)
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
            Drivers = null;
        }

    }
}
