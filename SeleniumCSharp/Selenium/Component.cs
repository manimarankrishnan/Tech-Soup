using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumCSharp.Selenium
{
    public abstract class Component
    {
        public Component()
        {

        }
        public Component(IWebElement rootElement)
        {
            RootElement = rootElement;
        }

        public IWebElement RootElement;
        public abstract void InitElements();

    }
}
