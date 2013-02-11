using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot3Ten3 : UnitTestBase
    {
        public UnitTestDot3Ten3()
            : base(0.300000000003f, 0.300000000003d, 0.300000000003m)
        {
        }
    }
}
