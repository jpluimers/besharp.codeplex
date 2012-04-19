using System;
using BeSharp.Generic;

namespace BeSharp.NumberVerification
{
    /// <summary>
    /// http://nl.wikipedia.org/wiki/Elfproef - note they are wrong for 10 digit bankrekeningnummer!
    /// http://en.wikipedia.org/wiki/Check_digit
    /// http://www.exactsoftware.com/docs/DocView.aspx?DocumentID=%7B1d45f854-8b31-457d-8348-cc7c9c9349d5%7D
    /// http://www.testnummers.nl/
    /// </summary>
    public class BankrekeningNummerVerification
    {
        public const int MaxBankRekeningNummerLength = 10;
        public const int MinBankRekeningNummerLength = 9;
        private const int modulus = 11;
        private const char zeroDigit = '0';

        public static string Generate(int bankRekeningNummerLength)
        {
            if (isInvalidBankRekeningNummerLength(bankRekeningNummerLength))
                throw new Exception<BankrekeningNummerVerification>("{0} is an invalid length".With(Reflector.GetNameSeparatorValue(new { bankRekeningNummerLength })));

            Random random = new Random();
            while (true)
            {
                try
                {
                    string result = string.Empty;

                    for (int i = 1; i < bankRekeningNummerLength; i++) // 1 less, because Complete will compute the CheckDigit at the end
                    {
                        int minDigit = 1 == i ? 1 :  0; // do not make leading zeros
                        int digit = random.Next(minDigit, 9);
                        result += digit.ToString();
                    }
                    result = Complete(result);

                    return result;
                }
                catch (Exception)
                {
                    continue; // we will eventually get a good number, the chance of having many bad numbers in a row is very low.
                }
            }
        }

        public static string Complete(string partialBankrekeningNummer)
        {
            string value = partialBankrekeningNummer + zeroDigit;

            bool passedSanityCheck = saneWithoutPoints(ref value);
            if (!passedSanityCheck)
                throw new Exception<BankrekeningNummerVerification>("{0} didn't pass sanity check".With(Reflector.GetNameSeparatorValue(new { partialBankrekeningNummer })));

            int weightedModulus = getWeightedModulus(ref value);

            int lastDigit = (modulus - weightedModulus) % modulus;

            if (lastDigit > 9)
                throw new Exception<BankrekeningNummerVerification>("Cannot complete {0} as it would result in {1}".With(Reflector.GetNameSeparatorValue(new { partialBankrekeningNummer }), Reflector.GetNameSeparatorValue(new { lastDigit })));

            value = partialBankrekeningNummer + lastDigit.ToString();

            return value;
        }

        public static bool IsValid(string bankrekeningNummer)
        {
            string value = bankrekeningNummer;

            bool passedSanityCheck = saneWithoutPoints(ref value);
                        
            if (!passedSanityCheck)
                return false;

            int weightedModulus = getWeightedModulus(ref value);

            bool result = weightedModulus == 0;
            return result;
        }

        private static int getWeightedModulus(ref string value)
        {
            value = value.PadLeft(MaxBankRekeningNummerLength, zeroDigit);

            // http://en.wikipedia.org/wiki/Weighted_sum

            // http://www.testforum.nl/viewtopic.php?t=9495
            // http://www.exactsoftware.com/Docs/DocView.aspx?DocumentID=%7Badaddb0b-a797-4b77-a609-da7401ed6e55%7D
            int weightedSum = 0;
            int weight = 10;
            foreach (char item in value)
            {
                int digit = int.Parse(item.ToString());
                weightedSum += digit * weight;
                weight--;
            }

            int weightedModulus = weightedSum % modulus;
            return weightedModulus;
        }

        private static bool isInvalidBankRekeningNummerLength(int value)
        {
            return (value < MinBankRekeningNummerLength) || (value > MaxBankRekeningNummerLength);
        }

        private static bool passesSanityCheck(string value)
        {
            if (!value.IsAllDigits())
                return false;

            int valueLength = value.Length;
            if (isInvalidBankRekeningNummerLength(valueLength))
                return false;

            return true;
        }

        private static bool saneWithoutPoints(ref string value)
        {
            value = stripPoints(value);

            bool passedSanityCheck = passesSanityCheck(value);
            return passedSanityCheck;
        }

        private static string stripPoints(string bankrekeningNummer)
        {
            string value = bankrekeningNummer.Replace(".", string.Empty);
            return value;
        }
    }
}
