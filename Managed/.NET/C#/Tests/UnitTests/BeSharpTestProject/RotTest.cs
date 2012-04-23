using BeSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeSharpTestProject
{
    
    
    /// <summary>
    ///This is a test class for RotTest and is intended
    ///to contain all RotTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RotTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Rot Constructor
        ///</summary>
        [TestMethod()]
        public void RotConstructorTest()
        {
            Rot target = new Rot();
        }

        // this might be nicer: http://stackoverflow.com/a/4617433/29290 but we do it in once class.
        private readonly Lazy<Rot13TestData> lazyRot13TestData = new Lazy<Rot13TestData>();
        protected Rot13TestData rot13TestData
        {
            get
            {
                return lazyRot13TestData.Value;
            }
        }

        private readonly Lazy<Rot5TestData> lazyRot5TestData = new Lazy<Rot5TestData>();
        protected Rot5TestData rot5TestData
        {
            get
            {
                return lazyRot5TestData.Value;
            }
        }

        /// <summary>
        ///A test for Rot13
        ///</summary>
        [TestMethod()]
        public void Rot13Test()
        {
            rotTest(rot13TestData, Rot.Rot13);
        }

        private static void rotTest(RotTestData rotTestData, Func<string, string>rotFunc)
        {
            foreach (KeyValuePair<string, string> valueRot in rotTestData)
            {
                string value = valueRot.Key;
                string expected = valueRot.Value;
                string actual = rotFunc(value);
                Assert.AreEqual(expected, actual);
            }
            foreach (KeyValuePair<string, string> valueRot in rotTestData)
            {
                string value = valueRot.Value;
                string expected = valueRot.Key;
                string actual = rotFunc(value);
                Assert.AreEqual(expected, actual);
            }
            foreach (KeyValuePair<string, string> valueRot in rotTestData)
            {
                string value = valueRot.Key;
                string expected = valueRot.Key;
                string actual = rotFunc(value);
                actual = rotFunc(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for Rot13
        ///</summary>
        [TestMethod()]
        public void Rot13Test1()
        {
            Rot135Test();
        }

        /// <summary>
        ///A test for Rot135
        ///</summary>
        [TestMethod()]
        public void Rot135Test()
        {
            Rot13Test();
        }

        /// <summary>
        ///A test for Rot135
        ///</summary>
        [TestMethod()]
        public void Rot135Test1()
        {
            Rot13Test1();
        }

        /// <summary>
        ///A test for Rot5
        ///</summary>
        [TestMethod()]
        public void Rot5Test()
        {
            rotTest(rot5TestData, Rot.Rot5);
        }

        /// <summary>
        ///A test for Rot5
        ///</summary>
        [TestMethod()]
        public void Rot5Test1()
        {
            Rot5Test();
        }
    }
}
