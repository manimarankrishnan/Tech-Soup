using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
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
            var componentList = new List<T>();
            foreach (var element in searchContext.FindElements(By))
            {
                var component = new T();
                component.RootElement = element;
                component.InitElements();
                componentList.Add(component);
            }
            return componentList;
        }
    }
}
