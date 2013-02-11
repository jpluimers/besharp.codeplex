using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot1 : UnitTestBase
    {
        public UnitTestDot1()
            : base(0.1f, 0.1d, 0.1m)
        {
        }
    }
}
