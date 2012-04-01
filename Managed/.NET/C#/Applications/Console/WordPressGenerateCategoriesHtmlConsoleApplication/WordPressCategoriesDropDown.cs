using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPressGenerateCategoriesHtmlConsoleApplication
{
    partial class selectType
    {
        /// <summary>
        /// build the parent tree for the optionType items in option
        /// </summary>
        public void FixParents()
        {
            Stack<optionType> itemStack = new Stack<optionType>();
            optionType parent = null;
            int previousLevel = -1;

            foreach (optionType item in option)
            {
                int itemLevel = item.Level;
                if (itemLevel == previousLevel)
                {
                    if (optionType.RootLevel != itemLevel)
                    {
                        itemStack.Pop();
                        item.parent = parent;
                    }
                    itemStack.Push(item);
                }
                else
                {
                    if (itemLevel > previousLevel)
                    {
                        parent = itemStack.Peek();
                    }
                    else
                    {
                        do
                        {
                            itemStack.Pop();
                            parent = itemStack.Peek();
                            previousLevel = parent.Level;
                        }
                        while (previousLevel >= itemLevel);
                    }
                    itemStack.Push(item);
                    item.parent = parent;
                    previousLevel = itemLevel;
                }
                //Console.WriteLine("{0}, {1}", item.Value.Replace(optionType.NbspEscaped, " "), item.HtmlPath);
            }
        }
    }

    partial class optionType
    {
        public const int RootLevel = -1;

        private const string slash = "/";
        private const char hyphen = '-';
        public const string NbspEscaped = "&nbsp;";
        private const string emptyCountInParenthesis = "(-1)";

        public optionType parent { get; set; }

        public int Level
        {
            get
            {
                if (string.IsNullOrWhiteSpace(@class))
                    return -1;
                string[] split = @class.Split(hyphen);
                string number = split[1];
                int result = int.Parse(number);
                return result;
            }
        }

        public string Category
        {
            get
            {
                string result;
                string countInParenthesis;
                splitValue(out result, out countInParenthesis);
                return result;
            }
        }

        /// <summary>
        /// This is the HTML part that WordPress uses to reference a Category
        /// </summary>
        public string Slug
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (char item in Category)
                {
                    if (char.IsLetterOrDigit(item))
                        result.Append(item.ToString().ToLower());
                    else
                        if (result.Length > 0)
                            result.Append(hyphen);
                }
                return result.ToString();
            }
        }

        public string HtmlPath
        {
            get
            {
                if (RootLevel == Level)
                    return string.Empty;

                string result = Slug;
                if (null != parent)
                    result = parent.HtmlPath + slash + result;
                return result;
            }
        }

        private void splitValue(out string category, out string countInParenthesis)
        {
            // might want to do this using RegEx, but that is a write-only language http://wiert.me/category/development/software-development/regex/
            string result = Value;
            int nbspCountToStripFromLeftOfValue = Level * 3; // strip 3 &nbsp; for each Level
            for (int i = 0; i < nbspCountToStripFromLeftOfValue; i++)
            {
                int nbspEscapedLength = NbspEscaped.Length;
                if (result.StartsWith(NbspEscaped))
                    result = result.Substring(nbspEscapedLength, result.Length - nbspEscapedLength);
            }
            string doubleNbspEscaped = NbspEscaped + NbspEscaped;
            if (result.Contains(doubleNbspEscaped))
            {
                string[] separators = new string[] { doubleNbspEscaped };
                string[] split = result.Split(separators, StringSplitOptions.None);
                category = split[0];
                countInParenthesis = split[1];
            }
            else
            {
                category = result;
                countInParenthesis = emptyCountInParenthesis;
            }
        }

        public int Count
        {
            get
            {
                string category;
                string countInParenthesis;
                splitValue(out category, out countInParenthesis);
                string count = countInParenthesis.Substring(1, countInParenthesis.Length - 2);
                int result = int.Parse(count);
                return result;
            }
        }

        public override string ToString()
        {
            string result = string.Format("Level {0}, Count {1}, Slug {2}, HtmlPath {3}, Category '{4}'", Level, Count, Slug, HtmlPath, Category);
            return result;
        }
    }
}
