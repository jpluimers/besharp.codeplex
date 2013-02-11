using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestDot9Ten9 : UnitTestBase
    {
        public UnitTestDot9Ten9()
            : base(0.900000000009f, 0.900000000009d, 0.900000000009m)
        {
        }
    }
}
