using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BeSharp;

namespace WordPressGenerateCategoriesHtmlConsoleApplication
{
    class Program : ConsoleProgram
    {
        protected void help()
        {
            log("Syntax: {0} categoriesSelectHtmlFileName WordPressBlogRootUrl [outputFileName]", ExecutableName);
            log("From the HTML that files Categories combobox, it will generate output with");
            log("- the category hierchy as text, and");
            log("- a full page with an HTML tree linking to the various category pages");
        }

        protected override int logic(string[] args)
        {
            if (2 > args.Length)
            {
                help();
                return -1;
            }
            else
            {
                string inputFileName = args[0];
                string inputHtml = getHtml(inputFileName);
                string xml = toXml(inputHtml);
                selectType select = selectType.Deserialize(xml);
                select.FixParents();

                //dump(select);

                string rootUrl = args[1];
                StringBuilder outputHtml = new StringBuilder();

                addOptionsAsHtmlTree(select, rootUrl, outputHtml);

                string outputFileName = args.Length > 2 ? args[2] : null;

                save(outputHtml, outputFileName);

                List<optionType> options = select.option; // even more readable: use an intermediate variable
                IEnumerable<int> counts = from option in options select option.Count;
                double maxCount = counts.Max(); // aggregating in LINQ http://msdn.microsoft.com/en-us/library/bb386914.aspx http://msdn.microsoft.com/en-us/vstudio/bb737922

                Console.WriteLine("Max;Min;Average;{0};{1};{2}", maxCount, counts.Min(), counts.Average());

                foreach (optionType item in select.option)
                {
                    // goal is to end up with a good distribution of HTML font sizes between 1 and 7 (both inclusive) http://www.w3schools.com/tags/att_font_size.asp
                    const int minHtmlFontSize = 1;
                    const int maxHtmlFontSize = 7;
                    double log10CountPlus2 = Math.Log10(item.Count + 2); // add 2 so Log10 won't bail on 0 or -1
                    double relativeCount = Math.Abs(item.Count / maxCount); // range 0 to 1
                    double relativeCount7 = minHtmlFontSize + (maxHtmlFontSize - minHtmlFontSize) * relativeCount; // scale from 1 to 7
                    double relativeCount10000 = 10000 * relativeCount;
                    double log10RelativeCount = Math.Log10(relativeCount10000); // range 1.3 to 4
                    double log10RelativeCount175 = (4 / maxHtmlFontSize) * log10RelativeCount; // scale from 2.3 to 7
                    double log10Log10RelativeCount = Math.Log10(log10RelativeCount); // range 0.1 to 0.6
                    double log10Log10RelativeCount11 = 11 * log10Log10RelativeCount; // scale from 1.3 to 7

                    //Console.WriteLine("{0};{1};{2};{3};{4}", item.Count, log10CountPlus2, relativeCount7, log10RelativeCount175, log10Log10RelativeCount11);
                    int fontSize = Convert.ToInt32(log10Log10RelativeCount11);
                    Console.WriteLine("{0};{1};{2};{3};{4}", item.Count, Convert.ToInt32(log10CountPlus2), Convert.ToInt32(relativeCount7), Convert.ToInt32(log10RelativeCount175), fontSize);

                    // later we will calculate the CSS font size %: http://www.w3schools.com/cssref/pr_font_font-size.asp

                }
                outputHtml.AppendLine("<div>");
                foreach (optionType item in select.option)
                {
                    if (item.Level == optionType.RootLevel)
                        continue;

                    // goal is to end up with a good distribution of HTML font sizes between 1 and 7 (both inclusive) http://www.w3schools.com/tags/att_font_size.asp
                    // later we will calculate the CSS font size %: http://www.w3schools.com/cssref/pr_font_font-size.asp

                    double relativeCount = Math.Abs(item.Count / maxCount); // range 0 to 1
                    double relativeCount10000 = 10000 * relativeCount;
                    double log10RelativeCount = Math.Log10(relativeCount10000); // range 1.3 to 4
                    double log10Log10RelativeCount = Math.Log10(log10RelativeCount); // range 0.1 to 0.6
                    double log10Log10RelativeCount11 = 11 * log10Log10RelativeCount; // scale from 1.3 to 7

                    int fontSize = Convert.ToInt32(log10Log10RelativeCount11);  // Convert.ToInt32() will round: http://msdn.microsoft.com/en-us/library/ffdk7eyz.aspx

                    // <a href='http://wiert.me/category/development/xmlxsd/' style='font-size: 100.3986332574%; padding: 1px; margin: 1px;' title='XML/XSD (23)'>XML/XSD</a>
                    string url = String.Format("{0}/category{1}", rootUrl, item.HtmlPath);
                    string prefix = new string('.', item.Level * 5);// optionType.NbspEscaped.Repeat(item.Level);
                    outputHtml.AppendFormat("{0}<a href='{1}' title='{2}'><font size='{5}'>{2}</font>{3}({4})</a>", prefix, url, item.Category, optionType.NbspEscaped, item.Count, fontSize);
                    outputHtml.AppendLine();
                }
                outputHtml.AppendLine("</div>");

                save(outputHtml, outputFileName);

                //exportOptionsAsCSV(select);

                return 0; // success
            }
        }

