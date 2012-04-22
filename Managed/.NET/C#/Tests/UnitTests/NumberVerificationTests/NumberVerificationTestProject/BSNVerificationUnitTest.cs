using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeSharp.NumberVerification;
using BeSharp.Generic;

namespace NumberVerificationTestProject
{
    /// <summary>
    /// http://nl.wikipedia.org/wiki/Elfproef / http://en.wikipedia.org/wiki/Check_digit
    /// http://www.rijksoverheid.nl/onderwerpen/paspoort-en-identificatie/vraag-en-antwoord/wat-is-het-burgerservicenummer-bsn.html
    /// </summary>
    [TestClass]
    public class BSNVerificationUnitTest
    {
        readonly string[] invalidTestBSNs = 
        { 
            "007006029", "010476898", "020510517", "029115076", "049410995", "055965634", "065458263", "070094592", "080629561", "093831690", 
            "102696509", "111374578", "124248177", "128802276", "140927865", "152913414", "166602203", "172508892", "182551901", "193260180", 
            "200115829", "206505378", "210381157", "230535346", "241265035", "257075354", "266048593", "277021802", "285150411", "285771270",
            "abscdefghi", "abscdefgh"
        };
        readonly string[] validTestBSNs = 
        {
            "007006020", "010476891", "020510512", "029115073", "049410994", "055965635", "065458266", "070094597", "080629568", "093831699", 
            "102696500", "111374571", "124248172", "128802273", "140927864", "152913415", "166602206", "172508897", "182551908", "193260189", 
            "200115820", "206505371", "210381152", "230535343", "241265034", "257075355", "266048596", "277021807", "285150418", "285771279"
        };

        readonly string[] becomesNineDigits = { "00000000", "00000001", "00000002", "00000003", "00000004", "00000006", "00000007", "00000008", "00000009" };
        readonly string[] cannotBecomeNineDigits = { "00000005" };

        [TestMethod]
        public void Complete_TestMethod()
        {
            BSNVerification verification = new BSNVerification();

            foreach (string becomesNineDigit in becomesNineDigits)
            {
                string nineDigit = verification.Complete(becomesNineDigit);
                bool valid = verification.IsValid(nineDigit);
                Assert.IsTrue(valid, "Completed {0} into {1}, but {1} was not valid", becomesNineDigit, nineDigit);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception<BSNVerification>))]
        public void Complete_cannotBecomeTenDigits_TestMethod()
        {
            BSNVerification verification = new BSNVerification();

            Exception exception = null;
            foreach (string cannotBecomeNineDigit in cannotBecomeNineDigits)
            {
                try
                {
                    string nineDigit = verification.Complete(cannotBecomeNineDigit);
                    Assert.Inconclusive("It should not be possible that {0} completed into {1}", Reflector.GetNameSeparatorValue(new { cannotBecomeNineDigit }), Reflector.GetNameSeparatorValue(new { nineDigit }));
                }
                catch (Exception ex)
                {
                    exception = ex;
                    continue;
                }
            }
            throw exception;
        }

        [TestMethod]
        public void IsValid_TestMethod()
        {
            BSNVerification verification = new BSNVerification();

            foreach (string inValidBSNTestNummer in invalidTestBSNs)
            {
                bool result = verification.IsValid(inValidBSNTestNummer);
                bool expected = false;
                Assert.AreEqual(expected, result, "Expected BSNVerification.IsValid({0} to be {1}", inValidBSNTestNummer, expected);
            }

            foreach (string validBSNTestNummer in validTestBSNs)
            {
                bool result = verification.IsValid(validBSNTestNummer);
                bool expected = true;
                Assert.AreEqual(expected, result, "Expected BSNVerification.IsValid({0} to be {1}", validBSNTestNummer, expected);
            }
        }

        [TestMethod]
        public void Random_validLengths_TestMethod()
        {
            BSNVerification verification = new BSNVerification();
            runRandomManyTimesForAllLengths(verification, validateGeneratedNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception<BankrekeningNummerBSNVerificationBase>))]
        public void Random_invalidLength_TestMethod()
        {
            BSNVerification verification = new BSNVerification();
            string generatedBSN = verification.Random(100); // invalid length; should throw an exception
        }
        private static void validateGeneratedNumber(BSNVerification verification, string generatedBSN)
        {
            bool validBSN = verification.IsValid(generatedBSN);
            Assert.IsTrue(validBSN, "Generated {0} is invalid", Reflector.GetNameSeparatorValue(new { generatedBSN }));
        }

        [TestMethod]
        public void Scramble_TestMethod()
        {
            BSNVerification verification = new BSNVerification();

            foreach (string validBSNTestNummer in validTestBSNs)
            {
                scamble_TestMethod(verification, validBSNTestNummer);
            }
            runRandomManyTimesForAllLengths(verification, scamble_TestMethod);
        }

        private static void scamble_TestMethod(BSNVerification verification, string validBSNTestNummer)
        {
            string rot5 = verification.Scramble(validBSNTestNummer);
            foreach (char item in rot5)
            {
                bool result = char.IsNumber(item) || (item == '.');
                bool expected = true;
                Assert.AreEqual(expected, result, "Expected all characters returned by BSNVerification.Rot5 to be numeric or dot, but {0} isn't", item);
            }
        }

        private static void runRandomManyTimesForAllLengths(BSNVerification verification, Action<BSNVerification, string> action)
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int length = BSNVerification.MinBSNLength; length < BSNVerification.MaxBSNLength; length++)
                {
                    string generatedBSN = verification.Random(length);
                    action(verification, generatedBSN);
                }
            }
        }

    }
}
