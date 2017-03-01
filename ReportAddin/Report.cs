using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;
using Utils.Core;
using System.IO;

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
            ExtentTest.Log(LogStatus.Warning, e);
        }


        public static void LogPass(string message)
        {
            ExtentTest.Log(LogStatus.Pass, message);
        }

        public static void LogSkip(string message)
        {
            ExtentTest.Log(LogStatus.Skip, message);
        }

        public static void LogFail(string message)
        {
            ExtentTest.Log(LogStatus.Fail, message);
        }

        public static void EndTest(TestStatus testStatus, string TestName)
        {

            LogStatus logstatus;
            switch (testStatus)
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
