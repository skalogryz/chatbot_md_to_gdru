using System;
using System.Collections.Generic;
using System.Text;

namespace Md2ChatToGd
{
    public static class MdToGdRu
    {
        public static Dictionary<string, string> mdToGdRuLang = new Dictionary<string, string>
        {
            ["python"] = "py",
            ["php"] = "php",
            ["javascript"] = "js",
            ["pascal"] = "pas"
        };
        public static string ReplaceMarkup(string t)
        {
            int j = 0;
            int i = 0;
            StringBuilder b = new StringBuilder();
            j = 0;
            bool isOpen = false;
            i = t.IndexOf("**");
            if (i < 0) 
                return t;

            while (i >= 0)
            {
                b.Append(t, j, i - j);
                isOpen = !isOpen;
                if (isOpen)
                    b.Append("[b]");
                else
                    b.Append("[/b]");
                i += 2;
                j = i;
                i = t.IndexOf("**", i);
            }
            if (j < t.Length) b.Append(t, j, t.Length - j);
            return b.ToString();
        }

        public static string GetGDRunLang(string lang)
        {
            if (mdToGdRuLang.TryGetValue(lang, out var result))
                return result;
            return "";
        }

        public static string CodeStart(string text)
        {
            if (text.StartsWith("````"))
                return "````";
            if (text.StartsWith("```"))
                return "```";
            return "";
        }

        public static string Convert(string text)
        {
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            bool inQuote = false;
            bool inCode = false;
            string codeStarted = ""; // can be either ``` or ```` (or more?)
            bool inTable = false;
            bool inFormula = false;
            StringBuilder b = new StringBuilder();


            foreach(var l in lines)
            {
                string t = l;
                var codest = CodeStart(t);
                if (inCode)
                {
                    if (codest == codeStarted)
                    {
                        b.AppendLine("[/code]");
                        inCode = false;
                    }
                    else
                        b.AppendLine(t);
                    continue;
                }
                if (inFormula)
                {
                    if (t == "]")
                    {
                        b.AppendLine("[/cht]");
                        inFormula = false;
                    }
                    else b.AppendLine(t);
                    continue;
                }

                if (t.StartsWith(">"))
                {
                    if (!inQuote)
                    {
                        b.AppendLine("[quote]");
                        inQuote = true;
                    }
                    t = t.Substring(1);
                }
                else if (inQuote)
                {
                    b.AppendLine("[/quote]");
                    inQuote = false;
                }

                if (t.StartsWith("|"))
                {
                    if (!inTable)
                    {
                        inTable = true;
                    }
                    t = t.Substring(1);
                }
                else if (inTable)
                {
                    b.AppendLine("[/table]");
                    inTable = false;
                }


                // deep seek formulas
                if (t.StartsWith("$$") && t.EndsWith("$$") && (t.Length > 4))
                {
                    b.Append("[cht]");
                    b.Append(t.Substring(2, t.Length - 4));
                    b.Append("[/cht]");
                }
                else if (t == "[") // chatgpt forumals
                {
                    inFormula = true;
                }
                else if (t.StartsWith("###"))
                {
                    b.Append("[b]");
                    b.Append(t, 3, t.Length - 3);
                    b.Append("[/b]");
                }
                else if (t.StartsWith("##"))
                {
                    b.Append("[b]");
                    b.Append(t, 2, t.Length - 2);
                    b.Append("[/b]");
                }
                else if (!string.IsNullOrEmpty(codest) && !inCode)
                {
                    inCode = true;
                    codeStarted = codest;
                    string lang = t.Substring(codest.Length);
                    lang = GetGDRunLang(lang);

                    b.Append("[code");
                    if (!string.IsNullOrEmpty(lang))
                    {
                        b.Append("=");
                        b.Append(lang);
                    }
                    b.Append("]");
                    //b.AppendLine(t);
                }
                else
                {
                    t = ReplaceMarkup(t);
                    b.Append(t);
                }

                b.AppendLine();
            }
            return b.ToString();
        }
    }
}
