using System;
using BeSharp.Generic;
using System.Diagnostics.Contracts;

namespace BeSharp.NumberVerification
{
    /// <summary>
    /// http://nl.wikipedia.org/wiki/Elfproef - note they are wrong for 10 digit BSN!
    /// http://en.wikipedia.org/wiki/Check_digit
    /// http://www.rijksoverheid.nl/onderwerpen/paspoort-en-identificatie/vraag-en-antwoord/wat-is-het-burgerservicenummer-bsn.html
    /// https://nl.wikipedia.org/wiki/Burgerservicenummer
    /// http://www.burgerservicenummer.nl/
    /// </summary>
    public class BSNVerification : BankrekeningNummerBSNVerificationBase
    {
        public const int MaxBSNLength = 9;
        public const int MinBSNLength = 9;
        private const int modulus = 11;
        private const char zeroDigit = '0';

        public override string Complete(string incompleteNumber)
        {
            string value = incompleteNumber + zeroDigit;

            bool passedSanityCheck = saneWithoutPoints(ref value);
            if (!passedSanityCheck)
                throw new Exception<BSNVerification>("{0} didn't pass sanity check".With(Reflector.GetNameSeparatorValue(new { partialBSN = incompleteNumber })));

            int weightedModulus = getWeightedModulus(ref value);

            int lastDigit = weightedModulus;

            if (lastDigit > 9)
                throw new Exception<BSNVerification>("Cannot complete {0} as it would result in {1}".With(Reflector.GetNameSeparatorValue(new { partialBSN = incompleteNumber }), Reflector.GetNameSeparatorValue(new { lastDigit })));

            value = incompleteNumber + lastDigit.ToString();

            return value;
        }

        protected override int getWeightedModulus(ref string value)
        {
            value = value.PadLeft(MaxBSNLength, zeroDigit);
            // http://en.wikipedia.org/wiki/Weighted_sum
            // https://nl.wikipedia.org/wiki/Burgerservicenummer#11-proef
            int weightedSum = 0;
            int weight = MaxBSNLength;
            foreach (char item in value)
            {
                int digit = int.Parse(item.ToString());
                if (1 == weight)
                    weightedSum -= digit * weight;
                else
                    weightedSum += digit * weight;
                weight--;
            }

            int weightedModulus = weightedSum % modulus;
            return weightedModulus;
        }

        protected override bool isInvalidLength(int value)
        {
            return (value < MinBSNLength) || (value > MaxBSNLength);
        }

    }
}
