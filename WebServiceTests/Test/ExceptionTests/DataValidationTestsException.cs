using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using Utils.Core;

namespace WebServiceTests.Test.ExceptionTests
{
    public class DataValidationTestsException
    {

        public string errorMessage = "Call AddURLParametersToURL method before calling the SetRequestMethod";

        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/check
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyOutlookEmailWithRequestURLAsDictionaryException()
        {
            try
            {
                //Set _urlParameters in a Dictionary
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("access_key", "0af0be9cbdd897e592a3008443a29a55");
                values.Add("email", "webserviceapi@outlook.com");
                values.Add("smtp", "1");
                values.Add("format", "1");

                WebServiceClient client = new WebServiceClient("http://apilayer.net/api/check");
                client.SetRequest().AddURLParametersToURL(values);
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

            /// <summary>
        /// Base URI- http://apilayer.net/api/check
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyGmailWithRequestURLAsDictionaryException()
        {            
            try
            { 
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "0af0be9cbdd897e592a3008443a29a55");
            values.Add("email", "testing123@gmail.com");
            values.Add("smtp", "1");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/check");
            client.SetRequest().AddURLParametersToURL(values);
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/detect
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void CheckLanguageEnglishWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "cd2c60c0d993eabf34506a4bbc28f301");
            values.Add("query", "this%20is%20english");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/detect");
            client.SetRequest().AddURLParametersToURL(values);
                }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/detect
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void CheckLanguageFrenchWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "cd2c60c0d993eabf34506a4bbc28f301");
            values.Add("query", "J’ achète du pain tous l");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/detect");
            client.SetRequest().AddURLParametersToURL(values);
                }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/detect
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyVATWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "cd2c60c0d993eabf34506a4bbc28f301");
            values.Add("query", "this%20is%20english");
           
            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/detect");
            client.SetRequest().AddURLParametersToURL(values);
                }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberIndiaWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "61054062e54a845e89e158ded3ee383c");
            values.Add("number", "+919677025895");
            values.Add("country_code", "");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.SetRequest().AddURLParametersToURL(values);
                }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberBangaloreIndiaWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "61054062e54a845e89e158ded3ee383c");
            values.Add("number", "+917259688069");
            values.Add("country_code", "");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.SetRequest().AddURLParametersToURL(values);
                }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- http://apilayer.net/api/validate
        /// TC- Request will be hardcoded in TC itself using AddURLParametersToURL() method
        /// </summary>
        [Test]
        public void VerifyMobileNumberUSWithRequestURLAsDictionaryException()
        {
            try
            {
            //Set _urlParameters in a Dictionary
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("access_key", "61054062e54a845e89e158ded3ee383c");
            values.Add("number", "+14158586273");
            values.Add("country_code", "");
            values.Add("format", "1");

            WebServiceClient client = new WebServiceClient("http://apilayer.net/api/validate");
            client.SetRequest().AddURLParametersToURL(values);
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }
        }
    }