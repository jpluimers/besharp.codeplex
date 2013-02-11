using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot1Ten1 : UnitTestBase
    {
        public UnitTestDot1Ten1()
            : base(0.100000000001f, 0.100000000001d, 0.100000000001m)
        {
        }
    }
}
