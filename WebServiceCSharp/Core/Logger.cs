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
                if (o.GetType().ToString().Contains("Exception"))
                {
                    Exception e = (Exception)o;
                    Info("Inner Exception: {0} \n Stacktrace : {1}", e.InnerException, e.StackTrace);
                    return;
                }
                WriteToFile(o,"INFO");
            }
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

            if (mode == LogMode.DEBUG || mode == LogMode.ERROR)
            {
                if (o.GetType().ToString().Contains("Exception"))
                {
                    Exception e = (Exception)o;
                    Debug("Inner Exception: {0} \n Stacktrace : {1}", e.InnerException, e.StackTrace);
                    return;
                }
                WriteToFile(o,"DEBUG");
            }
               
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
            if(o.GetType().ToString().Contains("Exception")){
                Exception e = (Exception)o;
                Error("Inner Exception: {0} \n Stacktrace : {1}", e.InnerException, e.StackTrace);
                return;
            }
          
            WriteToFile(o,"ERROR");
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
            using (StreamWriter sw = new StreamWriter(getFilePath(), true))
            {
                sw.AutoFlush = true;
                sw.WriteLine(logmode +": "+ o.ToString());
            }
        }

        /// <summary>
        /// Get the file name for the current execution
        /// </summary>
        /// <returns></returns>
        private static string getFilePath()
        {
            if (name == null)
                name = "TestResult";
            String fileName = name.Split('.').LastOrDefault();
            String filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", buildStartTime.ToString("MMMM_dd_yyyy HHmmss"), name.Replace(fileName, ""));
            Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, fileName + ".txt");
            return filePath;
        }
    }


    public enum LogMode{
        DEBUG,
        INFO,
        ERROR
    }
}
