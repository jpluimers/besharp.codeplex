using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot5Ten5 : UnitTestBase
    {
        public UnitTestDot5Ten5()
            : base(0.500000000005f, 0.500000000005d, 0.500000000005m)
        {
        }
    }
}
