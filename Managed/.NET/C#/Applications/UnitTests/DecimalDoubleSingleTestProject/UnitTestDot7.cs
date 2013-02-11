using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot7 : UnitTestBase
    {
        public UnitTestDot7()
            : base(0.7f, 0.7d, 0.7m)
        {
        }
    }
}
