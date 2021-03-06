﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text;

namespace FASTLogToXml
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                string path;
                const string doubleQuote = "\""; // case someone passes the parameter without ending double-quote as: "C:\Users\developer\Dropbox\Shared-Talon\Downloads
                if (arg.EndsWith(@"\") || (arg.EndsWith(doubleQuote) && !arg.StartsWith(doubleQuote)))
                    path = arg.Substring(0, arg.Length - 1);
                else
                    path = arg;
                if (Directory.Exists(path))
                {
                    processDirectory(path);
                }
                else if (File.Exists(arg))
                {
                    processFile(arg);
                }
                else
                {
                    Console.WriteLine("Skipped, as it is not a file or directory: {0}", arg);
                }
            }
        }

        static void processDirectory(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileInfo[] logFilePaths = directoryInfo.GetFiles("*.log");
            foreach (FileInfo logFilePath in logFilePaths)
            {
                processFile(logFilePath.FullName);
            }
        }

        static void processFile(string filePath)
        {
            Console.WriteLine("Processing log file {0}", filePath);
            List<LogEntry> logEntries = parseLogEntries(filePath);

            string logEntriesXmlFilePath = Path.ChangeExtension(filePath, ".xml");
            Console.WriteLine("Exporting to XML file {0}", logEntriesXmlFilePath);

            serializeToXml(logEntries, logEntriesXmlFilePath);
            logEntries = deserializeFromXml(logEntriesXmlFilePath);

            string logEntriesDelimitedFilePath = Path.ChangeExtension(filePath, ".PipeDelimited.txt");
            Console.WriteLine("Exporting to Pipe (|) Delimited file {0}", logEntriesDelimitedFilePath);

            StringBuilder pipeDelimited = new StringBuilder();
            pipeDelimited.AppendFormatLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}", "TimeStamp", "FileSizeBytes", "TransferredBytes", "ExchangedBytes", "TransferEfficiencyPercentage", "FileExtension", "FilePath");
            foreach (LogEntry logEntry in logEntries)
            {
                pipeDelimited.AppendFormatLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}", logEntry.TimeStamp, logEntry.FileSizeBytes, logEntry.TransferredBytes, logEntry.ExchangedBytes, logEntry.TransferEfficiencyPercentage, logEntry.FileExtension, logEntry.FilePath);
            }
            File.WriteAllText(logEntriesDelimitedFilePath, pipeDelimited.ToString());

            string logEntriesByExtensionDelimitedFilePath = Path.ChangeExtension(filePath, ".ByExtension.PipeDelimited.txt");
            Console.WriteLine("Exporting summary by extension to Pipe (|) Delimited file {0}", logEntriesDelimitedFilePath);

            IEnumerable<IGrouping<string, LogEntry>> extensionGroups = from item in logEntries
                                                                       group item by item.FileExtension.ToLower();

            var extensionSummaries = from grouping in extensionGroups
                                     select new
                                         {
                                             Extension = grouping.Key,
                                             Count = grouping.Count(),
                                             // there is no Sum for ulong/UInt64, so get it from our Extension
                                             SumFileSizeBytes = grouping.Sum(logEntry => logEntry.FileSizeBytes),
                                             SumExchangedBytes = grouping.Sum(logEntry => logEntry.ExchangedBytes),
                                             SumTransferredBytes = grouping.Sum(logEntry => logEntry.TransferredBytes)
                                         };

            StringBuilder extensionSummariesPipeDelimited = new StringBuilder();

            extensionSummariesPipeDelimited.AppendFormatLine("{0}|{1}|{2}|{3}|{4}", "Extension", "Count", "SumFileSizeBytes", "SumExchangedBytes", "SumTransferredBytes");
            foreach (var extensionSummary in extensionSummaries)
            {
                extensionSummariesPipeDelimited.AppendFormatLine("{0}|{1}|{2}|{3}|{4}", extensionSummary.Extension, extensionSummary.Count, extensionSummary.SumFileSizeBytes, extensionSummary.SumExchangedBytes, extensionSummary.SumTransferredBytes);
            }
            File.WriteAllText(logEntriesByExtensionDelimitedFilePath, extensionSummariesPipeDelimited.ToString());

            /*
# APPLIANCE ID: LON-BRIAN1
# LOG FILES ARE ROTATED EVERY 8 HOURS
# (SINCE THE LAST SYSTEM RESTART) OR
# EVERY 1MB, WHICHEVER HAPPENS EARLIER


# APPLIANCE ID: LON-BRIAN1
# LOG FILES ARE ROTATED EVERY 8 HOURS
# (SINCE THE LAST SYSTEM RESTART) OR
# EVERY 1MB, WHICHEVER HAPPENS EARLIER

(Fri Sep 14 07:41:05 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\build-49\Appliance-Install\release\Talon-0-9-0-0-49-WinLH-x64-release.exe File Size[9882132] Bytes Transferred[9885770] Bytes Exchanged[9863796] Transfer Efficiency[0.185547]


# APPLIANCE ID: LON-BRIAN1
# LOG FILES ARE ROTATED EVERY 8 HOURS
# (SINCE THE LAST SYSTEM RESTART) OR
# EVERY 1MB, WHICHEVER HAPPENS EARLIER

(Fri Sep 14 08:00:32 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\127.0.0.1\FASTShare$\Scheduler\policydb.xml File Size[707] Bytes Transferred[707] Bytes Exchanged[707] Transfer Efficiency[0]

(Fri Sep 14 08:16:43 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Build_1.0\NDIS\WNET_X64\netim.inf File Size[3702] Bytes Transferred[3702] Bytes Exchanged[3702] Transfer Efficiency[0]

(Fri Sep 14 08:16:54 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Marketing\Thumbs.db File Size[20480] Bytes Transferred[20506] Bytes Exchanged[16850] Transfer Efficiency[17.7246]

(Fri Sep 14 08:31:34 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Howto Use Talon Log Parser.txt File Size[715] Bytes Transferred[715] Bytes Exchanged[715] Transfer Efficiency[0]

(Fri Sep 14 08:31:53 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Development\Talon Storage Solutions - Log Parser v1.0.zip File Size[310697] Bytes Transferred[310828] Bytes Exchanged[309241] Transfer Efficiency[0.468624]

(Fri Sep 14 08:33:07 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Thumbs.db File Size[33280] Bytes Transferred[33312] Bytes Exchanged[16359] Transfer Efficiency[50.8444]

(Fri Sep 14 08:33:19 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 User Guide.docx File Size[1675196] Bytes Transferred[1675828] Bytes Exchanged[1547453] Transfer Efficiency[7.62556]

(Fri Sep 14 08:33:20 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 User Guide.docx File Size[162] Bytes Transferred[162] Bytes Exchanged[162] Transfer Efficiency[0]

(Fri Sep 14 08:33:48 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\Talon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx File Size[1674533] Bytes Transferred[1675165] Bytes Exchanged[1546799] Transfer Efficiency[7.62804]

(Fri Sep 14 08:33:50 2012)\tInfo\tFrom Datacenter Default T:\AMS-TSM1\ams-fsrv1\TalonStorage\Products\FAST\Guides\~$lon FAST 1.0 Quick Start Guide updated by Jaap Vers2.docx File Size[162] Bytes Transferred[162] Bytes Exchanged[162] Transfer Efficiency[0]
             */
        }

        private static List<LogEntry> parseLogEntries(string filePath)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                int lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (LogEntry.IsValid(line))
                        {
                            LogEntry logEntry = new LogEntry(line);
                            if (logEntry.IsValid())
                                logEntries.Add(logEntry);
                            else
                                Console.WriteLine("Line {0} produces invalid logEntry: {1}{2}", lineNumber, Environment.NewLine, line);
                        }
                        else
                            Console.WriteLine("Line {0} is invalid: {1}{2}", lineNumber, Environment.NewLine, line);
                    }
                    lineNumber++;
                }
            }
            return logEntries;
        }

        private static List<LogEntry> deserializeFromXml(string logEntriesXmlFilePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<LogEntry>));
            TextReader textReader = new StreamReader(logEntriesXmlFilePath);
            List<LogEntry> result;
            result = (List<LogEntry>)deserializer.Deserialize(textReader);
            textReader.Close();

            return result;
        }

        private static void serializeToXml(List<LogEntry> logEntries, string logEntriesXmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LogEntry>));
            using (TextWriter textWriter = new StreamWriter(logEntriesXmlFilePath))
            {
                serializer.Serialize(textWriter, logEntries);
                textWriter.Close();
            }
        }
    }
}
