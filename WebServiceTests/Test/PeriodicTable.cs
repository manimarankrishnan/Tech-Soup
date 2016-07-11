using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
namespace WebServiceTests.Test
{
    public  class PeriodicTable
    {
        [SetUp]
        public void Setup()
        {
            Logger.logWriter = TestContext.Out;
            Logger.name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        [Test]
        public void GetAtomicNumber()
        {
            WebServiceClient client = new WebServiceClient("http://www.webservicex.net", "TestCaseData_WebServicexTests_TCAtomicNumber");
            String ss = client.SetRequest().CallService().GetResponseBody();
        }

        [Test]
        public void GetCurrenctOfCountry()
        {
            WebServiceClient client2 = new WebServiceClient("http://www.webservicex.net", "TestCaseData_WebServicexTests_TCGetCurrency");
            String yes = client2.SetRequest().CallService().GetResponseBody();
        }
        [Test]
        public void GetCurrenctOfCountry1()
        {
            WebServiceClient client2 = new WebServiceClient("http://www.webservicex.net", "TestCaseData_WebServicexTests_TCGetCurrency");
            String yes = client2.SetRequest().CallService().GetResponseBody();
        }

    }
}
