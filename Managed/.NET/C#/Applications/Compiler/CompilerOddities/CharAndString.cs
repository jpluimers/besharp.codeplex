namespace CompilerOddities
{
    public class CharAndString
    {
        public const string A = "A";
        public const string B = "B";
        public const string AB = A + B;
        public const string IJK = "IJK";
        public const char X = 'X'; // int 88
        public const char Y = 'Y'; // int 89
        public const int Value = X + Y; // because char is actually int with a special syntax: 177
        // these don't compile:
        public const string XY = X + Y; // Cannot implicitly convert type 'int' to 'string'
        public const string YX = Y.ToString() + X.ToString(); // The expression being assigned to 'CompilerOddities.CharAndString.YX' must be constant
        public const char I = (char)(IJK[0]); // The expression being assigned to 'CompilerOddities.CharAndString.A1' must be constant
    }
}
