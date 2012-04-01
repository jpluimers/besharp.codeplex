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
                // escape the ampersand as XML uses the & as a special character too: http://stackoverflow.com/questions/1328538/how-do-i-escape-ampersands-in-xml
                // this is mainly to replace the "&nbsp;", but since any & in HTML will kill the XML, replace them all. Reading the text back from get us back the &, so "&amp;nbsp;" will become "&nbsp;" 
                // since WordPress has escaped other HTML sensitive characters too, we will take those in one go.
                string xml = inputHtml.Replace("&", "&amp;");

                selectType select = selectType.Deserialize(xml);
                select.FixParents();

                //dump(select);

                StringBuilder outputHtml = new StringBuilder();
                string rootUrl = args[1];
                foreach (optionType item in select.option)
                {
                    if (item.Level == optionType.RootLevel)
                        continue;

                    // <a href='http://wiert.me/category/development/xmlxsd/' style='font-size: 100.3986332574%; padding: 1px; margin: 1px;' title='XML/XSD (23)'>XML/XSD</a>
                    string url = String.Format("{0}/category{1}", rootUrl, item.HtmlPath);
                    string prefix = new string('.', item.Level * 5);// optionType.NbspEscaped.Repeat(item.Level);
                    outputHtml.AppendFormat("{0}<a href='{1}' style='font-size: 100%; padding: 1px; margin: 1px;' title='{2}'>{2}</a>", prefix, url, item.Category);
                    outputHtml.AppendLine();
                }

                string outputFileName = args.Length > 2 ? args[2] : null;
                Stream outputStream = BaseTextProcessor.GetOutputStream(outputFileName);
                try
                {
                    StreamWriter outputStreamWriter = new StreamWriter(outputStream);
                    outputStreamWriter.Write(outputHtml.ToString());
                    outputStreamWriter.Flush();
                    return 0; // success
                }
                finally
                {
                    //if (args.Length > 2) // outputfilename, do not close the default OutputStream, as the console relies on that
                        outputStream.Close();
                }
            }
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