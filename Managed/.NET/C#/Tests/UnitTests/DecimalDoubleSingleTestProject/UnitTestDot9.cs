using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot9 : UnitTestBase
    {
        public UnitTestDot9()
            : base(0.9f, 0.9d, 0.9m)
        {
        }
    }
}
