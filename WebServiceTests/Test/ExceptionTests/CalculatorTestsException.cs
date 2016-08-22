using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using WebServiceTests.Main.Calculator.Addition;
using WebServiceTests.Main.Calculator.Subtraction;
using WebServiceTests.Main.Calculator.Multiplication;
using WebServiceTests.Main.Calculator.Deletion;
using Utils.Core;

namespace WebServiceTests.Test.ExceptionTests
{
    public class CalculatorTestsException
    {
        public string errorMessage = "Call the SetRequestBody method before calling SetRequest method.";
        
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
        /// Call the SetRequestBody method before calling SetRequest method.
        /// </summary>
        [Test]
        [TestCase(6, 2)]
        [TestCase(5, 3)]
        public void PerformAdditionWithoutRequestBodyException(int a, int b)
        {
            string value = Config.GetConfigValue("BaseEndPointUrl");

            try
            {
                WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Add_Object");
                client.SetRequest().SetRequestBody(new AdditionRequest(a, b).ToString());
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message,errorMessage);
            }
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// Call the SetRequestBody method before calling SetRequest method.
        /// </summary>
        [Test]
        [TestCase(6, 2)]
        [TestCase(5, 3)]
        public void PerformSubtractionWithoutRequestBodyException(int a, int b)
        {
     
            try
            {
                WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Add_Object");
                client.SetRequest().SetRequestBody(new AdditionRequest(a, b).ToString());
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// Call the SetRequestBody method before calling SetRequest method.
        /// </summary>
        [Test]
        [TestCase(6, 2)]
        [TestCase(5, 3)]
        public void PerformMultiplicationWithoutRequestBodyException(int a, int b)
        {
            try
            {
                WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Add_Object");
                client.SetRequest().SetRequestBody(new AdditionRequest(a, b).ToString());
            }

            catch (Exception e)
            {
                Logger.Debug(e);

                Assert.AreEqual(e.Message, errorMessage);
            }
        }

        /// <summary>
        /// Base URI- empty,gets from excel
        /// TC- gets from excel (TestCaseData_CalculatorTestsWithURI)using identifier
        /// Call the SetRequestBody method before calling SetRequest method.
        /// </summary>
        [Test]
        [TestCase(6, 2)]
        [TestCase(5, 3)]
        public void PerformDeletionWithoutRequestBodyException(int a, int b)
        {
            try
            {
                WebServiceClient client = new WebServiceClient("", "TestCaseData_CalculatorTestsWithURI_TC_Add_Object");
                client.SetRequest().SetRequestBody(new AdditionRequest(a, b).ToString());
            }

            catch (Exception e)
            {
                Assert.AreEqual(e.Message, errorMessage);
            }
        }
    }
}