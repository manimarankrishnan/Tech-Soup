using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.ObjectModel;
using Utils.Core;
using System.Drawing;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumCSharp.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace SeleniumCSharp.Selenium
{

    public class WebElementWrapper : IWebElement, ISearchContext, ILocatable, IWrapsElement
    {
        public By by { get; set; }
        public ISearchContext SearchContext { get; set; }

        private DriverWrapper _driver;
        public DriverWrapper Driver
        {
            get
            {
                if (_driver != null)
                    return _driver;
                ISearchContext searchContext = SearchContext;
                while (searchContext is WebElementWrapper && _driver==null)
                {
                    _driver = ((WebElementWrapper)searchContext).Driver ?? _driver;
                    searchContext = ((WebElementWrapper)searchContext).SearchContext;
                }
                if (searchContext is DriverWrapper  && _driver==null)
                {
                    _driver = (DriverWrapper)searchContext;
                }
                if (searchContext is IWebDriver && _driver == null)
                    _driver= new DriverWrapper((IWebDriver)searchContext);

                return _driver;
            }
            set
            {
                _driver = value;
            }
        }

        public WebElementWrapper(IWebElement element, DriverWrapper driver = null)
        {
            this.WrappedElement = element;
            this._driver = driver;
        }

        public WebElementWrapper(IWebDriver driver, By by)
        {
            this._driver = new DriverWrapper(driver);
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
            this._driver = driver;
            this.SearchContext = driver;
            this.by = by;
        }
        public WebElementWrapper(WebElementWrapper parentElementWrapper, By by)
        {
            this._driver = parentElementWrapper.Driver;
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

        private TimeSpan ElementWaitTimeout = TimeSpan.FromSeconds(TestConfiguration.ElementWaitTimeout);
        private TimeSpan PollingInterval = TimeSpan.FromMilliseconds(TestConfiguration.PollingInterVal);

        public IWebElement FindElement(By by)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IWebElement result; ;

            var element = WrappedElement;
            while (element is IWrapsElement)
                element = ((IWrapsElement)element).WrappedElement;
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(element, ElementWaitTimeout, PollingInterval);
            List<By> locators = new List<By> { by };
            try
            {
                result = new WebElementWrapper(retryElementLocator.LocateElement(locators));

            }
            catch (StaleElementReferenceException e)
            {
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    retryElementLocator = new RetryingElementLocator(WrappedElement, ElementWaitTimeout, PollingInterval);
                    result = new WebElementWrapper(retryElementLocator.LocateElement(locators));

                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            watch.Stop();
            Logger.Info("Found element using the locator {0} in webelement {1}. Time Taken - {2} milliseconds ", by, this,watch.ElapsedMilliseconds);
            return result;

        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ReadOnlyCollection<IWebElement> result;
            RetryingElementLocator retryElementLocator = new RetryingElementLocator(WrappedElement,ElementWaitTimeout,PollingInterval);
            List<By> locators = new List<By> { by };
            try
            {
                result = retryElementLocator.LocateElements(locators);
            }
            catch (StaleElementReferenceException e)
            {
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    retryElementLocator = new RetryingElementLocator(WrappedElement, ElementWaitTimeout, PollingInterval);
                    result = retryElementLocator.LocateElements(locators);
                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            watch.Stop();
            Logger.Info("Found {0} elements using the locator {1} in webelement {2} . Time Taken - {3} milliseconds ", result.Count, by, this,watch.ElapsedMilliseconds);

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
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    WrappedElement.Clear();
                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            Logger.Info("Cleared the element : {0}", by);
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
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    WrappedElement.Click();
                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            Logger.Info("Clicked the element : {0}", by);

        }

        public bool Displayed
        {
            get {
                var displayed = WrappedElement.Displayed;
                Logger.Info("Element {0} is {1} displayed.", by, displayed ? "" : "not");
                return displayed;
            
            
            }
        }

        public bool Enabled
        {
            get {
                var enabled = WrappedElement.Enabled;
                Logger.Info("Element {0} is {1} enabled.", by, enabled ? "" : "not");
                return enabled; 
            
            }
        }

        /// <summary>
        /// Get an attribute's value in the webelement
        /// </summary>
        /// <param name="attributeName">attribute name</param>
        /// <returns></returns>
        public string GetAttribute(string attributeName)
        {
            var attr = WrappedElement.GetAttribute(attributeName);
            Logger.Info("Returned attribute: '{0}' Value: '{1}' from element {2} .",attributeName,attr, by);
            return attr ;
        }

        /// <summary>
        /// Get the Css property value of the webelement
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
        public string GetCssValue(string propertyName)
        {
            var cssProp = WrappedElement.GetCssValue(propertyName);
            Logger.Info("Returned CSS property: '{0}' Value: '{1}' from element {2} .", propertyName, cssProp, by);
            return cssProp;
        }

        /// <summary>
        /// Location of the webelement on the screen
        /// </summary>
        public Point Location
        {
            get {
                var location = WrappedElement.Location;
                Logger.Info("Returned Location X: '{0}' Y: '{1}' of element {2} .", location.X, location.Y, by);
                return location ; 
            }
        }

        public bool Selected
        {
            get
            {
                var selected = WrappedElement.Selected;
                Logger.Info("Element {0} is {1} selected.", by, selected ? "" : "not");
                return selected;
            }
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
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    WrappedElement.SendKeys(text);
                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            Logger.Info("Entered text '{0}' in the webelement : {1}", text, by);

        }

        /// <summary>
        /// Size of the webelement
        /// </summary>
        public Size Size
        {
            get
            {
                var size = WrappedElement.Size;
                Logger.Info("Returned Location Height: '{0}' Width: '{1}' of element {2} .", size.Height, size.Width, by);
                return size;
            }
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
                if (Driver != null && by != null)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    WrappedElement.Submit();
                }
                else
                    throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            Logger.Info("Entered Submit in the webelement : {0}",  by);

        }

        /// <summary>
        /// Get the tag name of the Webelement
        /// </summary>
        public string TagName
        {
            get
            {
                var tagname = WrappedElement.TagName;
                Logger.Info("Returned tagname '{0}' for the webelement : {1}", tagname, by);
                return tagname;
            }
        }


        /// <summary>
        /// Get the text inside the webelement
        /// </summary>
        public string Text
        {
            get {
                var text = WrappedElement.Text;
                Logger.Info("Returned text '{0}' from the webelement : {1}", text, by);
                return text; 
            }
        }

        /// <summary>
        /// Select the webelement
        /// </summary>
        public void Check()
        {
            if (!Selected)
                Click();
            if (!Selected)
            {
                var e = new Exception (String.Format("Element {0} is clicked. But its not selected.", this));
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Un Select the webelement
        /// </summary>
        public void UnCheck()
        {
            if (Selected)
                Click();
            if (Selected)
            {
                var e = new Exception(String.Format("Element {0} is clicked. But its not unselected.", this));
                Logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Wait for element to be found using the By Locator
        /// </summary>
        /// <param name="timeOutInSeconds">Timeout period in seconds</param>
        /// <param name="pollingIntervalInMilliSeconds">Polling interval in milliseconds</param>
        /// <returns></returns>
        public WebElementWrapper Wait(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }
            try
            {
                RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext,ElementWaitTimeout,PollingInterval);
                List<By> locators = new List<By> { by };
                WrappedElement = retryElementLocator.LocateElement(locators);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            watch.Stop();
            if (SearchContext is IWebDriver)
                Logger.Info("Found element using locator: {0} in {1}. Time Taken - {2} milliseconds ", by, SearchContext, watch.ElapsedMilliseconds);
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
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }

            RetryingElementLocator retryElementLocator = new RetryingElementLocator(SearchContext,ElementWaitTimeout,PollingInterval);
            List<By> locators = new List<By> { by };
            var elements = retryElementLocator.LocateElements(locators);
            watch.Stop();
            if(SearchContext is IWebDriver)
                Logger.Info("Found {0} elements using locator {1} in {2}. Time Taken - {3} milliseconds. ", elements.Count, by, SearchContext,watch.ElapsedMilliseconds);
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
        /// Wait for element to be clickable
        /// </summary>
        /// <param name="timeOutInSeconds">Time out period in seconds.</param>
        /// <param name="pollingIntervalInMilliSeconds"> Polling interval in milliseconds</param>
        /// <returns></returns>
        public WebElementWrapper WaitToBeClickable(long timeOutInSeconds, long pollingIntervalInMilliSeconds = 500)
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
                WrappedElement = wait.Until(ExpectedConditions.ElementToBeClickable(by));
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
            if (Driver == null)
            {
                Exception e = new Exception("Driver is null");
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
                new Actions((IWebDriver)this.Driver).MoveToElement(WrappedElement).Build().Perform();
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

        private WebElementWrapper _parentElement;
        /// <summary>
        /// Parent element of the WebELement got using locator By.XPath(".//..")
        /// </summary>
        public WebElementWrapper ParentElement
        {
            get
            {
                _parentElement = _parentElement ??  new WebElementWrapper(this, By.XPath(".//..")); ;
                return _parentElement;
            }
        }

        private WebElementWrapper _immediateChild;
        /// <summary>
        /// Immediate inside the WebELement got using locator By.XPath("./*")
        /// </summary>
        public WebElementWrapper ImmediateChild
        {
            get
            {
                _immediateChild = _immediateChild ?? new WebElementWrapper(this, By.XPath("./*"));
                return _immediateChild; 
            }
        }

        private WebElementWrapper _descendantLink;
        /// <summary>
        /// First Link Descendant inside the WebELement got using locator By.TagName("a")
        /// </summary>
        public WebElementWrapper DescendantLink
        {
            get
            {
                _descendantLink = _descendantLink ?? new WebElementWrapper(this, By.TagName("a"));
                return _descendantLink;
            }
        }

        private WebElementWrapper _descendantCheckbox;
        /// <summary>
        /// First Checkbox Descendant inside the WebELement got using locator By.CssSelector("input[type='checkbox']")
        /// </summary>
        public WebElementWrapper DescendantCheckbox
        {
            get
            {
                _descendantCheckbox = _descendantCheckbox ?? new WebElementWrapper(this, By.CssSelector("input[type='checkbox']"));
                return _descendantCheckbox;
            }
        }

        private WebElementWrapper _descendantRadioButton;
        /// <summary>
        /// First Radio Button Descendant inside the WebELement got using locator By.CssSelector("input[type='radio']"
        /// </summary>
        public WebElementWrapper DescendantRadioButton
        {
            get
            {
                _descendantRadioButton = _descendantRadioButton ?? new WebElementWrapper(this, By.CssSelector("input[type='radio']"));
                return _descendantRadioButton; 
            }
        }
        
        private SelectElementWrapper _descendantSelect;
        /// <summary>
        /// First SelectElement Descendant inside the WebELement got using locator By.TagName("select")
        /// </summary>
        public SelectElementWrapper DescendantSelect
        {
            get
            {
                _descendantSelect = _descendantSelect ?? new SelectElementWrapper(this, By.TagName("select"));
                return _descendantSelect; 
            }
        }

        private WebElementWrapper _descendantTextBox;
        /// <summary>
        /// First Textbox Descendant inside the WebELement got using locator By.CssSelector("input[type='text']")
        /// </summary>
        public WebElementWrapper DescendantTextBox
        {
            get
            {
                _descendantTextBox = _descendantTextBox ?? new WebElementWrapper(this, By.CssSelector("input[type='text']"));
                return _descendantTextBox; 
            }
        }

        private WebElementWrapper _descendantTextArea;
        /// <summary>
        /// First Textarea Descendant inside the WebELement got using locator By.TagName("textarea")
        /// </summary>
        public WebElementWrapper DescendantTextArea
        {
            get
            {
                _descendantTextArea=_descendantTextArea??new WebElementWrapper(this, By.TagName("textarea"));
                return _descendantTextArea;
            }
        }
        
        private WebElementWrapper _descendantImage;
        /// <summary>
        /// First Image Descendant inside the WebELement got using locator  By.TagName("img")
        /// </summary>
        public WebElementWrapper DescendantImage
        {
            get
            {
                _descendantImage = _descendantImage ?? new WebElementWrapper(this, By.TagName("img"));
                return _descendantImage;
            }
        }

        /// <summary>
        /// Value in the 'value' attribute of the element
        /// </summary>
        public String Value
        {
            get
            {
                return  GetAttribute("value");
            }
        }

        /// <summary>
        /// Value in the name attribute of the element
        /// </summary>
        public String Name
        {
            get
            {
                return GetAttribute("name");
            }
        }

        /// <summary>
        /// Value in the id attribute of the element
        /// </summary>
        public String Id
        {
            get
            {
                return GetAttribute("id");
            }
        }

        /// <summary>
        /// Got fromt the method GetCssValue("color")
        /// </summary>
        public String Color
        {
            get
            {
                return WrappedElement.GetCssValue("color");
            }
        }

        /// <summary>
        /// Got fromt the method GetAttribute("class")
        /// </summary>
        public String Class
        {
            get
            {
                return WrappedElement.GetAttribute("class");
            }
        }

        /// <summary>
        /// Got fromt the method GetCssValue("background-color")
        /// </summary>
        public String BackgroundColor
        {
            get
            {
                return WrappedElement.GetCssValue("background-color");
            }
        }
        public override string ToString()
        {
            if (by != null)
                return by.ToString();
            else
            {
                try
                {
                    return TagName;
                }
                catch
                {
                    return "WebElementWrapper";
                }
            }
        }
    }
}
