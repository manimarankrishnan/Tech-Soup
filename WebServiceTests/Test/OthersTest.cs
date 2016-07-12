﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test
{
    public class OthersTest
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
            XmlDocument x = new XmlDocument();
            x.LoadXml(r);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(responseBody);
            Assert.AreEqual(client._expectedResponseBody, xd, "Actual and Expected response body are not eaual");
        }       

    }
}
