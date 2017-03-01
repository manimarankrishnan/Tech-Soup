using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharp.Framework;
using NUnit.Framework;
using Utils.Core;
using ReportAddin;
namespace SeleniumCSharp.Tests
{   
        [TestFixture]
        [Parallelizable(ParallelScope.Fixtures)]
        public class ExtentReportTest1
        {

            [SetUp]
            public void Setup()
            {
                Logger.Name = TestContext.CurrentContext.Test.FullName;
                Logger.mode = LogMode.INFO; 
                //Reports.SetUp();
                Report.StartTest(TestContext.CurrentContext.Test.FullName);
               
            }

            [Test, Property("TestId", "123")]
            public void LoginToSchoolnet()
            {
                Report.LogPass("Into Login Test");

                try
                {
                    Assert.IsTrue(1 == 1, "The values are not equal");
                }
                catch (Exception e)
                {
                    Report.LogException(e);
                    throw;
                }

            }

            [Test, Property("TestId", "123")]
            public void LoginToSchoolnet1()
            {
                Report.LogPass("Into Login Test2-2");
                try
                {
                    Assert.IsTrue(1 == 2, "The values are not equal");
                }
                catch (Exception e)
                {
                    Report.LogException(e);
                    throw;
                }

            }

            [TearDown]
            public void CleanUp()
            {
                var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
                Report.EndTest(testStatus, TestContext.CurrentContext.Test.FullName);
                DriverUtils.QuitDrivers();
            }

            [OneTimeTearDown]
            public void OneTimeStop()
            {
                Report.Flush();
            }
        }
   
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
   public class ExtentReportTest2
    {
            [SetUp]
            public void Setup()
        {
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO; 
                //Reports.setup();
                Report.StartTest(TestContext.CurrentContext.Test.FullName);
            }

            [Test, Property("TestId", "123")]
            public void LoginToSchoolnet()
            {
                Report.LogPass("Into Login Test");

                try
                {
                    Assert.IsTrue(1 == 1, "The values are not equal");
                }
                catch (Exception e)
                {
                    Report.LogException(e);
                    throw;
                }

            }

            [Test, Property("TestId", "123")]
            public void LoginToSchoolnet1()
            {
                Report.LogPass("Into Login Test2");
                try
                {
                    Assert.IsTrue(2 == 2, "The values are not equal");
                }
                catch (Exception e)
                {
                    Report.LogException(e);
                    throw;
                }

            }

            [TearDown]
            public void CleanUp()
            {
                var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
                Report.EndTest(testStatus, TestContext.CurrentContext.Test.FullName);
                DriverUtils.QuitDrivers();
            }

            [OneTimeTearDown]
            public void OneTimeStop()
            {
                Report.Flush();
            }
        }

}
