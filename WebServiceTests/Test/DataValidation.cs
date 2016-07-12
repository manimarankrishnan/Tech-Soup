﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test
{
    public class DataValidation
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyOutlookEmail()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyOutlookEmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyGmail()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyGmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageEnglish()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_CheckLanguageEnglish");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageFrench()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_CheckLanguageFrench");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyVAT()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyVat");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberIndia()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyMobileNumberIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberBangaloreIndia()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyMobileNumberBangaloreIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberUS()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationWithURI_TC_verifyMobileNumberUS");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }




        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyOutlookEmailWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyOutlookEmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyGmailWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyGmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageEnglishWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_CheckLanguageEnglish");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageFrenchWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_CheckLanguageFrench");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyVATWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyVat");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberIndiaWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyMobileNumberIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberBangaloreIndiaWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyMobileNumberBangaloreIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberUSWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationWithoutURI_TC_verifyMobileNumberUS");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not eaual");
        }
    }
}