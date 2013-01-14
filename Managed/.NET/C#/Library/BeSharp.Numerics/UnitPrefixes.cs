using System;
using System.Numerics;

namespace BeSharp.Numerics
{
    public static class UnitPrefixes
    {
        // http://en.wikipedia.org/wiki/Binary_prefix

        public const UInt64 BitsPerByte = 8;

        #region Binary

        // favour Int32/Int64 over UInt32/UInt64, as most C# APIs use uint
        public const Int32 kibi = 1024;
        public const Int32 mebi = kibi * kibi;
        public const Int32 gibi = kibi * mebi;
        public const Int64 tebi = kibi * ((Int64)gibi);
        public const Int64 pebi = kibi * tebi;
        public const Int64 exbi = kibi * pebi;
        public readonly static BigInteger zebi = kibi * ((BigInteger)exbi);
        public readonly static BigInteger yobi = kibi * zebi;

        public const Int32 KiB = kibi;
        public const Int32 MiB = mebi;
        public const Int32 GiB = gibi;
        public const Int64 Ti = tebi;
        public const Int64 Pi = pebi;
        public const Int64 Ei = exbi;
        public readonly static BigInteger Zi = zebi;
        public readonly static BigInteger Yi = yobi;
        //Prefixes for multiples of
        //bits (b) or bytes (B)
        //Decimal
        //Value	Metric
        //1000	k	kilo
        //10002	M	mega
        //10003	G	giga
        //10004	T	tera
        //10005	P	peta
        //10006	E	exa
        //10007	Z	zetta
        //10008	Y	yotta
        //Binary
        //Value	JEDEC	IEC
        //1024	K	kilo	Ki	kibi
        //10242	M	mega	Mi	mebi
        //10243	G	giga	Gi	gibi
        //10244			Ti	tebi
        //10245			Pi	pebi
        //10246			Ei	exbi
        //10247			Zi	zebi
        //10248			Yi	yobi
        #endregion

        // http://en.wikipedia.org/wiki/Metric_prefix

        #region Metric
        public const Int32 one = 1;
        public const Int32 deca = 10;
        public const Int32 hecto = 100;
        public const Int32 kilo = 1000;
        public const Int32 mega = kilo * kilo;
        public const Int32 giga = kilo * mega;
        public const Int64 tera = kilo * ((Int64)giga);
        public const Int64 peta = kilo * tera;
        public const Int64 exa = kilo * peta;
        public readonly static BigInteger zetta = kilo * ((BigInteger)exa);
        public readonly static BigInteger yotta = kilo * zetta;

        public const Int32 h = hecto;
        public const Int32 k = kilo;
        public const Int32 M = mega;
        public const Int32 G = giga;
        public const Int64 T = tera;
        public const Int64 P = peta;
        public const Int64 E = exa;
        public readonly static BigInteger Z = zetta;
        public readonly static BigInteger Y = yotta;

        // use Decimal for fractions: http://csharpindepth.com/Articles/General/Decimal.aspx
        public const Decimal deci = 1 / ((Decimal)deca);
        public const Decimal centi = deci / deca;
        public const Decimal milli = centi / deca;
        public const Decimal micro = milli / milli;
        public const Decimal nano = milli / micro;

        public const Decimal d = deci;
        public const Decimal c = centi;
        public const Decimal m = milli;
        public const Decimal μ = micro;
        public const Decimal n = nano;

        //Metric prefixes in everyday use
        //Text	Symbol	Factor
        //tera	T	1000000000000
        //giga	G	1000000000
        //mega	M	1000000
        //kilo	k	1000
        //hecto	h	100
        //(none)	(none)	1
        //deci	d	0.1
        //centi	c	0.01
        //milli	m	0.001
        //micro	μ	0.000001
        //nano	n	0.000000001
        #endregion
    }
}
