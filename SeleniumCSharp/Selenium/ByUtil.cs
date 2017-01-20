using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Selenium
{
   public  class ByUtil : By
    {

       private int index;
       private By by;
       private ByUtil(By by, int index)
       {
           this.by = by;
           this.index = index;
       }

       public static By NthResult(By by, int index)
       {
           return new ByUtil(by, index);
       }
       public override IWebElement FindElement(ISearchContext context)
       {
           var allElements= base.FindElements(context);
           if (allElements.Count <= index)
           {
               throw new NoSuchElementException(String.Format("The locator:{0} returned only {1} results, But needed result index was {2}.", by, allElements.Count, index));
           }
           return allElements[index];
       }

       public override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
       {
           return base.FindElements(context);
       }
    }
}
