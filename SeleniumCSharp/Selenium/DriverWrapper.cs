using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using WebServiceCSharp.Core;


namespace SeleniumCSharp.Selenium
{

    public class DriverWrapper : IWebDriver, ISearchContext, IDisposable, IJavaScriptExecutor, IHasInputDevices, ITakesScreenshot, IWrapsDriver
    {
        private IWebDriver driver;

        public IWebDriver WrappedDriver
        {
            get
            {               
                return ((EventFiringWebDriver)this.driver).WrappedDriver;
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
                    return (Screenshot)null;
                }
                catch (Exception ex2)
                {
                   Logger.Debug("Failed to take a screen shot...");
                    return (Screenshot)null;
                }
            }
        }
    }
}
