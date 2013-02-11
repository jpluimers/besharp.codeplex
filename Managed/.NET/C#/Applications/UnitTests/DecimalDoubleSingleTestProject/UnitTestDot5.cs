using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot5 : UnitTestBase
    {
        public UnitTestDot5()
            : base(0.5f, 0.5d, 0.5m)
        {
        }
    }
}
