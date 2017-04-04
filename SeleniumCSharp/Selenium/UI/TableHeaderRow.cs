using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
namespace SeleniumCSharp.Selenium.UI
{
    public class TableHeaderRow : TableRow 
    {

        //public override ReadOnlyCollection<string> ColumnList
        //{
        //    get {
        //        return new ReadOnlyCollection<string>( ValueList);
        //    }
        //}


        public override OpenQA.Selenium.By ByTableData
        {
            get
            {
                return By.TagName("th");
            }
        }

        
    }
}
