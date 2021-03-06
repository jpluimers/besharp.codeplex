﻿using System;
using System.Linq;
using System.IO;
using System.Globalization;

namespace FASTLogToXml
{
    public class LogEntry // must be public because of XML serialization
    {
        /*
Date, Time, Type, Filename, Extension, Size, Transferred, Exchanged, Efficiency
14-Sep-12, 07:41:05, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\build-49\Appliance-Install\release\Talon-0-9-0-0-49-WinLH-x64-release.exe , .exe ,9882132,9885770,9863796,0.185547
14-Sep-12, 08:00:32, From Datacenter Default , T:\AMS-TSM1\127.0.0.1\FASTShare$\Scheduler\policydb.xml , .xml ,707,707,707,0
14-Sep-12, 08:16:43, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\NDIS\WNET_X64\netim.inf , .inf ,3702,3702,3702,0
14-Sep-12, 08:16:54, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Marketing\Thumbs.db , .db ,20480,20506,16850,17.7246
14-Sep-12, 08:31:34, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Howto Use Talon Log Parser.txt , .txt ,715,715,715,0
14-Sep-12, 08:31:53, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Talon Storage Solutions - Log Parser v1.0.zip , .zip ,310697,310828,309241,0.468624
14-Sep-12, 08:33:07, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Thumbs.db , .db ,33280,33312,16359,50.8444
14-Sep-12, 08:33:19, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 User Guide.docx , .docx ,1675196,1675828,1547453,7.62556
14-Sep-12, 08:33:20, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 User Guide.docx , .docx ,162,162,162,0
14-Sep-12, 08:33:48, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx , .docx ,1674533,1675165,1546799,7.62804
14-Sep-12, 08:33:50, From Datacenter Default , T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx , .docx ,162,162,162,0
,,,,,,,,
,,,,,,13606857,13305946,
         */
        // Fields...
        public DateTime TimeStamp { get; set; }
        public string TransferType { get; set; }
        public string FilePath { get; set; }
        public string FileExtension
        {
            get
            {
                return Path.GetExtension(FilePath);
            }
        }
        public UInt64 FileSizeBytes { get; set; }
        public UInt64 TransferredBytes { get; set; }
        public UInt64 ExchangedBytes { get; set; }
        public decimal TransferEfficiencyPercentage { get; set; }
        //Date, Time, Type, Filename, Extension, Size, Transferred, Exchanged, Efficiency
        public static bool IsValid(string logEntry)
        {
            if (string.IsNullOrEmpty(logEntry))
                return false;
            if (!logEntry.StartsWith("("))
                return false;
            if (!logEntry.EndsWith("]"))
                return false;
            if (logEntry.Length < 60)
                return false;

            return true;
        }

