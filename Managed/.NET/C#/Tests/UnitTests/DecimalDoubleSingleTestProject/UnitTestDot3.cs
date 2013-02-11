using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot3 : UnitTestBase
    {
        public UnitTestDot3()
            : base(0.3f, 0.3d, 0.3m)
        {
        }
    }
}
