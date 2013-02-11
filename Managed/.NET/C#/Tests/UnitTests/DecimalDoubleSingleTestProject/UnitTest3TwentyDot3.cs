using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTest3TwentyDot3 : UnitTestBase
    {
        public UnitTest3TwentyDot3()
            : base(344444444444444444444.3f, 344444444444444444444.3d, 344444444444444444444.3m)
        {
        }
    }
}
