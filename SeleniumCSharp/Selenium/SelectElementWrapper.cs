using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using WebServiceCSharp.Core;

namespace SeleniumCSharp.Selenium
{
    public class SelectElementWrapper : IWrapsElement
    {

        private SelectElement _selectElement;
        public By by { get; set; }
        public SelectElement SelectElement
        {
            get
            {
                if (_selectElement == null)
                    InitializeElement();
                return _selectElement;
            }
            set
            {
                _selectElement = value;
            }
        }

        private DriverWrapper Driver { get; set; }
        private WebElementWrapper _element;
        public IWebElement WrappedElement
        {
            get { return SelectElement.WrappedElement; }
        }

        public SelectElementWrapper(DriverWrapper driver, By by)
        {
            this._element = new WebElementWrapper(driver, by);
            this.Driver = driver;
            this.by = by;
        }

        public SelectElementWrapper(IWebElement element)
        {
            this._element = new WebElementWrapper(element);
            this.by = null;
        }

        public SelectElementWrapper(WebElementWrapper parentElement, By by)
        {
            this._element = new WebElementWrapper(parentElement, by);
            this.Driver = parentElement.Driver;
            this.by = by;
        }

        public SelectElementWrapper InitializeElement()
        {
            if (by == null)
            {
                Exception e = new Exception("The By locator is not initialized.");
                Logger.Error(e);
                throw e;
            }
            try
            {
                _selectElement = new SelectElement(_element);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            return this;
        }


        // Summary:
        //     Gets all of the selected options within the select element.
        public IList<IWebElement> AllSelectedOptions
        {
            get
            {
                try
                {
                    return SelectElement.AllSelectedOptions;
                }
                catch (StaleElementReferenceException e)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    return SelectElement.AllSelectedOptions;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    throw;
                }
            }
        }
        //
        // Summary:
        //     Gets a value indicating whether the parent element supports multiple selections.
        public bool IsMultiple
        {
            get
            {

                try
                {
                    return SelectElement.IsMultiple;
                }
                catch (StaleElementReferenceException e)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    return SelectElement.IsMultiple;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    throw;
                }

            }
        }
        //
        // Summary:
        //     Gets the list of options for the select element.
        public IList<IWebElement> Options
        {

            get
            {
                try
                {
                    return SelectElement.Options;
                }
                catch (StaleElementReferenceException e)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    return SelectElement.Options;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    throw;
                }

            }
        }
     
        //
        // Summary:
        //     Gets the selected item within the select element.
        //
        // Exceptions:
        //   OpenQA.Selenium.NoSuchElementException:
        //     Thrown if no option is selected.
        //
        // Remarks:
        //     If more than one item is selected this will return the first item.
        public WebElementWrapper SelectedOption
        {

            get
            {

                try
                {
                    return new WebElementWrapper(SelectElement.SelectedOption,Driver);
                }
                catch (StaleElementReferenceException e)
                {
                    Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                    InitializeElement();
                    return new WebElementWrapper(SelectElement.SelectedOption, Driver);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    throw;
                }

            }
        }

        //
        // Summary:
        //     Deselect the option by the index, as determined by the "index" attribute
        //     of the element.
        //
        // Parameters:
        //   index:
        //     The value of the index attribute of the option to deselect.
        public void DeselectByIndex(int index)
        {
            try
            {
                SelectElement.DeselectByIndex(index);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.DeselectByIndex(index);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }

        }
        //
        // Summary:
        //     Deselect the option by the text displayed.
        //
        // Parameters:
        //   text:
        //     The text of the option to be deselected.
        //
        // Remarks:
        //     When given "Bar" this method would deselect an option like:
        //     <option value="foo">Bar</option>
        public void DeselectByText(string text)
        {
            try
            {
                SelectElement.DeselectByText(text);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.DeselectByText(text);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        //
        // Summary:
        //     Deselect the option having value matching the specified text.
        //
        // Parameters:
        //   value:
        //     The value of the option to deselect.
        //
        // Remarks:
        //     When given "foo" this method will deselect an option like:
        //     <option value="foo">Bar</option>
        public void DeselectByValue(string value)
        {
            try
            {
                SelectElement.DeselectByValue(value);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.DeselectByValue(value);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        //
        // Summary:
        //     Select the option by the index, as determined by the "index" attribute of
        //     the element.
        //
        // Parameters:
        //   index:
        //     The value of the index attribute of the option to be selected.
        //
        // Exceptions:
        //   OpenQA.Selenium.NoSuchElementException:
        //     Thrown when no element exists with the specified index attribute.
        public void SelectByIndex(int index)
        {
            try
            {
                SelectElement.SelectByIndex(index);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.SelectByIndex(index);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        //
        // Summary:
        //     Select all options by the text displayed.
        //
        // Parameters:
        //   text:
        //     The text of the option to be selected. If an exact match is not found, this
        //     method will perform a substring match.
        //
        // Exceptions:
        //   OpenQA.Selenium.NoSuchElementException:
        //     Thrown if there is no element with the given text present.
        //
        // Remarks:
        //     When given "Bar" this method would select an option like:
        //     <option value="foo">Bar</option>
        public void SelectByText(string text)
        {
            try
            {
                SelectElement.SelectByText(text);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.SelectByText(text);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        //
        // Summary:
        //     Select an option by the value.
        //
        // Parameters:
        //   value:
        //     The value of the option to be selected.
        //
        // Exceptions:
        //   OpenQA.Selenium.NoSuchElementException:
        //     Thrown when no element with the specified value is found.
        //
        // Remarks:
        //     When given "foo" this method will select an option like:
        //     <option value="foo">Bar</option>
        public void SelectByValue(string value)
        {
            try
            {
                SelectElement.SelectByValue(value);
            }
            catch (StaleElementReferenceException e)
            {
                Logger.Info("Caught exception {0}. Attempting to re-initialize element", e.Message);
                InitializeElement();
                SelectElement.SelectByValue(value);
            }

            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
    }
}
