using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
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
        public void GetCurrenctOfCountry()
        {
            WebServiceClient client2 = new WebServiceClient("http://www.webservicex.net", "TestCaseData_WebServicexTests_TCGetCurrency");
            String yes = client2.SetRequest().CallService().GetResponseBody();
        }

        [Test]
        public void GetAtomicNumber()
        {
            WebServiceClient client = new WebServiceClient("http://www.webservicex.net", "TestCaseData_WebServicexTests_TCAtomicNumber");
            String ss = client.SetRequest().CallService().GetResponseBody();
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
            Assert.AreEqual(xd,dd, "Actual and Expected response body are not eaual");
        }       
    }
}
