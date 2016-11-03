using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using SeleniumCSharp.Framework;
namespace SeleniumCSharp.Selenium
{
    public class ComponentCollection<T> : ReadOnlyCollection<T> where T : Component, new()
    {
        public ComponentCollection(ISearchContext searchContext, By by)
            : base(GetListOfComponents(searchContext, by))
        {

        }

        private static List<T> GetListOfComponents(ISearchContext searchContext, By By)
        {

            ReadOnlyCollection<IWebElement> listOfElements;
            if(searchContext is IWebElement)
            {
                listOfElements = new WebElementWrapper(searchContext as IWebElement, By).WaitForElements(5);
            }
            else if(searchContext is IWebDriver)
            {
                listOfElements = new WebElementWrapper(searchContext as IWebDriver, By).WaitForElements(TestConfiguration.ElementWaitTimeout);
            }
            else
            {
                listOfElements = searchContext.FindElements(By);
            }
            var componentList = new List<T>();
            foreach (var element in listOfElements)
            {
                var component = new T();
                component.WrappedElement = element;
                component.InitElements();
                componentList.Add(component);
            }
            return componentList;
        }
    }
}
