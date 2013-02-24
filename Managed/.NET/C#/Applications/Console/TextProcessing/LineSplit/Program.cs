using System;
using System.IO;
using System.Text;

namespace LineSplit
{
    class Program
    {
        const int bufferSize = BeSharp.Numerics.UnitPrefixes.MiB;

        static void Main(string[] args)
        {
            try
            {
                if (2 > args.Length)
                {
                    help();
                }
                else
                {
                    Int64 maxLineCount = Int64.Parse(args[0]);
                    string fileName = args[1];

                    // simpler, but does not have a buffersize:
                    //using (StreamReader reader = File.OpenText(fileName))
                    using (Stream readStream = File.OpenRead(fileName))
                    {
                        using (StreamReader reader = new StreamReader(readStream, Encoding.ASCII, true, bufferSize))
                        {
                            GenerateOutput(maxLineCount, fileName, reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                help();
                Console.WriteLine(ex);
            }
        }

        private static void GenerateOutput(Int64 maxLineCount, string fileName, StreamReader reader)
        {
            int totalLineCount = 0;
            int fileCount = 1;

            Encoding encoding = reader.CurrentEncoding;
            WriteLineOutput output = new WriteLineOutput(fileName, fileCount, encoding, bufferSize);

            try
            {
                string line = null;
                while (null != (line = reader.ReadLine())) // convoluted: assignment and null check in one expression, but easier to manage
                {
                    totalLineCount++;
                    output.WriteLine(line);
                    if (output.lineCount >= maxLineCount)
                    {
                        output.Dispose();
                        fileCount++;
                        output = new WriteLineOutput(fileName, fileCount, encoding, bufferSize);
                    }
                }

            }
            finally
            {
                output.Dispose();
            }

            Console.WriteLine("{0} lines in '{1}'", totalLineCount, fileName);
        }

        private static void help()
        {
            // need to find a way to get rid of the "vshost" part somehow.
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            string currentProcessMainModuleFileName = currentProcess.MainModule.FileName;
            Console.WriteLine("Syntax: {0}{1}{2} maxLineCount fileName", Path.GetDirectoryName(currentProcessMainModuleFileName), currentProcess.ProcessName, Path.GetExtension(currentProcessMainModuleFileName));
        }

        class WriteLineOutput : IDisposable
        {
            protected bool Disposed { get; private set; }
            public int lineCount { get; private set; }
            protected string writeFileName { get; private set; }
            protected Stream writeStream { get; private set; }
            protected StreamWriter writer { get; private set; }

            /// <summary>
            /// Initializes a new instance of the WriteLineOutput class.
            /// </summary>
            public WriteLineOutput(string fileName, int fileCount, Encoding encoding, int bufferSize)
            {
                writeFileName = getWriteFileName(fileName, fileCount);
                writeStream = File.Create(writeFileName);
                writer = new StreamWriter(writeStream, encoding, bufferSize);
                lineCount = 0;
            }

            private static string getWriteFileName(string fileName, int fileCount)
            {
                return string.Format("{0}.{1}", fileName, fileCount);
            }

            public void WriteLine(string line)
            {
                writer.WriteLine(line);
                lineCount++;
            }

            private void Dispose(bool disposing)
            {
                if (!Disposed)
                {
                    if (disposing)
                    {
                        Console.WriteLine("{0} lines written to '{1}'", lineCount, writeFileName); // ugly, but saves 2 linesplit in the Program class
                        if (null != writer)
                        {
                            writer.Dispose();
                            writer = null;
                        }
                        if (null != writeStream)
                        {
                            writeStream.Dispose();
                            writeStream = null;
                        }
                    }

                    Disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~WriteLineOutput()
            {
                Dispose(false);
            }
        }

    }
}
