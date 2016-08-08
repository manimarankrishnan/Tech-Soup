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
            get
            {
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
            get
            {
                IWebElement element = WrappedElement;
                while (element is IWrapsElement)
                {
                    element = ((IWrapsElement)element).WrappedElement;
                }
                return ((ILocatable)element).Coordinates;
            }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get
            {
                IWebElement element = WrappedElement;
                while (element is IWrapsElement)
                {
                    element = ((IWrapsElement)element).WrappedElement;
                }
                return ((ILocatable)element).LocationOnScreenOnceScrolledIntoView;
            }
        }

        public IWebElement FindElement(By by)
        {
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(WrappedElement, TimeSpan.FromSeconds(TestConfiguration.ElementWaitTimeout), TimeSpan.FromMilliseconds(TestConfiguration.PollingInterVal));
            List<By> locators = new List<By> { by };
            try
            {
                return retryElementLocator.LocateElement(locators);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
                return retryElementLocator.LocateElement(locators);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
          
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            ReadOnlyCollection<IWebElement> result;
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(WrappedElement, TimeSpan.FromSeconds(TestConfiguration.ElementWaitTimeout), TimeSpan.FromMilliseconds(TestConfiguration.PollingInterVal));
            List<By> locators = new List<By> { by };
            try
            {
                result = retryElementLocator.LocateElements(locators);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
                result = retryElementLocator.LocateElements(locators);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            Logger.Info("Found {0} element using the locator {1} in webelement {2} ", result.Count, by, WrappedElement);
            return result;
        }

        public void Clear()
        {
            try
            {
                WrappedElement.Clear();
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
                WrappedElement.Clear();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }
            Logger.Info("Cleared the element : {0}", WrappedElement);
        }
        

        /// <summary>
        /// Click a webelement
        /// </summary>
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
                WrappedElement.Click();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }
            Logger.Info("Clicked the element : {0}", WrappedElement);

        }

        public bool Displayed
        {
            get { return WrappedElement.Displayed; }
        }

        public bool Enabled
        {
            get { return WrappedElement.Enabled; }
        }

        /// <summary>
        /// Get an attribute's value in the webelement
        /// </summary>
        /// <param name="attributeName">attribute name</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        /// <summary>
        /// Get the Css property value of the webelement
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
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

        /// <summary>
        /// Send keys to the WebElement
        /// </summary>
        /// <param name="text">Text to be entered</param>
        public void SendKeys(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                Logger.Debug("Recived null or empty string for sendkeys, returning without proceeding.");
                return;
            }
            try
            {
                WrappedElement.SendKeys(text);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Error("Caught exception {0}. Attempting to re-initialize element", e);
                InitializeElement();
                WrappedElement.SendKeys(text); 
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }
            Logger.Info("Entered text {0} in the webelement : {1}",text, WrappedElement);

        }

        /// <summary>
        /// Size of the webelement
        /// </summary>
        public Size Size
        {
            get { return WrappedElement.Size; }
        }

        /// <summary>
        /// Submit a webelement
        /// </summary>
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
                throw ;
            }
        }

        /// <summary>
        /// Get the tag name of the Webelement
        /// </summary>
        public string TagName
        {
            get { return WrappedElement.TagName; }
        }


        /// <summary>
        /// Get the text inside the webelement
        /// </summary>
        public string Text
        {
            get { return WrappedElement.Text; }
        }

        /// <summary>
        /// Select the webelement
        /// </summary>
        public void Check()
        {
            if (!Selected)
                Click();
        }

        /// <summary>
        /// Un Select the webelement
        /// </summary>
        public void UnCheck()
        {
            if (Selected)
                Click();
        }

        /// <summary>
        /// Wait for element to be found using the By Locator
        /// </summary>
        /// <param name="timeOutInSeconds">Timeout period in seconds</param>
        /// <param name="pollingIntervalInMilliSeconds">Polling interval in milliseconds</param>
        /// <returns></returns>
        public WebElementWrapper Wait(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {
            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }
            try
            {
                RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext, TimeSpan.FromSeconds(timeOutInSeconds), TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds));
                List<By> locators = new List<By> { by };
                WrappedElement = retryElementLocator.LocateElement(locators);
                Logger.Info("Found element using locator: {0}", by);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
           return this;
        }

        /// <summary>
        /// Gets the elements found using the By locator 
        /// </summary>
        /// <param name="timeOutInSeconds">Timeout period in seconds</param>
        /// <param name="pollingIntervalInMilliSeconds">Polling interval in milliseconds</param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> WaitForElements(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {

            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext, TimeSpan.FromSeconds(timeOutInSeconds), TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds));
            List<By> locators = new List<By> { by };
            var elements = retryElementLocator.LocateElements(locators);
            Logger.Info("Found {0} elements using locator {1}", elements.Count, by);
            return elements;
        }

        /// <summary>
        /// Wait for element to be visible
        /// </summary>
        /// <param name="timeOutInSeconds">Time out period in seconds.</param>
        /// <param name="pollingIntervalInMilliSeconds"> Polling interval in milliseconds</param>
        /// <returns></returns>
        public WebElementWrapper WaitToBeVisible(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {
            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliSeconds);
                WrappedElement = wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
           
            return this;

        }

        /// <summary>
        /// Scroll into the view of an element.
        /// </summary>
        /// <returns></returns>
        public WebElementWrapper ScrollToView()
        {
             if (Driver == null){
                  Exception e = new Exception ("Driver is null");
                    Logger.Error(e);
                    throw e;
             }
            try
            {
               ((IJavaScriptExecutor)Driver).ExecuteScript(JavaScripts.ScrollIntoView, WrappedElement);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
           
            return this;
        }

        /// <summary>
        /// Move to a Webelement
        /// </summary>
        /// <returns></returns>
        public WebElementWrapper MoveToElement()
        {
            if (Driver == null)
            {
                Exception e = new Exception("Driver is null");
                Logger.Error(e);
                throw e;
            }
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

        /// <summary>
        /// Gets the Webelement inside multiple levels of Wrapper classes.
        /// </summary>
        /// <returns></returns>
        public IWebElement GetInnerMostWrapedElement()
        {
            IWebElement element = WrappedElement;
            while (element is IWrapsElement)
            {
                element = ((IWrapsElement)element).WrappedElement;
            }
            return element;
        }
    }
}
