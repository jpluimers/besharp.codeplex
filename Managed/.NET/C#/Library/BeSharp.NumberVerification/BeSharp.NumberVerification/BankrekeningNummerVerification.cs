using System;
using BeSharp.Generic;
using System.Diagnostics.Contracts;

namespace BeSharp.NumberVerification
{
    /// <summary>
    /// http://nl.wikipedia.org/wiki/Elfproef - note they are wrong for 10 digit bankrekeningnummer!
    /// http://en.wikipedia.org/wiki/Check_digit
    /// http://www.exactsoftware.com/docs/DocView.aspx?DocumentID=%7B1d45f854-8b31-457d-8348-cc7c9c9349d5%7D
    /// </summary>
    public class BankrekeningNummerVerification : BankrekeningNummerBSNVerificationBase
    {
        public const int MaxBankrekeningNummerLength = 10;
        public const int MinBankrekeningNummerLength = 9;

        protected override int getLastDigitFromWeightedModulus(int weightedModulus)
        {
            int lastDigit = (modulus - weightedModulus) % modulus;
            return lastDigit;
        }

        protected override int getWeightedModulus(ref string value)
        {
            value = value.PadLeft(MaxBankrekeningNummerLength, zeroDigit);
            // http://en.wikipedia.org/wiki/Weighted_sum
            // http://www.testforum.nl/viewtopic.php?t=9495
            // http://www.exactsoftware.com/Docs/DocView.aspx?DocumentID=%7Badaddb0b-a797-4b77-a609-da7401ed6e55%7D
            int weightedSum = 0;
            int weight = MaxBankrekeningNummerLength;
            foreach (char item in value)
            {
                int digit = int.Parse(item.ToString());
                weightedSum += digit * weight;
                weight--;
            }

            int weightedModulus = weightedSum % modulus;
            return weightedModulus;
        }

        protected override bool isInvalidLength(int value)
        {
            return (value < MinBankrekeningNummerLength) || (value > MaxBankrekeningNummerLength);
        }

    }
}
