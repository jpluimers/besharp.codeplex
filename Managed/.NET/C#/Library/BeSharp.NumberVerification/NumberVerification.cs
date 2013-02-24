using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp.NumberVerification
{
    public abstract class NumberVerification
    {
        public abstract string Complete(string incompleteNumber);
        public abstract bool IsValid(string number);
        public abstract string Random(int numberLength);
        public abstract string Scramble(string validNumber);
    }
}
