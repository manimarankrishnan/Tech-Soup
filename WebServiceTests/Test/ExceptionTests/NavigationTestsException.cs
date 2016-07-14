using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test.ExceptionTests
{
    public class NavigationTestsException
    {
        public string errorMessage = "Call AddHeaders method before calling the SetRequestMethod";
        
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_NavigationTestsWithoutHeaders)using identifier
        /// Request Header will be set using SetRequestHeaders method
        /// </summary>
        [Test]
        public void GetCitiesByCountryNameWithoutHeaderException()
        {
            try
            {
                WebServiceClient client = new WebServiceClient("", "TestCaseData_NavigationTestsWithoutHeaders_TC_getCitiesByCountry");
                client.SetRequest().AddHeaders("");
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message,errorMessage);
            }
        }
    }
}