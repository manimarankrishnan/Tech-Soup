using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using WebServiceTests.Main.Other;
namespace WebServiceTests.Test
{
    public class OtherTest
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        [Test]
        public void TextToBraille()
        {
            WebServiceClient client = new WebServiceClient("http://www.webservicex.net", "TestCaseData_Others_TC_textToBraille");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            String r = client._expectedResponseBody;
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(responseBody);
            XmlDocument dd = new XmlDocument();
            dd.LoadXml(Utils.GetFileAsString(client._expectedResponseBody));
            Assert.AreEqual(xd, dd, "Actual and Expected response body are not eaual");
        }


        #region-----------SerializationJSON---------------------

        ///JSONPlaceHolder WebService
        ///Checks Status code of the response
        [Test]
        [TestCase("Create", "BodyRequest", 15)]
        public void CreateResourceSerialization(string title, string body, int userID)
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_Others_TC_createResource");
            String responseBody =  client.SetRequestBody(new CreateResourceRequest(title, body, userID).ToString()).SetRequest().CallService().GetResponseBody();
            responseBody=responseBody.Replace("\n ", "");
            String statusCode = client.GetStatusCodeOfResponse();
            Assert.AreEqual(statusCode, "Created", "Status code mismatch");
            Assert.AreEqual(Utils.FormatJsonString(Utils.GetFileAsString(client._expectedResponseBody)), Utils.FormatJsonString(responseBody), "ResonseBody mismatch");

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


        #region-----------DeSerializationJSON---------------------

        ///JSONPlaceHolder WebServicek
        [Test]
        [TestCase("Title", "BodyRequest", 15)]
        public void CreateResourceUsingResponseValues(string title, string body, int userID)
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_Others_TC_createResource");
            client.SetRequestBody(new CreateResourceRequest(title, body, userID).ToString()).SetRequest().CallService();

            //Deserialize
            CreateResourceResponse.Rootobject response = (CreateResourceResponse.Rootobject)client.GetResponseAsObject(typeof(CreateResourceResponse.Rootobject));

            WebServiceClient clientNew = new WebServiceClient("", "TestCaseData_Others_TC_createResource");
            clientNew.SetRequestBody(new CreateResourceRequest(response.title, response.body, response.userId).ToString()).SetRequest().CallService();
        }
        #endregion------------DeSerializationJSON-----------------------

    }
}
        
