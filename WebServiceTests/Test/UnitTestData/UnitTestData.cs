using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebServiceCSharp.Core;
using Utils.Core;

namespace WebServiceTests.Test
{
    public class UnitTestData
    {
        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
        }

        /// <summary>
        /// Test Method: String GetValue(String key)
        /// </summary>
        [Test]
        public void TestMethodGetValue()
        {
            Data dataObj = new Data("TestCaseData_Others_TC_createResource");
            string testID = dataObj.GetValue("ID");
            string uri = dataObj.GetValue("URISegment");
            string method = dataObj.GetValue("Method");
            string headers = dataObj.GetValue("Headers");

            //verify the values to check whether GetValue method gets the proper values
            Assert.AreEqual(testID, "TC_createResource");
            Assert.AreEqual(uri, "http://jsonplaceholder.typicode.com/posts");
            Assert.AreEqual(method, "POST");
            Assert.AreEqual(headers, "Content-Type:application/json");
        }

        /// <summary>
        /// Test Method: List<String> GetValueList(String key)
        /// </summary>
        [Test]
        public void TestMethodGetValueList()
        {            
            List<string> values = new List<string> ();
                values.Add("one");
                values.Add("two");
                values.Add("three");
            Data dataObj = new Data("TestCaseData_Others_TC_createResource");
            dataObj.SetValueList("key", values);
            List<string> sampleList = dataObj.GetValueList("key");
            
        }

        /// <summary>
        /// Test Method: Data GetDataObject(String key)
        /// </summary>
        [Test]
        public void TestMethodGetDataObject()
        {
            Data obj = new Data("TestCaseData_Others_TC_createResource");
            Data objNew = new Data("TestCaseData_Others_TC_createResource");
            obj.SetDataObject("key",objNew);
            Data sampleList = obj.GetDataObject("key");

        }

        /// <summary>
        /// Test Method: bool IsValueKeyPresent(String key)
        /// </summary>
        [Test]
        public void TestMethodIsValueKeyPresent()
        {
            Data obj = new Data();
            obj.SetValue("ID","SomeVAlue");
            bool isPresent = obj.IsValueKeyPresent("ID");
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Test Method: IsListValueKeyPresent(String listKey)
        /// </summary>
        [Test]
        public void TestMethodIsListValueKeyPresent()
        {
            Data obj = new Data();
            List<string> values = new List<string>();
            values.Add("one");
            values.Add("two");
            values.Add("three");
            obj.SetValueList("ID", values);
            bool isPresent = obj.IsListValueKeyPresent("ID");
            Assert.IsTrue(isPresent);
        }

        /// <summary>
        /// Test Method: IsDataKeyPresent(String dataKey)
        /// </summary>
        [Test]
        public void TestMethodIsDataKeyPresent()
        {
            Data obj = new Data("TestCaseData_Others_TC_createResource");
            Data objNew = new Data("TestCaseData_Others_TC_createResource");
            obj.SetDataObject("key", objNew);
            bool isPresent = obj.IsDataKeyPresent("key");
            Assert.IsTrue(isPresent);
        }
                
    }
}
