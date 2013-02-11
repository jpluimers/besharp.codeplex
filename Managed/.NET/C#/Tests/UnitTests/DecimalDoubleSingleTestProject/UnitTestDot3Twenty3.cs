using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot3Twenty3 : UnitTestBase
    {
        public UnitTestDot3Twenty3()
            : base(0.3222222222222222222223f, 0.3222222222222222222223d, 0.3222222222222222222223m)
        {
        }
    }
}
