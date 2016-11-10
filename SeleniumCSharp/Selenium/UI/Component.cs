using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.ObjectModel;

namespace SeleniumCSharp.Selenium.UI
{
    public abstract class Component : ISearchContext, IWrapsElement
    {
        public Component()
        {

        }
        public Component(IWebElement rootElement)
        {
            WrappedElement = rootElement;
        }

        public IWebElement WrappedElement{get;set;}
        public abstract void InitElements();

        public IWebElement FindElement(By by)
        {
            return WrappedElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WrappedElement.FindElements(by);
        }
        public bool Displayed
        {
            get { return WrappedElement.Displayed; }
        }
    }

}
