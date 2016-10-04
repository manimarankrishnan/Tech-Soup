using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Selenium.UI;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumCSharp.Tests.Main.Components
{
    public class CategoriesRow : TableRow
    {


        public override ReadOnlyCollection<string> ColumnList
        {
            get { return ExpectedColumnList; }
        }

        public static ReadOnlyCollection<string> ExpectedColumnList
        {
            get
            {
                return new ReadOnlyCollection<string>(

                    new List<string>() { "","Show",	"Category Name" }

                    );

            }
        }




    }



    public class CategoriesTable : Table<CategoriesRow,TableHeaderRow>
    {

        public CategoriesTable(IWebDriver driver)
            : base(driver, By.XPath("//div[@id='divCategoryList']//table"))
        {

        }
        
    }

    



}
