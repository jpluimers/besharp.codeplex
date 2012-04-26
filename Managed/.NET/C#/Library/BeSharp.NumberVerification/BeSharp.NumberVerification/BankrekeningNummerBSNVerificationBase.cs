using System;
using System.Collections.Generic;
using System.Linq;
using BeSharp.Generic;
using System.Diagnostics.Contracts;

namespace BeSharp.NumberVerification
{
    public abstract class BankrekeningNummerBSNVerificationBase : NumberVerification
    {
        protected const int modulus = 11;
        protected const char zeroDigit = '0';

        protected abstract bool isInvalidLength(int length);

        protected abstract int getLastDigitFromWeightedModulus(int weightedModulus);

        protected abstract int getWeightedModulus(ref string value);

        protected virtual bool passesSanityCheck(string value)
        {
            if (!value.IsAllDigits())
                return false;

            if (isInvalidLength(value.Length))
                return false;

            return true;
        }

        protected virtual bool saneWithoutPoints(ref string value)
        {
            value = stripPoints(value);

            bool passedSanityCheck = passesSanityCheck(value);
            return passedSanityCheck;
        }

        protected virtual string stripPoints(string BSN)
        {
            string value = BSN.Replace(".", string.Empty);
            return value;
        }

        public override string Complete(string incompleteNumber)
        {
            string value = incompleteNumber + zeroDigit;

            bool passedSanityCheck = saneWithoutPoints(ref value);
            if (!passedSanityCheck)
                throw new Exception<BankrekeningNummerBSNVerificationBase>("{0} didn't pass sanity check".With(Reflector.GetNameSeparatorValue(new { partialBankrekeningNummer = incompleteNumber })));

            int weightedModulus = getWeightedModulus(ref value);

            int lastDigit = getLastDigitFromWeightedModulus(weightedModulus);

            if (lastDigit > 9)
                throw new Exception<BankrekeningNummerBSNVerificationBase>("Cannot complete {0} as it would result in {1}".With(Reflector.GetNameSeparatorValue(new { partialBankrekeningNummer = incompleteNumber }), Reflector.GetNameSeparatorValue(new { lastDigit })));

            value = incompleteNumber + lastDigit;

            return value;
        }

        public override bool IsValid(string number)
        {
            string value = number;
            bool passedSanityCheck = saneWithoutPoints(ref value);
            if (!passedSanityCheck)
                return false;

            int weightedModulus = getWeightedModulus(ref value);

            bool result = weightedModulus == 0;
            return result;
        }

        public override string Random(int numberLength)
        {
            if (isInvalidLength(numberLength))
                throw new Exception<BankrekeningNummerBSNVerificationBase>("{0} is an invalid length".With(Reflector.GetNameSeparatorValue(new { numberLength })));

            Random random = new Random();
            while (true)
            {
                try
                {
                    string result = string.Empty;

                    for (int i = 1; i < numberLength; i++) // 1 less, because Complete will compute the CheckDigit at the end
                    {
                        int minDigit = 1 == i ? 1 : 0; // do not make leading zeros
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

        public override string Scramble(string validNumber)
        {
            Contract.Requires(IsValid(validNumber));
            string rot5number = Rot.Rot5(validNumber);
            int lastDigitPosition = rot5number.LastIndexOfAny(Chars.Digits);
            // aaaaaaa1bb
            // 0123456789
            // length = 10
            // lastDigitPosition = 7
            // suffixLength = 2
            // prefixLength = 7
            string incompleteNumber = rot5number.Substring(0, lastDigitPosition);
            string result;
            try
            {
                result = Complete(incompleteNumber);
            }
            catch (Exception)
            {
                // checkDigit was 10, so force it to be 8:
                // we alter the 2nd digit from the end; the modulus was 10, the total will be 1*20+10 resulting in a modulus of 8
                int alterDigitPosition = incompleteNumber.LastIndexOfAny(Chars.Digits);
                char alterDigitChar = incompleteNumber[alterDigitPosition];
                int alterDigit = int.Parse(alterDigitChar.ToString());
                int alteredDigit = (alterDigit + 1) % 10;
                string alteredIncompleteNumber = incompleteNumber.Substring(0, alterDigitPosition) + alteredDigit + incompleteNumber.Substring(alterDigitPosition + 1);
                result = Complete(alteredIncompleteNumber);
            }
            result += rot5number.Substring(lastDigitPosition + 1); // make sure non digits get at the end
            return result;
        }

    }
}
