using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using WebServiceCSharp.Core;
using System.Xml;
using Utils.Core;
namespace WebServiceTests.Test
{

    public class ExcelSuiteRunner
    {
        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }


        //Test with multiple parameters - Data From Excel using Identifier
        [Test, TestCaseSource(typeof(MyFactoryClass), "TestCases")]
        public void LoginDataFromExcelUsingIdentifierTest(Data TCDatavalues)
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
                   expectedXmlDoc= GeneralUtils.GetFileAsXMLDocument(expectedValue);
                }
                else{
                    expectedXmlDoc = new XmlDocument();
                    expectedXmlDoc.LoadXml(expectedValue);
                }
                Assert.AreEqual(expectedXmlDoc,client.GetResponseAsXMLDocument());
            }

            else if (client.Response.ContentType.Contains("json"))
            {
                String expectedJsonString = null;

                if (expectedValue.EndsWith(".json") || expectedValue.EndsWith(".txt"))
                {
                    expectedJsonString = GeneralUtils.FormatJsonString(GeneralUtils.GetFileAsString(expectedValue));
                }
                else
                {
                    expectedJsonString = GeneralUtils.FormatJsonString(expectedValue);
                }
                Assert.AreEqual(expectedJsonString, GeneralUtils.FormatJsonString(client.GetResponseBody()));
            }
            else
            {
                if (expectedValue.EndsWith(".txt"))
                {
                    expectedValue = GeneralUtils.GetFileAsString(expectedValue);
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
                    List<Data> TestCases = new List<Data>();

                    foreach (String[] moduleSheet in GeneralUtils.GetDataFromExcel(Config.GetConfigValue("FunctionalSuiteFile"), 1))
                    {
                        if(!String.IsNullOrEmpty(moduleSheet[1]))
                            TestCases.AddRange(GeneralUtils.GetExcelValueAsDataList(moduleSheet[1]));
                    }
                    foreach (var testcaseData in TestCases)
                    {
                        String tcName = testcaseData.GetValue("ID");
                        if (string.IsNullOrEmpty(tcName) || tcName.Equals("ID"))
                            continue;
                        yield return new TestCaseData(testcaseData)
                            .SetName(tcName)
                     .SetDescription("Test case executed");
                    }


                }
            }
        }

    }
}
