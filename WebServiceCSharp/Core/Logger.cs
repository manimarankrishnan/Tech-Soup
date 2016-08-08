using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WebServiceCSharp.Core
{
    public  class Logger
    {

        [ThreadStatic]
        public static TextWriter logWriter;
        [ThreadStatic]
        public static String name;

        private static String filePath { get; set; }
        public static LogMode mode=LogMode.DEBUG;
        private static DateTime buildStartTime = DateTime.Now;

        public static void SetLogWriter(TextWriter writer)
        {
            logWriter = writer;
        }

        /// <summary>
        /// Log info into the log file when log mode is Info
        /// </summary>
        /// <param name="o"></param>
        public static void Info(Object o)
        {
            if (mode == LogMode.INFO)
            {
               WriteToFile(o,"INFO");
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
        public static void Info( String format, params Object[] args)
        {
            if (mode == LogMode.INFO)
            {
                if (args.Length == 0)
                    Info((Object)format);
                else
                    WriteToFile(String.Format(format,args), "INFO");
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
               WriteToFile(o,"DEBUG");
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
            WriteToFile(o,"ERROR");
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
        private static void WriteToFile(Object o,String logmode){

            if(logWriter!=null)
              logWriter.WriteLine(o);
            using (StreamWriter sw = new StreamWriter(GetFilePath() + ".log", true))
            {
                sw.AutoFlush = true;
                sw.WriteLine(logmode +": "+ o.ToString());
            }
        }

         /// <summary>
        /// Get the file name for the current execution
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            if (name == null)
                name = "TestResult";
            String fileName = name.Split('.').LastOrDefault();
            var invalids = Path.GetInvalidFileNameChars().ToList();
             invalids.AddRange(Path.GetInvalidPathChars());
            var newName = String.Join("_", fileName.Split(invalids.ToArray(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            String filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", buildStartTime.ToString("MMMMdd_yyyy_HHmmss"), name.Replace(fileName, ""));
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, newName );
            return filePath;
        }


    }


    public enum LogMode{
        DEBUG,
        INFO,
        ERROR
    }
}
