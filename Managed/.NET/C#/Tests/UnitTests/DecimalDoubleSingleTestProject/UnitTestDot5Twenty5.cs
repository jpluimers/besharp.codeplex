using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot5Twenty5 : UnitTestBase
    {
        public UnitTestDot5Twenty5()
            : base(0.5222222222222222222225f, 0.5222222222222222222225d, 0.5222222222222222222225m)
        {
        }
    }
}
