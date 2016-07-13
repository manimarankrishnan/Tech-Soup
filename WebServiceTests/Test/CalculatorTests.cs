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
    public class CalculatorTests
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
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformAddition()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Add");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformSubtraction()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Sub");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformMultiplication()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Mul");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformDivision()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Div");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }




        /// <summary>
        /// Base URI- http://www.dneonline.com
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithoutURI)using identifier
        /// </summary>
        [Test]
        public void PerformAdditionWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://www.dneonline.com", "TestCaseData_CalculatorTestsWithoutURI_TC_Add");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://www.dneonline.com
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithoutURI)using identifier
        /// </summary>
        [Test]
        public void PerformSubtractionWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://www.dneonline.com", "TestCaseData_CalculatorTestsWithoutURI_TC_Sub");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://www.dneonline.com
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithoutURI)using identifier
        /// </summary>
        [Test]
        public void PerformMultiplicationWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://www.dneonline.com", "TestCaseData_CalculatorTestsWithoutURI_TC_Mul");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://www.dneonline.com
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithoutURI)using identifier
        /// </summary>
        [Test]
        public void PerformDivisionWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://www.dneonline.com", "TestCaseData_CalculatorTestsWithoutURI_TC_Div");
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }






        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData__CalculatorTestsWithoutHeaders)using identifier
        /// Request Header will be set using SetRequestHeaders method
        /// </summary>
        [Test]
        public void PerformAdditionWithoutHeaders()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithoutHeaders_TC_Add");
            XmlDocument responseBody = client
                .AddHeaders(@"Content-Type:text/xmlContent-Type:text/xml;charset=utf-8,SOAPAction:http://tempuri.org/Add")
                .SetRequest()
                .CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(responseBody, Utils.GetFileAsXMLDocument(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData__CalculatorTestsWithoutHeaders)using identifier
        /// Request Header will be set using SetRequestHeaders method
        /// </summary>
        [Test]
        public void PerformSubtractionWithoutHeaders()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithoutHeaders_TC_Sub");
            XmlDocument responseBody = client
                .AddHeaders(@"Content-Type:text/xmlContent-Type:text/xml;charset=utf-8,SOAPAction:http://tempuri.org/Subtract")
                .SetRequest()
                .CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(responseBody, Utils.GetFileAsXMLDocument(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData__CalculatorTestsWithoutHeaders)using identifier
        /// Request Header will be set using SetRequestHeaders method
        /// </summary>
        [Test]
        public void PerformMultiplicationWithoutHeaders()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithoutHeaders_TC_Mul");
            XmlDocument responseBody = client
                .AddHeaders(@"Content-Type:text/xmlContent-Type:text/xml;charset=utf-8,SOAPAction:http://tempuri.org/Multiply")
                .SetRequest()
                .CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(responseBody, Utils.GetFileAsXMLDocument(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData__CalculatorTestsWithoutHeaders)using identifier
        /// Request Header will be set using SetRequestHeaders method
        /// </summary>
        [Test]
        public void PerformDivisionWithoutHeaders()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithoutHeaders_TC_Div");
            XmlDocument responseBody = client
                .AddHeaders(@"Content-Type:text/xmlContent-Type:text/xml;charset=utf-8,SOAPAction:http://tempuri.org/Divide")
                .SetRequest()
                .CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(responseBody, Utils.GetFileAsXMLDocument(client._expectedResponseBody), "Actual and Expected response body are not eaual");
        }






        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformAdditionWithRawData()
        {
            String[] values = Utils.GetDataFromExcel("TestCaseData_CalculatorTestsWithURI_TC_Add").First();
            WebServiceClient client = new WebServiceClient("", values);
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformSubtractionWithRawData()
        {
            String[] values = Utils.GetDataFromExcel("TestCaseData_CalculatorTestsWithURI_TC_Sub").First();
            WebServiceClient client = new WebServiceClient("", values);
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformMultiplicationWithRawData()
        {
            String[] values = Utils.GetDataFromExcel("TestCaseData_CalculatorTestsWithURI_TC_Mul").First();
            WebServiceClient client = new WebServiceClient("", values);
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// </summary>
        [Test]
        public void PerformDivisionWithRawData()
        {
            String[] values = Utils.GetDataFromExcel("TestCaseData_CalculatorTestsWithURI_TC_Div").First();
            WebServiceClient client = new WebServiceClient("", values);
            XmlDocument responseBody = client.SetRequest().CallService().GetResponseAsXMLDocument();
            Assert.AreEqual(Utils.GetFileAsXMLDocument(client._expectedResponseBody), responseBody, "Actual and Expected response body are not eaual");
        }
    }
}
