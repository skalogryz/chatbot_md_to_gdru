using System;
using System.Collections.Generic;
using System.Text;

namespace Md2ChatToGd
{
    public class ConvertOpt
    {
        public string InlineCodeStart = @"<span style=""font-family:monospace;font-size:initial;"">";
        public string InclineCodeEnd = "</span>";

        public static ConvertOpt Default = new ConvertOpt();

        public static ConvertOpt NoChange = new ConvertOpt
        {
            InlineCodeStart = "`",
            InclineCodeEnd = "`"
        };
    }
}
