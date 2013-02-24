using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DecimalDoubleSingleTestProject
{
    [TestClass]
    public class UnitTestPi : UnitTestBase
    {
        /// <summary>
        /// http://www.piday.org/million/
        /// </summary>
        private const decimal pi = 3.141592653589793238462643383279502884197169399375105820974944592307816406286m;
        public UnitTestPi()
            : base((float)Math.PI, Math.PI, pi)
        {
        }
    }
}