        public bool IsValid()
        {
                try
                {
                    string test = FileExtension;
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
        }
        

        /// <summary>
        /// Parameterless constructor for XML Serialization
        /// </summary>
        internal LogEntry()
        {
        }

        public LogEntry(string logEntry)
        {
            if (!IsValid(logEntry))
                throw new ArgumentException("Invalid LogEntry string", "logEntry");
            // The line is ALWAYS of this form:
            // (note: \t is the TAB character)
            //(Fri Sep 14 07:41:05 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\build-49\Appliance-Install\release\Talon-0-9-0-0-49-WinLH-x64-release.exe File Size[9882132] Bytes Transferred[9885770] Bytes Exchanged[9863796] Transfer Efficiency[0.185547]
            //(Fri Sep 14 08:00:32 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\127.0.0.1\FASTShare$\Scheduler\policydb.xml File Size[707] Bytes Transferred[707] Bytes Exchanged[707] Transfer Efficiency[0]
            //(Fri Sep 14 08:16:43 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\NDIS\WNET_X64\netim.inf File Size[3702] Bytes Transferred[3702] Bytes Exchanged[3702] Transfer Efficiency[0]
            //(Fri Sep 14 08:16:54 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Marketing\Thumbs.db File Size[20480] Bytes Transferred[20506] Bytes Exchanged[16850] Transfer Efficiency[17.7246]
            //(Fri Sep 14 08:31:34 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Howto Use Talon Log Parser.txt File Size[715] Bytes Transferred[715] Bytes Exchanged[715] Transfer Efficiency[0]
            //(Fri Sep 14 08:31:53 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Talon Storage Solutions - Log Parser v1.0.zip File Size[310697] Bytes Transferred[310828] Bytes Exchanged[309241] Transfer Efficiency[0.468624]
            //(Fri Sep 14 08:33:07 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Thumbs.db File Size[33280] Bytes Transferred[33312] Bytes Exchanged[16359] Transfer Efficiency[50.8444]
            //(Fri Sep 14 08:33:19 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 User Guide.docx File Size[1675196] Bytes Transferred[1675828] Bytes Exchanged[1547453] Transfer Efficiency[7.62556]
            //(Fri Sep 14 08:33:20 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 User Guide.docx File Size[162] Bytes Transferred[162] Bytes Exchanged[162] Transfer Efficiency[0]
            //(Fri Sep 14 08:33:48 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx File Size[1674533] Bytes Transferred[1675165] Bytes Exchanged[1546799] Transfer Efficiency[7.62804]
            //(Fri Sep 14 08:33:50 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx File Size[162] Bytes Transferred[162] Bytes Exchanged[162] Transfer Efficiency[0]
            //(Fri Oct 19 04:01:17 2012)\tInfo\tTo Datacenter Default LON-FSRV1\qa\dir10\TacitFS_Test_Shortcut.lnk File Size[989] Bytes Transferred[989] Bytes Exchanged[989] Transfer Efficiency[0]
            //(Fri Oct 19 04:01:17 2012)\tInfo\tTo Datacenter Gathered-write LON-FSRV1\qa\dir80\hfbvgmepgwmesmstlmwgxsesqtsrhhvb79220479FIONXYIQF.NUST File Size[43] Bytes Transferred[43] Bytes Exchanged[43] Transfer Efficiency[0]
            //0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
            //0---------1---------2---------3---------4---------5---------6---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1---------1
            // 1,3 5,3 9  2  5  8  21,4
            //(Tue Dec 06 16:11:46 2011)\tInfo\tTo Datacenter Default T:\dfrgscore-ishr1\DFRDCFILESVRV01.dfraus.local\ME-DFRAD1\Home\driley\2011-12 ARES YTD Progress.xls File Size[71680] Bytes Transferred[71724] Bytes Exchanged[19081] Transfer Efficiency[73.3803]
            //(Tue Dec 06 16:11:54 2011)\tInfo\tFrom Datacenter Default T:\dfrgscore-ishr1\DFRDCFILESVRV01.dfraus.local\ME-DFRAD1\Home\scobrown\Personal Related\Medical\Pathology Records WOFF Scott Neil Brown 8195511 08Jan85-06Dec10 Part2.tif File Size[2171856] Bytes Transferred[2172671] Bytes Exchanged[1938545] Transfer Efficiency[10.7425]
            //(Tue Dec 06 16:12:15 2011)\tInfo\tTo Datacenter Gathered-write T:\dfrgscore-ishr1\DFRFP1-DC.dfraus.local\Richmond_CRMC\Team leader\Lyle Staff\One on One\Hagan Ebert\~$ One on One Master.doc File Size[162] Bytes Transferred[162] Bytes Exchanged[162] Transfer Efficiency[0]
            //const string dateTimePrefix = "(";
            //const string dateTime = "Tue Dec 06 16:12:15 2011";
            //const string dateTimeSuffix = ")";
            const string info = "\tInfo\t";
            //const string trasferType = "To Datacenter Gathered-write";
            //const string space = " ";
            //const string filename = @"T:\dfrgscore-ishr1\DFRFP1-DC.dfraus.local\Richmond_CRMC\Team leader\Lyle Staff\One on One\Hagan Ebert\~$ One on One Master.doc";
            const string fileSizePrefix = " File Size[";
            //const string fileSize = "162";
            const string fileSizeSuffix = "]";
            const string transferredPrefix = " Bytes Transferred[";
            //const string transferred = "162";
            const string transferredSuffix = "]";
            const string exchangedPrefix = " Bytes Exchanged[";
            //const string exchanged = "162";
            const string exchangedSuffix = "]";
            const string transferEfficiencyPrefix = " Transfer Efficiency[";
            //const string transferEfficiency = "0";
            const string transferEfficiencySuffix = "]";

            string remaining = logEntry;

            int firstInfoIndex = remaining.IndexOf(info);

            string encodedDateTime = remaining.Substring(0, firstInfoIndex);
            decodeDateTime(encodedDateTime);

            remaining = remaining.Substring(firstInfoIndex + info.Length);

            int fileSizePrefixIndex = remaining.LastIndexOf(fileSizePrefix);
            string statistics = remaining.Substring(fileSizePrefixIndex);

            remaining = remaining.Substring(0, fileSizePrefixIndex);

            int colonIndex = remaining.IndexOf(":");
            int filePathIndex;

            if (-1 == colonIndex) // Core log: file path starts with a machine name and a backslash. Before the machine name, there is a space.
            {
                //To Datacenter Default LON-FSRV1\qa\dir10\TacitFS_Test_Shortcut.lnk
                //To Datacenter Gathered-write LON-FSRV1\qa\dir80\hfbvgmepgwmesmstlmwgxsesqtsrhhvb79220479FIONXYIQF.NUST
                int backslashIndex = remaining.IndexOf(@"\");
                string upuntilBackslash = remaining.Substring(0, backslashIndex);
                int lastSpaceIndex = upuntilBackslash.LastIndexOf(" ");
                filePathIndex = lastSpaceIndex + 1;
            }
            else // Edge log: file path stars with a drive letter and a colon
            {
                //From Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\build-49\Appliance-Install\release\Talon-0-9-0-0-49-WinLH-x64-release.exe
                //From Datacenter Default T:\AMS-TSM1\127.0.0.1\FASTShare$\Scheduler\policydb.xml
                filePathIndex = colonIndex - 1;
            }
            this.TransferType = remaining.Substring(0, filePathIndex - 1);
            this.FilePath = remaining.Substring(filePathIndex);

            // statistics is like:
            // File Size[1674533] Bytes Transferred[1675165] Bytes Exchanged[1546799] Transfer Efficiency[7.62804]
            this.FileSizeBytes = this.TransferredBytes = extractUInt64(fileSizePrefix, fileSizeSuffix, ref statistics);
            this.TransferredBytes = extractUInt64(transferredPrefix, transferredSuffix, ref statistics);
            this.ExchangedBytes = extractUInt64(exchangedPrefix, exchangedSuffix, ref statistics);

            string transferEfficiencyValue = extractString(transferEfficiencyPrefix, transferEfficiencySuffix, ref statistics);
            this.TransferEfficiencyPercentage = Decimal.Parse(transferEfficiencyValue, NumberFormatInfo.InvariantInfo); // allow decimal point
        }

        private void decodeDateTime(string encodedDateTime)
        {
            //(Tue Dec 06 16:12:15 2011)
            //01234567890123456789012345
            //0---------1---------2-----
            string dayOfWeekValue = encodedDateTime.Substring(1, 3);
            string monthValue = encodedDateTime.Substring(5, 3);
            string dayValue = encodedDateTime.Substring(9, 2);
            string hourValue = encodedDateTime.Substring(12, 2);
            string minuteValue = encodedDateTime.Substring(15, 2);
            string secondValue = encodedDateTime.Substring(18, 2);
            string yearValue = encodedDateTime.Substring(21, 4);

            //DayOfWeek dayOfWeek // TODO verify the dayOfWeekValue
            string[] months = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }; // "" to make it 1-based
            int month = months.ToList<string>().IndexOf(monthValue); // TODO netter maken
            byte day = byte.Parse(dayValue);
            byte hour = byte.Parse(hourValue);
            byte minute = byte.Parse(minuteValue);
            byte second = byte.Parse(secondValue);
            int year = int.Parse(yearValue);
            TimeStamp = new DateTime(year, month, day, hour, minute, second);
        }

        private static UInt64 extractUInt64(string prefix, string suffix, ref string remaining)
        {
            string value = extractString(prefix, suffix, ref remaining);
            UInt64 result = UInt64.Parse(value);
            return result;
        }

        private static string extractString(string prefix, string suffix, ref string remaining)
        {
            int prefixIndex = remaining.IndexOf(prefix);
            remaining = remaining.Substring(prefixIndex + prefix.Length);
            int suffixIndex = remaining.IndexOf(suffix);
            string result = remaining.Substring(0, suffixIndex);
            return result;
        }
    }

}
