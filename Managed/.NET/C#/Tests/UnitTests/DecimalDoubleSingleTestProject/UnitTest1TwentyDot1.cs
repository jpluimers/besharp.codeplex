using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTest1TwentyDot1 : UnitTestBase
    {
        public UnitTest1TwentyDot1()
            : base(144444444444444444444.1f, 144444444444444444444.1d, 144444444444444444444.1m)
        {
        }
    }
}
