using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Selenium.UI;
namespace SeleniumCSharp.Tests.Main.Components
{
    public class TestCentralRow : TableRow
    {


        //public override System.Collections.ObjectModel.ReadOnlyCollection<string> ColumnList
        //{
        //    get {
        //    return ExpectedColumnList;
        //    }

        //}
        //public static System.Collections.ObjectModel.ReadOnlyCollection<string> ExpectedColumnList{get{
        //        return new System.Collections.ObjectModel.ReadOnlyCollection<string>(

        //            new List<string>(){"Test Name",	
        //                                "Test Category",	
        //                                "Subject",	
        //                                "Grade Level",	
        //                                "Test Stage",	
        //                                "Start Date",	
        //                                "End Date"}

        //            );

        //}
        //}
    }



    public class MagagzineRow : TableRow
    {
        
        //public override System.Collections.ObjectModel.ReadOnlyCollection<string> ColumnList
        //{
        //    get {
        //    return ExpectedColumnList;
        //    }

        //}
        public static System.Collections.ObjectModel.ReadOnlyCollection<string> ExpectedColumnList{get{
                return new System.Collections.ObjectModel.ReadOnlyCollection<string>(

                    new List<string>(){"Company",	"Q1",	"Q2",	"Q3",	"Q4"}

                    );

        }
        }


        
    }



    public class DataTableExample : TableRow
    {
        
        //public override System.Collections.ObjectModel.ReadOnlyCollection<string> ColumnList
        //{
        //    get {
        //    return ExpectedColumnList;
        //    }

        //}
        //public static System.Collections.ObjectModel.ReadOnlyCollection<string> ExpectedColumnList{get{
        //        return new System.Collections.ObjectModel.ReadOnlyCollection<string>(

        //            new List<string>(){"Name",	"Position",	"Start" ,"date",	"Salary"}

        //            );

        //}
        //}


        
    }





}
