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
            ["pascal"] = "pas",
            ["cpp"] = "cpp",
            ["c"] = "cpp",
            ["csharp"] = "csharp",
        };

        public static bool IsBoldSwitch(string t, int idx)
        {
            return ((idx < t.Length - 1) && (t[idx] == '*') && (t[idx + 1] == '*'));
        }
        public static bool IsItalicSwitch(string t, int idx)
        {
            return (idx < t.Length) && (t[idx] == '*') && (!IsBoldSwitch(t, idx));
        }


        public static bool IsWhiteSpaceOrNone(string t, int idx)
        {
            if ((idx >= t.Length) || (idx < 0))
                return true;
            return char.IsWhiteSpace(t, idx);
        }


        // Replaces ** and * the line of text
        // It's important that openning styling starts next to the characters
        //  correct: **bold
        //  wrong:   ** bold
        // and the same rules applies to italic:
        //   correct: *italic
        //   wrong:   * italic
        // This allow to destiguish between the formatting and bullet lists:
        //  * item 1
        //  * item 2
        //  * *italic item* 3
        public static string ReplaceMarkup(string t, ConvertOpt opt = null)
        {
            if (opt == null)
                opt = ConvertOpt.Default;

            int j = 0;
            int i = 0;
            StringBuilder b = new StringBuilder();
            j = 0;

            bool isBoldOpen = false;
            bool isItalicOpen = false;
            bool isInlineCode = false;

            while (i < t.Length)
            {
                if (t[i] == '*')
                {
                    int nx = i + 2;
                    bool bsw = IsBoldSwitch(t, i);
                    bool isw = false;
                    if (!bsw)
                    {
                        isw = IsItalicSwitch(t, i);
                        if (isw)
                            nx = i + 1;
                    }

                    string app = "";
                    if (bsw && !isBoldOpen && !IsWhiteSpaceOrNone(t, i + 2))
                    {
                        app = "[b]";
                        isBoldOpen = true;
                    }
                    else if (bsw && isBoldOpen && !IsWhiteSpaceOrNone(t, i - 1))
                    {
                        app = "[/b]";
                        isBoldOpen = false;
                    }
                    else if (isw && !isItalicOpen && !IsWhiteSpaceOrNone(t, i + 1))
                    {
                        app = "[i]";
                        isItalicOpen = true;
                    }
                    else if (isw && isItalicOpen && !IsWhiteSpaceOrNone(t, i - 1))
                    {
                        app = "[/i]";
                        isItalicOpen = false;
                    }
                    if (!string.IsNullOrEmpty(app))
                    {
                        b.Append(t, j, i - j);
                        b.Append(app);
                        i = nx;
                        j = i;
                    }
                    else
                        i++;
                }
                else if ((t[i] == '`') && (!isInlineCode) && !IsWhiteSpaceOrNone(t, i + 1))
                {
                    b.Append(t, j, i - j);
                    b.Append(opt.InlineCodeStart);
                    isInlineCode = true;
                    i++;
                    j = i;
                }
                else if ((t[i] == '`') && (isInlineCode) && !IsWhiteSpaceOrNone(t, i - 1))
                {
                    b.Append(t, j, i - j);
                    b.Append(opt.InclineCodeEnd);
                    isInlineCode = false;
                    i++;
                    j = i;
                }
                else
                    i++;
            }

            /*bool isOpen = false;
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
            }*/
            if (j < t.Length) b.Append(t, j, t.Length - j);

            var r = b.ToString();
            switch(opt.ReplaceSpaceBull)
            {
                case OffsetBullReplace.ItemsList:
                    r = ReplaceByItemsList(r);
                    break;
                case OffsetBullReplace.NbSpBull:
                    r = ReplaceWhiteSpaceBull(r);
                    break;
            }
            return r;
        }

        public static string ReplaceWhiteSpaceBull(string r)
        {
            if (string.IsNullOrWhiteSpace(r))
                return r;
            int i = 0;
            while ((i < r.Length)&&(Char.IsWhiteSpace(r, i)))
            {
                i++;
            }
            if (i == 0) return r;
            if (i >= r.Length) return r;
            if (r[i] != '*') return r;
            StringBuilder b = new StringBuilder();
            while (i > 0)
            {
                b.Append("&nbsp;");
                i--;
            }
            b.Append("&bull;");
            i += 2;
            if (i < r.Length)
                b.Append(r, i, r.Length - i);
            return b.ToString();
        }

        public static string ReplaceByItemsList(string r)
        {
            if (string.IsNullOrWhiteSpace(r))
                return r;
            int i = 0;
            while ((i < r.Length) && (Char.IsWhiteSpace(r, i)))
            {
                i++;
            }
            if (i == 0) return r;
            if (i >= r.Length) return r;
            if (r[i] != '*') return r;
            StringBuilder b = new StringBuilder();
            int cnt = i;
            for (int k = 0; k < cnt; k ++)b.Append("<ul>");
            b.Append("<li>");
            i += 2;
            if (i < r.Length)
                b.Append(r, i, r.Length - i);
            b.Append("</li>");
            for (int k = 0; k < cnt; k++) b.Append("</ul>");
            return b.ToString();
        }

        public static string GetGDRunLang(string lang)
        {
            if (mdToGdRuLang.TryGetValue(lang, out var result))
                return result;
            return "";
        }


        // todo: the whitespace offset can be different on start of the code 
        //       and the end of the code. but this requires the "language" section
        //       to be added here
        public static string CodeStart(string text)
        {
            const string code4 = "````";
            const string code3 = "```";

            // the order is important. The longest goes first
            string[] codes = new string[] { code4, code3 };

            int i = 0;
            while ((i < text.Length) && (Char.IsWhiteSpace(text, i)))
                i++;
            string pfx = "";
            if (i > 0) pfx = text.Substring(0, i);

            for (int j = 0; j < codes.Length; j++)
            {
                var c = codes[j];
                if (string.Compare(text, i, c, 0, c.Length) == 0)
                {
                    if (pfx == "") return c;
                    return $"{pfx}{c}";
                }
            }
            return "";
        }

        public static bool IsHeaderStart(string s, out string actualText)
        {
            actualText = "";
            bool result = true;
            if (s.StartsWith("##### "))
                actualText = s.Substring(6, s.Length - 6);
            else if (s.StartsWith("#### "))
                actualText = s.Substring(5, s.Length - 5);
            else if (s.StartsWith("### "))
                actualText = s.Substring(4, s.Length - 4);
            else if (s.StartsWith("## "))
                actualText = s.Substring(3, s.Length - 3);
            else if (s.StartsWith("# "))
                actualText = s.Substring(2, s.Length - 2);
            else
                result = false;
            return result;
        }

        public static bool IsHorzLine(string s)
        {
            if (s == "---") return true;
            if (s == "___") return true;
            if (s == "***") return true;
            return false;
        }

        public static string Convert(string text, ConvertOpt opt = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            if (opt == null)
                opt = ConvertOpt.Default;

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
                    // todo: пока что реализуем таблицу через плашу code
                    //       потому что chatgpt строит таблицу через asci 
                    if (!inTable)
                    {
                        inTable = true;
                        b.AppendLine("code");
                    }
                    t = t.Substring(1);
                }
                else if (inTable)
                {
                    b.AppendLine("[/code]");
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
                else if (IsHeaderStart(t, out var hdrText)) 
                {
                    b.Append("[b]");
                    b.Append(hdrText);
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
                else if (IsHorzLine(t))
                {
                    b.AppendLine(); // extra line break
                    b.Append("<hr>");
                    b.AppendLine(); // and another extra line break
                }
                else
                {
                    t = ReplaceMarkup(t, opt);
                    b.Append(t);
                }

                b.AppendLine();
            }
            return b.ToString();
        }
    }
}
