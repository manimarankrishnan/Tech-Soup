using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceCSharp.Core;
using NUnit.Framework;
namespace WebServiceCSharp.Core.Tests
{
    [TestFixture()]
    public class DataTests
    {
        [Test()]
        public void DataTest()
        {
            Data data = new Data();
            
        }

        [Test()]
        public void DataTest1()
        {
            Data data = new Data("TestCaseData_CalculatorTestsWithURI_TC_Div");
            String ss =data.GetValue("Postbody") ;
            String da ="Calculator\\ResponsePerformDivision.xml";
            Assert.AreEqual(ss, da);
        }

        [Test()]
        public void GetValueTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetValueListTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetDataObjectTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetAllValuesTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetAllValueListsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetAllDataObjectsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void SetValueTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void AddValuesTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void AddValuesToListTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void AddValuesToListTest1()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void SetValueListTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void AddValueListsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void SetDataObjectTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void LoadValuesTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void LoadValuesTest1()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void LoadValuesTest2()
        {
            throw new NotImplementedException();
        }
    }
}
