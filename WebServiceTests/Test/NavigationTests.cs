using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test
{
    public class NavigationTests
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_NavigationWithURI)using identifier
        /// </summary>
        [Test]
        public void GetCitiesByPostalCode()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_NavigationWithURI_TC_getCitiesPostalCode");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(Utils.GetFileAsString(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_NavigationWithURI)using identifier
        /// </summary>
        [Test]
        public void GetNearByCityName()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_NavigationWithURI_TC_getNearByCityName");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(responseBody, Utils.GetFileAsString(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_NavigationWithURI)using identifier
        /// </summary>
        [Test]
        public void GetCitiesByCountryName()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_NavigationWithURI_TC_getCitiesByCountry");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            //Assert.AreEqual(responseBody,client._expectedResponseBody,"Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// URI- http://api.geonames.org
        /// TC- gets from excel (TestCaseData_NavigationWithoutURI)using identifier
        /// </summary>
        [Test]
        public void GetCitiesByPostalCodeWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://api.geonames.org", "TestCaseData_NavigationWithoutURI_TC_getCitiesPostalCode");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(Utils.GetFileAsString(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// URI- http://api.geonames.org
        /// TC- gets from excel (TestCaseData_NavigationWithoutURI)using identifier
        /// </summary>
        [Test]
        public void GetNearByCityNameWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://api.geonames.org", "TestCaseData_NavigationWithoutURI_TC_getNearByCityName");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(responseBody, Utils.GetFileAsString(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://www.webserviceX.NET
        /// TC- gets from excel (TestCaseData_NavigationWithoutURI)using identifier
        /// </summary>
        [Test]
        public void GetCitiesByCountryNameWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://www.webserviceX.NET", "TestCaseData_NavigationWithoutURI_TC_getCitiesByCountry");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            //Assert.AreEqual(responseBody,client._expectedResponseBody,"Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_NavigationWithoutHeaders)using identifier
        /// </summary>
        [Test]
        public void GetCitiesByCountryNameWithoutHeader()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_NavigationWithoutHeaders_TC_getCitiesByCountry");

            String responseBody = client.SetRequest()
                .SetRequestHeaders(@"Content-Type:text/xml,SOAPAction:http://www.webserviceX.NET/GetCitiesByCountry")
                .CallService().GetResponseBody();
            //Assert.AreEqual(responseBody,client._expectedResponseBody,"Actual and Expected response body are not eaual");
        }
    }
}