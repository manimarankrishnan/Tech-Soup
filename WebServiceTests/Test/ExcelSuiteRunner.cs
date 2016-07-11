using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test
{

    public class ExcelSuiteRunner
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }


        //Test with multiple parameters - Data From Excel using Identifier
        [Test, TestCaseSource(typeof(MyFactoryClass), "TestCases")]
        public void LoginDataFromExcelUsingIdentifierTest(String[] TCDatavalues)
        {
            var client = new WebServiceClient("", TCDatavalues);

            var response = client.SetRequest().CallService().GetResponseBody();
            Logger.Debug(response);
            Console.WriteLine(response);
            String expectedValue = client._expectedResponseBody;
            if (expectedValue.EndsWith(".xml") || expectedValue.EndsWith(".json") || expectedValue.EndsWith(".txt"))
            {
                expectedValue = Utils.GetFileAsString(expectedValue);
            }
            Assert.AreEqual(expectedValue.Replace("\r\n", "\n"), response.Replace("\r\n", "\n"));



        }


        public class MyFactoryClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    List<String[]> TestCases = new List<string[]>();

                    foreach (String[] moduleSheet in Utils.GetDataFromExcel(Config.GetConfigValue("FunctionalSuiteFile"), 1))
                    {
                        TestCases.AddRange(Utils.GetDataFromExcel(moduleSheet[0]));
                    }
                    foreach (var testcaseData in TestCases)
                    {
                        String tcName = testcaseData[0];
                        Object TCData = testcaseData.Skip(1).ToArray();
                        yield return new TestCaseData(TCData)
                            .SetName(tcName)
                     .SetDescription("Test case executed");
                    }


                }
            }
        }

    }
}
