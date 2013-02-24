using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp.NumberVerification
{
    public class GS1
    {
        /// <summary>
        /// http://www.gs1.org/barcodes/support/prefix_list
        /// http://en.wikipedia.org/wiki/List_of_GS1_country_codes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string CountryByID(int id)
        {
            string result;
            if (!CountryByIdDictionary.TryGetValue(id, out result))
                result = gs1GlobalOffice;
            return result;
        }

        private static readonly string gs1GlobalOffice = "GS1 Global Office";

        private static Dictionary<int, string> countryByIdDictionary;
        protected static Dictionary<int, string> CountryByIdDictionary
        {
            get
            {
                if (null == countryByIdDictionary)
                {
                    countryByIdDictionary = new Dictionary<int, string>();
                    fillCountryByIdDictionary();
                }
                return countryByIdDictionary;
            }
        }

        private static void fillCountryByIdDictionary()
        {
            // ASSIGNED GS1 PREFIXES
            add(000, 019, "GS1 US");
            add(020, 029, "Restricted distribution (MO defined)");
            add(030, 039, "GS1 US");
            add(040, 049, "Restricted distribution (MO defined)");
            add(050, 059, "Coupons");
            add(060, 139, "GS1 US");
            add(200, 299, "Restricted distribution (MO defined)");
            add(300, 379, "GS1 France");
            add(380, "GS1 Bulgaria");
            add(383, "GS1 Slovenija");
            add(385, "GS1 Croatia");
            add(387, "GS1 BIH (Bosnia-Herzegovina)");
            add(389, "GS1 Montenegro");
            add(400, 440, "GS1 Germany");
            add(450, 459, 490, 499, "GS1 Japan");
            add(460, 469, "GS1 Russia");
            add(470, "GS1 Kyrgyzstan");
            add(471, "GS1 Taiwan");
            add(474, "GS1 Estonia");
            add(475, "GS1 Latvia");
            add(476, "GS1 Azerbaijan");
            add(477, "GS1 Lithuania");
            add(478, "GS1 Uzbekistan");
            add(479, "GS1 Sri Lanka");
            add(480, "GS1 Philippines");
            add(481, "GS1 Belarus");
            add(482, "GS1 Ukraine");
            add(484, "GS1 Moldova");
            add(485, "GS1 Armenia");
            add(486, "GS1 Georgia");
            add(487, "GS1 Kazakstan");
            add(488, "GS1 Tajikistan");
            add(489, "GS1 Hong Kong");
            add(500, 509, "GS1 UK");
            add(520, 521, "GS1 Association Greece");
            add(528, "GS1 Lebanon");
            add(529, "GS1 Cyprus");
            add(530, "GS1 Albania");
            add(531, "GS1 MAC (FYR Macedonia)");
            add(535, "GS1 Malta");
            add(539, "GS1 Ireland");
            add(540, 549, "GS1 Belgium & Luxembourg");
            add(560, "GS1 Portugal");
            add(569, "GS1 Iceland");
            add(570, 579, "GS1 Denmark");
            add(590, "GS1 Poland");
            add(594, "GS1 Romania");
            add(599, "GS1 Hungary");
            add(600, 601, "GS1 South Africa");
            add(603, "GS1 Ghana");
            add(604, "GS1 Senegal");
            add(608, "GS1 Bahrain");
            add(609, "GS1 Mauritius");
            add(611, "GS1 Morocco");
            add(613, "GS1 Algeria");
            add(615, "GS1 Nigeria");
            add(616, "GS1 Kenya");
            add(618, "GS1 Ivory Coast");
            add(619, "GS1 Tunisia");
            add(621, "GS1 Syria");
            add(622, "GS1 Egypt");
            add(624, "GS1 Libya");
            add(625, "GS1 Jordan");
            add(626, "GS1 Iran");
            add(627, "GS1 Kuwait");
            add(628, "GS1 Saudi Arabia");
            add(629, "GS1 Emirates");
            add(640, 649, "GS1 Finland");
            add(690, 695, "GS1 China");
            add(700, 709, "GS1 Norway");
            add(729, "GS1 Israel");
            add(730, 739, "GS1 Sweden");
            add(740, "GS1 Guatemala");
            add(741, "GS1 El Salvador");
            add(742, "GS1 Honduras");
            add(743, "GS1 Nicaragua");
            add(744, "GS1 Costa Rica");
            add(745, "GS1 Panama");
            add(746, "GS1 Republica Dominicana");
            add(750, "GS1 Mexico");
            add(754, 755, "GS1 Canada");
            add(759, "GS1 Venezuela");
            add(760, 769, "GS1 Schweiz, Suisse, Svizzera");
            add(770, 771, "GS1 Colombia");
            add(773, "GS1 Uruguay");
            add(775, "GS1 Peru");
            add(777, "GS1 Bolivia");
            add(778, 779, "GS1 Argentina");
            add(780, "GS1 Chile");
            add(784, "GS1 Paraguay");
            add(786, "GS1 Ecuador");
            add(789, 790, "GS1 Brasil");
            add(800, 839, "GS1 Italy");
            add(840, 849, "GS1 Spain");
            add(850, "GS1 Cuba");
            add(858, "GS1 Slovakia");
            add(859, "GS1 Czech");
            add(860, " GS1 Serbia");
            add(865, "GS1 Mongolia");
            add(867, "GS1 North Korea");
            add(868, 869, "GS1 Turkey");
            add(870, 879, "GS1 Netherlands");
            add(880, "GS1 South Korea");
            add(884, "GS1 Cambodia");
            add(885, "GS1 Thailand");
            add(888, "GS1 Singapore");
            add(890, "GS1 India");
            add(893, "GS1 Vietnam");
            add(896, "GS1 Pakistan");
            add(899, "GS1 Indonesia");
            add(900, 919, "GS1 Austria");
            add(930, 939, "GS1 Australia");
            add(940, 949, "GS1 New Zealand");
            add(950, gs1GlobalOffice);
            add(951, "GS1 Global Office (EPCglobal)");
            add(955, "GS1 Malaysia");
            add(958, "GS1 Macau");
            add(960 - 969, "Global Office (GTIN-8s)");
            add(977, "Serial publications (ISSN)");
            add(978, 979, "Bookland (ISBN)");
            add(980, "Refund receipts");
            add(981, 983, "Common Currency Coupons");
            add(990, 999, "Coupons");
            //Note: From the 960 prefix range, 9600 to 9604 have been assigned to GS1 UK for GTIN-8 allocations. Prefixes not listed above are used by GS1 Global Office for number allocations in non-member countries and reserved for future use.
        }

        private static void add(int startId1, int finishId1, int startId2, int finishId2, string country)
        {
            add(startId1, finishId1, country);
            add(startId2, finishId2, country);
        }

        private static void add(int startId, int finishId, string country)
        {
            for (int id = startId; id <= finishId; id++)
            {
                add(id, country);
            }
        }

        private static void add(int id, string country)
        {
            countryByIdDictionary.Add(id, country);
        }

    }
}
