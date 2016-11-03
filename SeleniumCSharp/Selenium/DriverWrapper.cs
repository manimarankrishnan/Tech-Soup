using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.Events;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using Utils.Core;
using OpenQA.Selenium.Remote;
using System.Drawing.Imaging;

namespace SeleniumCSharp.Selenium
{

    public class DriverWrapper : IWebDriver, IJavaScriptExecutor, IHasInputDevices, ITakesScreenshot, IWrapsDriver
    {
        private IWebDriver driver;

        public IWebDriver WrappedDriver
        {
            get
            {
                return driver;
            }
        }

        public IKeyboard Keyboard
        {
            get
            {
                return ((IHasInputDevices)this.driver).Keyboard;
            }
        }

        public IMouse Mouse
        {
            get
            {
                return ((IHasInputDevices)this.driver).Mouse;
            }
        }

        public string Url
        {
            get
            {
                return this.driver.Url;
            }
            set
            {
                this.driver.Url = value;
            }
        }

        public string CurrentWindowHandle
        {
            get
            {
                return this.driver.CurrentWindowHandle;
            }
        }

        public string Title
        {
            get
            {
                return this.driver.Title;
            }
        }

        public string PageSource
        {
            get
            {
                return this.driver.PageSource;
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return this.driver.WindowHandles;
            }
        }

        public DriverWrapper(IWebDriver driver)
        {
            this.driver = driver;
            while (this.driver is DriverWrapper)
            {
                this.driver = (driver as DriverWrapper).WrappedDriver;
            }
        }

        [DebuggerStepThrough]
        public object ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor)this.driver).ExecuteScript(script, args);
        }

        [DebuggerStepThrough]
        public object ExecuteAsyncScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor)this.driver).ExecuteAsyncScript(script, args);
        }

        public IWebElement FindElement(By by)
        {
            return this.driver.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.driver.FindElements(by);
        }

        [DebuggerStepThrough]
        public void Close()
        {
            this.driver.Close();
        }

        public void Quit()
        {
            this.driver.Quit();
        }

        public void Dispose()
        {
            this.driver.Dispose();
        }

        public ITargetLocator SwitchTo()
        {
            return this.driver.SwitchTo();
        }

        [DebuggerStepThrough]
        public IOptions Manage()
        {
            return this.driver.Manage();
        }

        [DebuggerStepThrough]
        public INavigation Navigate()
        {
            return this.driver.Navigate();
        }

        public String GetSessionId()
        {
            return CustomRemoteDriver.GetSessionId(WrappedDriver);            
        }

        public Screenshot GetScreenshot()
        {
            try
            {
                return ((ITakesScreenshot)this.driver).GetScreenshot();
            }
            catch (Exception ex1)
            {
                try
                {
                    ITakesScreenshot takesScreenshot = this.driver as ITakesScreenshot;
                    if (takesScreenshot != null)
                        return takesScreenshot.GetScreenshot();
                    Logger.Debug("Failed to take a screen shot...");
                    Logger.Debug(ex1);
                    return (Screenshot)null;
                }
                catch (Exception ex2)
                {
                    Logger.Debug("Failed to take a screen shot...");
                    Logger.Debug(ex2);
                    return (Screenshot)null;
                }
            }
        }

        public IWebDriver GetInnermostDriver()
        {
            IWebDriver driver = this.driver;
            while (driver is IWrapsDriver)
            {
                driver = ((IWrapsDriver)driver).WrappedDriver;
            }                
            return driver; 
        }


        public void SaveScreenshot()
        {
            try
            {
                String fileName = Logger.GetFilePath() + Utils.Core.GeneralUtils.GetUniqueNumber() + ".jpg";
                GetScreenshot().SaveAsFile(fileName, ImageFormat.Jpeg);
                Logger.Debug("Screenshot saved at file://{0}", fileName.Replace('\\', '/'));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void SavePageSource()
        {
            try
            {
                String fileName = Logger.GetFilePath() + Utils.Core.GeneralUtils.GetUniqueNumber() + ".html";
                using (var writer = new System.IO.StreamWriter(fileName))
                {
                    writer.Write(PageSource);
                }
                Logger.Debug("HTML Page Source Saved at file://{0}", fileName.Replace('\\', '/'));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void SaveScreenshotAndPageSource()
        {
            SaveScreenshot();
            SavePageSource();
        }


        public override string ToString()
        {
            return "Page : "+Title;
        }
       class CustomRemoteDriver :RemoteWebDriver
        {
           //Dummy Constructor
           private CustomRemoteDriver()
               : base(DesiredCapabilities.Firefox())
           {

           }
            public static  String GetSessionId(IWebDriver driver)
            {
                
                if (driver is RemoteWebDriver)
                    return ((RemoteWebDriver)driver).SessionId.ToString();
                return null;
            }

        }
        
    }
}
