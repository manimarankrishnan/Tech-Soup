using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceCSharp.Core;
using System.Drawing.Imaging;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
namespace SeleniumCSharp.Selenium
{
    public class DriverCommands
    {

        public DriverCommands(DriverWrapper driver)
        {
            Driver = driver;
        }

        public DriverWrapper Driver { get; set; }

        public void SaveScreenshot()
        {
            try
            {
                String fileName = Logger.GetFilePath() +Utils.GetUniqueNumber() + ".jpg";
                Driver.GetScreenshot().SaveAsFile(fileName, ImageFormat.Jpeg);
                Logger.Debug("Screenshot saved at file://{0}", fileName.Replace('\\', '/'));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void SavePageSource()
        {
            try
            {
                String fileName =Logger.GetFilePath()+Utils.GetUniqueNumber()+ ".html";
                using (var writer = new System.IO.StreamWriter(fileName))
                {
                    writer.Write(Driver.PageSource);
                    writer.Close();
                }
                Logger.Debug("HTML Page Source Saved at file://{0}", fileName.Replace('\\','/'));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void GetScreenshotAndPageSource()
        {
            SaveScreenshot();
            SavePageSource();
        }


    }
}
