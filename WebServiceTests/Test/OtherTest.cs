using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using WebServiceTests.Main.Other;
using Utils.Core;

namespace WebServiceTests.Test
{
    public class OtherTest
    {
        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }






        [Test]
        public void test1()
        {
            Assert.Fail();
        }

        [Test]
        [Retry(3)]
        public void test3()
        {
            Assert.Fail();
        }
        [Test]
        public void test2()
        {
            Assert.Fail();
        }









       // [Test]
        public void TextToBraille()
        {
            WebServiceClient client = new WebServiceClient("http://www.webservicex.net", "TestCaseData_Others_TC_textToBraille");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            String r = client.ExpectedResponseBody;
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(responseBody);
            XmlDocument dd = new XmlDocument();
            dd.LoadXml(GeneralUtils.GetFileAsString(client.ExpectedResponseBody));
            Assert.AreEqual(xd, dd, "Actual and Expected response body are not eaual");
        }


        #region-----------SerializationJSON---------------------

        ///JSONPlaceHolder WebService
        ///Checks Status code of the response
       // [Test]
        [TestCase("Create", "BodyRequest", 15)]
        public void CreateResourceSerialization(string title, string body, int userID)
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_Others_TC_createResource");
            String responseBody =  client.SetRequestBody(new CreateResourceRequest(title, body, userID).ToString()).SetRequest().CallService().GetResponseBody();
            responseBody=responseBody.Replace("\n ", "");
            String statusCode = client.GetStatusCodeOfResponse();
            Assert.AreEqual(statusCode, "Created", "Status code mismatch");
            Assert.AreEqual(GeneralUtils.FormatJsonString(GeneralUtils.GetFileAsString(client.ExpectedResponseBody)), GeneralUtils.FormatJsonString(responseBody), "ResonseBody mismatch");

        }


        #endregion------------SerializationJSON-----------------------


        #region-----------DeSerializationJSON---------------------

        ///JSONPlaceHolder WebService
        [Test]
        [TestCase("Title", "BodyRequest", 15)]
        public void CreateResourceDeSerialization(string title, string body, int userID)
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_Others_TC_createResource");
            client.SetRequestBody(new CreateResourceRequest(title, body, userID).ToString()).SetRequest().CallService();

            CreateResourceResponse.Rootobject response = (CreateResourceResponse.Rootobject)client.GetResponseAsObject(typeof(CreateResourceResponse.Rootobject));
            Assert.AreEqual(response.title, title, "Title mismatch");
            Assert.AreEqual(response.body, body, "Body mismatch");
            Assert.AreEqual(response.userId, userID, "UserID mismatch");
        }
        #endregion------------DeSerializationJSON-----------------------

    }
}
        
