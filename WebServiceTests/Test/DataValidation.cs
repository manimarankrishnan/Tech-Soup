using System;
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
        #region -----Start-of-DataValidationTests----------
        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// Check status code of the response
        /// </summary>
        [Test]
        public void VerifyOutlookEmail()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyOutlookEmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");

            String statusCode = client.GetStatusCodeOfResponse();
            Assert.AreEqual(statusCode, "OK", "Status code mismatch");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyGmail()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyGmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageEnglish()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_CheckLanguageEnglish");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageFrench()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_CheckLanguageFrench");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyVAT()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyVat");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberIndia()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyMobileNumberIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberBangaloreIndia()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyMobileNumberBangaloreIndia");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- empty
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyMobileNumberUS()
        {
            WebServiceClient client = new WebServiceClient("", "TestCaseData_DataValidationTestsWithURI_TC_verifyMobileNumberUS");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        #endregion -----End-of-DataValidationTests----------

        #region -----Start-of-DataValidationTestsWithURI----------

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyOutlookEmailWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationTestsWithoutURI_TC_verifyOutlookEmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void VerifyGmailWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationTestsWithoutURI_TC_verifyGmail");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageEnglishWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationTestsWithoutURI_TC_CheckLanguageEnglish");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        /// <summary>
        /// Base URI- http://apilayer.net
        /// TC- gets from excel using identifier
        /// </summary>
        [Test]
        public void CheckLanguageFrenchWithURI()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net", "TestCaseData_DataValidationTestsWithoutURI_TC_CheckLanguageFrench");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
            Assert.AreEqual(client._expectedResponseBody, responseBody, "Actual and Expected response body are not equal");
        }

        #endregion -----End-of-DataValidationTestsWithURI----------


        #region ---------------------Start-Below Test Methods are used to check the method AddURLParametersToURL(string keyValue)


        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberIndiaWithRequestURL()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.AddURLParametersToURL(@"access_key=61054062e54a845e89e158ded3ee383c&number=+919677025895&country_code=&format=1");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberBangaloreIndiaWithRequestURL()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.AddURLParametersToURL(@"access_key=61054062e54a845e89e158ded3ee383c&number=+917259688069&country_code=&format=1");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberUSWithRequestURL()
        {
            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.AddURLParametersToURL(@"access_key=61054062e54a845e89e158ded3ee383c&number=+14158586273&country_code=&format=1");
            String responseBody = client.SetRequest().CallService().GetResponseBody();
        }


        #endregion---------------------End-Below Test Methods are used to check the method AddURLParametersToURL(string keyValue)



        #region----------------Start-Below Test Methods are used to check the method AddURLParametersToURL(Dictionary<String,String> parametersValue)


        /// <summary>
        /// Base URI- http://apilayer.net/api/check
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyOutlookEmailWithRequestURLAsDictionary()
        {
            //Set _urlParameters in a Dictionary
            Dictionary<string,string> values = new Dictionary<string,string>();
            values.Add("access_key","0af0be9cbdd897e592a3008443a29a55");
            values.Add("email", "webserviceapi@outlook.com");
            values.Add("smtp", "1");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/check");
            client.AddURLParametersToURL(values);
            String responseBody = client.SetRequest().CallService().GetResponseBody();
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/check
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyGmailWithRequestURLAsDictionary()
        {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "0af0be9cbdd897e592a3008443a29a55");
            values.Add("email", "testing123@gmail.com");
            values.Add("smtp", "1");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/check");
            client.AddURLParametersToURL(values);
            String responseBody = client.SetRequest().CallService().GetResponseBody();
        }


        #endregion-------------------End-Below Test Methods are used to check the method AddURLParametersToURL(Dictionary<String,String> parametersValue)



    }
}