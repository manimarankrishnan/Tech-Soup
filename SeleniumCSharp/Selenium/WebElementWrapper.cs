using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.ObjectModel;
using WebServiceCSharp.Core;
using System.Drawing;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumCSharp.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.Selenium
{

    public class WebElementWrapper : IWebElement, ISearchContext, ILocatable, IWrapsElement
    {
        /*
        private const double DefaultTimeout = 90.0;
        public By By { get; private set; }

        private IWebElement element { get; set; }

        public IWebElement WrappedElement
        {
            get
            {
                return this.element;
            }
            set
            {
                this.element = value;
            }
        }

        public ReadOnlyCollection<IWebElement> WrappedElements { get; private set; }
        private IWebElement WaitForVisibleElement(By by, double timeout)
        {
            this.Report.Write("Wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "'.");
            IWebElement webElement;
            try
            {
                webElement = !(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)) ? new WebDriverWait((IWebDriver)this.Driver, TimeSpan.FromSeconds(timeout)).Until<IWebElement>(ExpectedConditions.ElementIsVisible(by)) : this.Driver.FindElement(by);
            }
            catch (WebDriverTimeoutException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the element by: '" + by.ToString() + "'.");
            return webElement;
        }

        private IWebElement WaitForVisibleElement(By by)
        {
            return this.WaitForVisibleElement(by, 90.0);
        }

        public WebElementWrapper Wait(double timeout)
        {
            this.element = this.WaitForVisibleElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper Wait()
        {
            this.element = this.WaitForVisibleElement(this.By);
            return this;
        }

        public WebElementWrapper WaitUntilVisible(double timeout)
        {
            this.element = this.WaitForVisibleElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper WaitUntilVisible()
        {
            this.element = this.WaitForVisibleElement(this.By);
            return this;
        }

        public WebElementWrapper WaitUntilExists(double timeout)
        {
            this.element = this.WaitForElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper WaitUntilExists()
        {
            this.element = this.WaitForElement(this.By);
            return this;
        }
         * */
        

        public By by { get; set; }

        public ISearchContext SearchContext { get; set; }

        public IWebDriver Driver
        {
            get
            {
                IWebDriver driver = null;
                ISearchContext searchContext = SearchContext;
                while (searchContext is WebElementWrapper)
                {
                    searchContext = ((WebElementWrapper)searchContext).SearchContext;
                }
                if (searchContext is IWebDriver)
                    driver = (IWebDriver)searchContext;
                return driver;
            }
        }

        public WebElementWrapper(IWebElement element)
        {
            this.WrappedElement = element;
        }

        public WebElementWrapper(IWebDriver driver, By by)
        {
            this.SearchContext = driver;
            this.by = by;
        }

        public WebElementWrapper(IWebElement parentElement, By by)
        {
            this.SearchContext = parentElement;
            this.by = by;
        }

        public WebElementWrapper(DriverWrapper driver, By by)
        {
            this.SearchContext = driver;
            this.by = by;
        }
        public WebElementWrapper(WebElementWrapper parentElementWrapper, By by)
        {
            this.SearchContext = parentElementWrapper;
            this.by = by;
        }

        private IWebElement _element;
        public IWebElement WrappedElement
        {
            get {
                if (_element == null)
                    InitializeElement();
                return _element;
            }
            set
            {
                _element = value;
            }
        }

        public WebElementWrapper InitializeElement()
        {
            Wait(TestConfiguration.ElementWaitTimeout, TestConfiguration.PollingInterVal);
            return this;
        }

        public ICoordinates Coordinates
        {
            get { return ((ILocatable)WrappedElement).Coordinates; }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get { return ((ILocatable)WrappedElement).LocationOnScreenOnceScrolledIntoView; }
        }

        public IWebElement FindElement(By by)
        {
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(WrappedElement);
            List<By> locators = new List<By> { by };
            return  retryElementLocator.LocateElement(locators);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(WrappedElement);
            List<By> locators = new List<By> { by };
            return retryElementLocator.LocateElements(locators);
        }

        public void Clear()
        {
            try
            {
                WrappedElement.Clear();
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element",e);
                InitializeElement();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
            
        }

        public void Click()
        {
            try
            {
                WrappedElement.Click();
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
         
        }

        public bool Displayed
        {
            get { return WrappedElement.Displayed; }
        }

        public bool Enabled
        {
            get { return WrappedElement.Enabled; }
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return WrappedElement.GetCssValue(propertyName);
        }

        public Point Location
        {
            get { return WrappedElement.Location; }
        }

        public bool Selected
        {
            get { return WrappedElement.Selected; }
        }

        public void SendKeys(string text)
        {
            try
            {
                WrappedElement.SendKeys(text);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
           
        }

        public Size Size
        {
            get { return WrappedElement.Size; }
        }

        public void Submit()
        {
            try
            {
                WrappedElement.Submit();
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
        }

        public string TagName
        {
            get { return WrappedElement.TagName; }
        }

        public string Text
        {
            get { return WrappedElement.Text; }
        }

        public void Check()
        {
            if (!Selected)
                Click();
        }

        public void UnCheck()
        {
            if (Selected)
                Click();
        }

        public WebElementWrapper Wait(long timeOutInSeconds,long pollingIntervalInMilliSeconds = 500)
        {
            if (by == null)
            {
                Exception e = new Exception("The By is not initialized.");
                Logger.Error(e);
                throw e;
            }
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext, TimeSpan.FromSeconds(timeOutInSeconds),TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds));
            List<By> locators = new List<By> { by };
            WrappedElement = retryElementLocator.LocateElement(locators); 
            return this;
        }

        public ReadOnlyCollection<IWebElement> WaitForElements(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {

            if (by == null)
            {
                Exception e = new Exception("The By is not initialized.");
                Logger.Error(e);
                throw e;
            }
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext, TimeSpan.FromSeconds(timeOutInSeconds), TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds));
            List<By> locators = new List<By> { by };
            return retryElementLocator.LocateElements(locators);
        }


        public WebElementWrapper WaitToBeVisible(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {
            if (by == null)
            {
                Exception e = new Exception("The By is not initialized.");
                Logger.Error(e);
                throw e;
            }
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds);
            WrappedElement = wait.Until(ExpectedConditions.ElementIsVisible(by));
            return this;
            
        }

        public WebElementWrapper ScrollToView()
        {           
            if (Driver != null)
                ((IJavaScriptExecutor)Driver).ExecuteScript(JavaScripts.ScrollToElement, WrappedElement);
            return this;
        }

        public WebElementWrapper MoveToElement()
        {
            try
            {
                new Actions((IWebDriver)this.Driver).MoveToElement(WrappedElement).Click().Build().Perform();
            }
            catch (WebDriverException ex)
            {
                Logger.Error(ex);
            }
            return this;
        }
    }
}