        private static void save(StringBuilder outputHtml, string outputFileName)
        {
            Stream outputStream = BaseTextProcessor.GetOutputStream(outputFileName);
            try
            {
                StreamWriter outputStreamWriter = new StreamWriter(outputStream);
                outputStreamWriter.Write(outputHtml.ToString());
                outputStreamWriter.Flush();
            }
            finally
            {
                //if (args.Length > 2) // outputfilename, do not close the default OutputStream, as the console relies on that
                outputStream.Close();
            }
        }

        private static void addOptionsAsHtmlTree(selectType select, string rootUrl, StringBuilder outputHtml)
        {
            outputHtml.AppendLine("<div>");
            foreach (optionType item in select.option)
            {
                if (item.Level == optionType.RootLevel)
                    continue;

                // <a href='http://wiert.me/category/development/xmlxsd/' style='font-size: 100.3986332574%; padding: 1px; margin: 1px;' title='XML/XSD (23)'>XML/XSD</a>
                string url = String.Format("{0}/category{1}", rootUrl, item.HtmlPath);
                string prefix = new string('.', item.Level * 5);// optionType.NbspEscaped.Repeat(item.Level);
                outputHtml.AppendFormat("{0}<a href='{1}' title='{2}'>{2}{3}({4})</a>", prefix, url, item.Category, optionType.NbspEscaped, item.Count);
                outputHtml.AppendLine();
            }
            outputHtml.AppendLine("</div>");
        }

        private static void exportOptionsAsCSV(selectType select)
        {
            // IEnumerable<int> counts = from option in select.option select option.Count; // doesn't compile, since LINQ uses the select keyword
            // IEnumerable<int> counts = from option in @select.option select option.Count; // compiles because of the @ escape

            List<optionType> options = select.option; // even more readable: use an intermediate variable
            IEnumerable<int> counts = from option in options select option.Count;

            Console.WriteLine("Max;Min;Average;{0};{1};{2}", counts.Max(), counts.Min(), counts.Average());

            foreach (optionType item in select.option)
            {
                Console.WriteLine("{0};{1};{2};{3};{4}", item.Level, item.Count, item.Category, item.Slug, item.HtmlPath);
            }
        }

        private static string toXml(string inputHtml)
        {
            // escape the ampersand as XML uses the & as a special character too: http://stackoverflow.com/questions/1328538/how-do-i-escape-ampersands-in-xml
            // this is mainly to replace the "&nbsp;", but since any & in HTML will kill the XML, replace them all. Reading the text back from get us back the &, so "&amp;nbsp;" will become "&nbsp;" 
            // since WordPress has escaped other HTML sensitive characters too, we will take those in one go.
            string result = inputHtml.Replace("&", "&amp;");
            return result;
        }

        private static void dump(selectType select)
        {
            Console.WriteLine("item.Level, item.Count, Slug, HtmlPath, Category");
            foreach (optionType item in select.option)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", item.Level, item.Count, item.Slug, item.HtmlPath, item.Category);
            }
        }

        private static string getHtml(string inputFileName)
        {
            // using will close input, as it is opened from a filename
            using (Stream inputStream = BaseTextProcessor.GetInputStream(inputFileName))
            {
                StreamReader inputStreamReader = new StreamReader(inputStream);
                string html = inputStreamReader.ReadToEnd();
                return html;
            }
        }

        static void Main(string[] args)
        {
#if DEBUG
            new Program().debugMain(args);
#else
            new Program().regularMain(args);
#endif
        }
    }
}