using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Utils.Core
{
    public class Logger
    {

        [ThreadStatic]
        private static TextWriter _logWriter;

        public static TextWriter LogWriter
        {
            get
            {
                return _logWriter;
            }
            set
            {
                _logWriter = value;
            }
        }

        [ThreadStatic]
        private static String _name;

        [ThreadStatic]
        private static String _logFilePath;
      
        public static String Name
        {
            get
            {
                return _name;
            }
            set
            {
                StackTrace stackTrace = new StackTrace();
                String className = stackTrace.GetFrame(1).GetMethod().DeclaringType.ToString();
                _name = (value.Contains(className) ? "" : className +".") + value;
                _logFilePath = GetFilePath() +".log";
            }
        }


        public static LogMode mode = LogMode.DEBUG;
        private static readonly DateTime buildStartTime = DateTime.Now;

        public static void SetLogWriter(TextWriter writer)
        {
            LogWriter = writer;
        }

        /// <summary>
        /// Log info into the log file when log mode is Info
        /// </summary>
        /// <param name="o"></param>
        public static void Info(Object o)
        {
            if (mode == LogMode.INFO)
            {
                WriteToFile(o, "INFO" );
            }
        }

        /// <summary>
        /// Log info into the log file when log mode is Info
        /// </summary>
        /// <param name="e">Exception</param>
        public static void Info(Exception e)
        {
            Info("{0} \nInner Exception:\n{1} ", e, e.InnerException);
        }

        /// <summary>
        /// Log info into the log file when log mode is Info
        /// </summary>
        /// <param name="format">String format</param>
        /// <param name="args">arguments</param>
        public static void Info(String format, params Object[] args)
        {
            if (mode == LogMode.INFO)
            {
                if (args.Length == 0)
                    Info((Object)format);
                else
                    WriteToFile(String.Format(format, args), "INFO" );
            }
        }

        /// <summary>
        /// Log Debug into the log file when logMode is Debug or Error
        /// </summary>
        /// <param name="o"></param>
        public static void Debug(Object o)
        {

            if (mode == LogMode.DEBUG || mode == LogMode.INFO)
            {
                WriteToFile(o, "DEBUG");
            }

        }
        /// <summary>
        /// Log Debug into the log file when logMode is Debug or Error
        /// </summary>
        /// <param name="e">exception</param>
        public static void Debug(Exception e)
        {
            Debug("{0} \nInner Exception:\n{1} ", e, e.InnerException);
        }

        /// <summary>
        /// Log Debug into the log file when logMode is Debug or Error
        /// </summary>
        /// <param name="format">String format</param>
        /// <param name="args">arguments</param>
        public static void Debug(String format, params Object[] args)
        {
            if (mode == LogMode.DEBUG || mode == LogMode.INFO)
            {
                if (args.Length == 0)
                    Debug((Object)format);
                else
                    WriteToFile(String.Format(format, args), "DEBUG");
            }

        }

        /// <summary>
        /// Log Debug into the log file
        /// </summary>
        /// <param name="o"></param>
        public static void Error(Object o)
        {
            WriteToFile(o, "ERROR");
        }

        /// <summary>
        /// Log Debug into the log file
        /// </summary>
        /// <param name="e">Exception</param>
        public static void Error(Exception e)
        {
            Error("{0} \nInner Exception:\n{1} ", e, e.InnerException);
        }

        /// <summary>
        /// Log Debug into the log file
        /// </summary>
        /// <param name="format">String format</param>
        /// <param name="args">arguments</param>
        public static void Error(String format, params Object[] args)
        {
            if (args.Length == 0)
                Error((Object)format);
            else
                WriteToFile(String.Format(format, args), "ERROR");
        }

        /// <summary>
        /// Write log to the file
        /// </summary>
        /// <param name="o">object to be written</param>
        /// <param name="logmode">log mode </param>
        private static void WriteToFile(Object o, String logmode)
        {

            if (LogWriter != null)
                LogWriter.WriteLine(o);
            _logFilePath = _logFilePath ?? GetFilePath() + ".log";
            using (StreamWriter sw = new StreamWriter(_logFilePath, true))
            {
                sw.AutoFlush = true;
                sw.WriteLine(logmode + ": " + o.ToString());
            }
        }

         /// <summary>
        /// Get the file name for the current execution
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            if (Name == null)
                Name = "TestResult";
            String fileName = Name.Split('.').LastOrDefault();
            var invalids = Path.GetInvalidFileNameChars().ToList();
            invalids.AddRange(Path.GetInvalidPathChars());
            var newName = String.Join("_", fileName.Split(invalids.ToArray(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            String filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", buildStartTime.ToString("MMMMdd_yyyy_HHmmss"), Name.Replace(fileName, ""));
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, newName);
            return filePath;
        }


    }


    public enum LogMode
    {
        DEBUG,
        INFO,
        ERROR
    }
}
