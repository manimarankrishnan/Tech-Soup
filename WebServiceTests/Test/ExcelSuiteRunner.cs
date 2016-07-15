using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using WebServiceCSharp.Core;
using System.Xml;
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
            WebServiceClient client = new WebServiceClient("", TCDatavalues);

            String response = client.SetRequest().CallService().GetResponseBody();
            Logger.Debug((Object)response);
            Console.WriteLine(response);
            String expectedValue = client._expectedResponseBody;
            if(client.Response.ContentType.Contains("xml"))
            {
                XmlDocument expectedXmlDoc= null;

                if(expectedValue.EndsWith(".xml") ){
                   expectedXmlDoc= Utils.GetFileAsXMLDocument(expectedValue);
                }
                else{
                    expectedXmlDoc = new XmlDocument();
                    expectedXmlDoc.LoadXml(expectedValue);
                }
                Assert.AreEqual(expectedXmlDoc,client.GetResponseAsXMLDocument());
            }
            else
            {
                if (expectedValue.EndsWith(".json") || expectedValue.EndsWith(".txt"))
                {
                    expectedValue = Utils.GetFileAsString(expectedValue);
                }

                //remove new line characters from response and expected
                expectedValue = expectedValue.Replace("\r\n", "\n").Replace("\n", "");
                response = response.Replace("\r\n", "\n").Replace("\n", "");

                //Verifying the values are equal
                Assert.AreEqual(expectedValue, response);
            }
           

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
                        if (string.IsNullOrEmpty(tcName))
                            continue;
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
