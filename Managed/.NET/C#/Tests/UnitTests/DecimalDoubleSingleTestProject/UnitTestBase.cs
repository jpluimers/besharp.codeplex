using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestBase
    {
        protected float f { get; private set; }
        protected double d { get; private set; }
        protected decimal m { get; private set; }

        /// <summary>
        /// Small trick to "work with" the system and not "work against" it:
        /// You cannot use [TestMethod] without using [TestClass] as you get compiler warnings like this one:
        ///UTA004: Illegal use of attribute on DecimalDoubleSingleTestProject.UnitTestBase.TestMethod_fd. The TestMethodAttribute can be defined only inside a class marked with the TestClass attribute.
        /// What we do instead is: keep the [TestClass] attribute on the Base class, 
        /// but skipp all tests when we are in exactly the base class.
        /// </summary>
        public bool ThisIsExactlyUnitTestBase
        {
            get
            {
                bool result = this.GetType() == typeof(UnitTestBase);
                return result;
                // The above code is similar to the "this is UnitTestBase", below
                // but now an exact match for the UnitTestBase class.
                //result = this is UnitTestBase;
            }
        }

        /// <summary>
        /// Needs to be public, or you get errors like this:
        ///TestMethod_fd has failed:
        ///  Unable to get default constructor for class DecimalDoubleSingleTestProject.UnitTestBase.
        /// </summary>
        public UnitTestBase()
        {
        }

        /// <summary>
        /// Descending classes call this constructor. In the future we might want to check if f/d and d/m are almost 1.
        /// For each parameter combination (fd, fm, dm) there are 3 variations: _cast_none, _cast_down and _cast_up.
        /// Those show the various comparison behaviours C# on the .NET framework has.
        /// </summary>
        /// <param name="f">float (Single) to test</param>
        /// <param name="d">double (Double) to test</param>
        /// <param name="m">money (Decimal) to test</param>
        public UnitTestBase(float f, double d, decimal m): this()
        {
            this.f = f;
            this.d = d;
            this.m = m;
        }

        [TestMethod]
        public void TestMethod_fd_cast_none()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(f, d);
        }

        [TestMethod]
        public void TestMethod_fd_cast_down()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(f, (float)d);
        }

        [TestMethod]
        public void TestMethod_fd_cast_up()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual((double)f, d);
        }

        [TestMethod]
        public void TestMethod_fm_cast_none()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(f, m);
        }

        [TestMethod]
        public void TestMethod_fm_cast_down()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(f, (float)m);
        }

        [TestMethod]
        public void TestMethod_fm_cast_up()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual((decimal)f, m);
        }

        [TestMethod]
        public void TestMethod_dm_cast_none()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(d, m);
        }

        [TestMethod]
        public void TestMethod_dm_cast_down()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual(d, (double)m);
        }

        [TestMethod]
        public void TestMethod_dm_cast_up()
        {
            if (!ThisIsExactlyUnitTestBase)
                Assert.AreEqual((decimal)d, m);
        }
    }
}
