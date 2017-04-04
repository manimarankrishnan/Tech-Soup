using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;
using Utils.Core;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportAddin
{
    public class Report
    {
        private static ExtentReports _extentReport = SetUp();

        [ThreadStatic]
        private static ExtentTest _testName;

        public static ExtentReports ExtentReport
        {
            get
            {
                return _extentReport;
            }
            set
            {
                _extentReport = value;
            }
        }

        public static ExtentTest ExtentTest
        {
            get
            {
                return _testName;
            }
            set
            {
                _testName = value;
            }
        }

        private static ExtentReports SetUp()
        {
            String reportPath = Path.Combine(Logger.GetCurrentLogDirectory(),Config.GetConfigValue("ExtentReportFileName"));
            var _extentReport = new ExtentReports(reportPath, true);
            _extentReport.LoadConfig(Path.Combine(Config.GetCurrentProjectDllPath(), Config.GetConfigValue("ExtentReportConfigFile")));
            return (_extentReport);
        }


        public static void LogException(Exception e)
        {
            Logger.Error(e);
            ExtentTest.Log(LogStatus.Warning, e);
        }


        public static void LogPass(string message)
        {
            Logger.Debug(message);
            ExtentTest.Log(LogStatus.Pass, message);
        }

        public static void LogSkip(string message)
        {
            Logger.Debug(message);
            ExtentTest.Log(LogStatus.Skip, message);
        }

        public static void LogFail(string message)
        {
            Logger.Error(message);
            ExtentTest.Log(LogStatus.Fail, message);
        }

        public static void EndTest(Object testStatus, string TestName)
        {

            LogStatus logstatus=LogStatus.Pass;

            string status = testStatus.ToString();

            switch(status.ToLower()){
                case "fail":
                case "failed":
                    logstatus = LogStatus.Fail;
                    break;
                case "inconclusive":
                case "aborted":
                    logstatus = LogStatus.Warning;
                    break;
                case "skipped":
                    logstatus = LogStatus.Skip;
                    break;

            }


            if (testStatus is TestStatus)
            {
                TestStatus status1 = ( TestStatus)testStatus;
                switch (status1)
                {
                    case TestStatus.Failed:
                        logstatus = LogStatus.Fail;
                        LogFail(TestName + ":Failed");
                        break;
                    case TestStatus.Inconclusive:
                        logstatus = LogStatus.Warning;
                        break;
                    case TestStatus.Skipped:
                        logstatus = LogStatus.Skip;
                        break;
                    default:
                        logstatus = LogStatus.Pass;
                        break;
                }
            }
            else if (testStatus is UnitTestOutcome)
            {
                 UnitTestOutcome status1 = ( UnitTestOutcome)testStatus;
                 switch (status1)
                {
                    case UnitTestOutcome.Failed:
                        logstatus = LogStatus.Fail;
                        LogFail(TestName + ":Failed");
                        break;
                    case UnitTestOutcome.Aborted:
                        logstatus = LogStatus.Warning;
                        break;
                    case UnitTestOutcome.Inconclusive:
                        logstatus = LogStatus.Skip;
                        break;
                    default:
                        logstatus = LogStatus.Pass;
                        break;
                }
            }
            ExtentTest.Log(logstatus, TestName + " " + testStatus);
            ExtentReport.EndTest(ExtentTest);

        }


        public static void StartTest(string TestName)
        {
            ExtentTest = ExtentReport.StartTest(TestName);

        }

        public static void Flush()
        {
            ExtentReport.Flush();
        }
      
    }

   
}
