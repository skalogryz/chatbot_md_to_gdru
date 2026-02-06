using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Md2ChatToGd
{
    public class MarkdownTableParser
    {
        public enum ColumnAlignment
        {
            Left,
            Center,
            Right,
            None
        }

        public class TableCell
        {
            public string Content { get; set; }
            public ColumnAlignment Alignment { get; set; }

            public TableCell(string content, ColumnAlignment alignment = ColumnAlignment.None)
            {
                Content = content?.Trim() ?? string.Empty;
                Alignment = alignment;
            }
        }

        public class Table
        {
            public List<TableCell> Headers { get; set; }
            public List<ColumnAlignment> Alignments { get; set; }
            public List<List<TableCell>> Rows { get; set; }

            public Table()
            {
                Headers = new List<TableCell>();
                Alignments = new List<ColumnAlignment>();
                Rows = new List<List<TableCell>>();
            }
        }

        /// <summary>
        /// Парсит Markdown-таблицу из текста
        /// </summary>
        public static Table ParseTable(string markdown)
        {
            if (string.IsNullOrWhiteSpace(markdown))
                return null;

            var lines = markdown.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(l => l.Trim())
                                .Where(l => !string.IsNullOrWhiteSpace(l))
                                .ToList();
            return ParseTable(lines);
        }
        public static Table ParseTable(List<string> lines)
        { 
            if ((lines == null)||(lines.Count < 2))
                return null; // Минимум: заголовок + разделитель

            var table = new Table();

            // 1. Парсим заголовок
            var headerLine = lines[0];
            if (!IsTableRow(headerLine))
                return null;

            var headerCells = SplitRow(headerLine);
            table.Headers = headerCells.Select(c => new TableCell(c)).ToList();

            // 2. Парсим разделитель (alignment)
            if (lines.Count < 2)
                return null;

            var separatorLine = lines[1];
            if (!IsSeparatorRow(separatorLine))
                return null;

            table.Alignments = ParseAlignments(separatorLine);

            // Применяем выравнивание к заголовкам
            for (int i = 0; i < table.Headers.Count && i < table.Alignments.Count; i++)
            {
                table.Headers[i].Alignment = table.Alignments[i];
            }

            // 3. Парсим строки данных
            for (int i = 2; i < lines.Count; i++)
            {
                var rowLine = lines[i];
                if (!IsTableRow(rowLine))
                    continue;

                var cells = SplitRow(rowLine);
                var row = new List<TableCell>();

                for (int j = 0; j < cells.Count; j++)
                {
                    var alignment = j < table.Alignments.Count
                        ? table.Alignments[j]
                        : ColumnAlignment.None;
                    row.Add(new TableCell(cells[j], alignment));
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Проверяет, является ли строка строкой таблицы
        /// </summary>
        private static bool IsTableRow(string line)
        {
            return line.Contains('|');
        }

        /// <summary>
        /// Проверяет, является ли строка разделителем (alignment row)
        /// </summary>
        private static bool IsSeparatorRow(string line)
        {
            // Разделитель содержит только |, -, : и пробелы
            var cleaned = line.Replace("|", "").Replace("-", "").Replace(":", "").Replace(" ", "");
            return cleaned.Length == 0 && line.Contains('-');
        }

        /// <summary>
        /// Разбивает строку таблицы на ячейки
        /// </summary>
        private static List<string> SplitRow(string line)
        {
            // Убираем крайние |
            line = line.Trim();
            if (line.StartsWith("|"))
                line = line.Substring(1);
            if (line.EndsWith("|"))
                line = line.Substring(0, line.Length - 1);

            // Разбиваем по | (с учётом экранирования \|)
            var cells = new List<string>();
            var currentCell = new StringBuilder();
            bool escaped = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (escaped)
                {
                    currentCell.Append(c);
                    escaped = false;
                }
                else if (c == '\\' && i + 1 < line.Length && line[i + 1] == '|')
                {
                    escaped = true;
                }
                else if (c == '|')
                {
                    cells.Add(currentCell.ToString());
                    currentCell.Clear();
                }
                else
                {
                    currentCell.Append(c);
                }
            }

            cells.Add(currentCell.ToString());

            return cells;
        }

        /// <summary>
        /// Парсит выравнивание из строки-разделителя
        /// </summary>
        private static List<ColumnAlignment> ParseAlignments(string separatorLine)
        {
            var cells = SplitRow(separatorLine);
            var alignments = new List<ColumnAlignment>();

            foreach (var cell in cells)
            {
                var trimmed = cell.Trim();
                bool startsWithColon = trimmed.StartsWith(":");
                bool endsWithColon = trimmed.EndsWith(":");

                if (startsWithColon && endsWithColon)
                    alignments.Add(ColumnAlignment.Center);
                else if (endsWithColon)
                    alignments.Add(ColumnAlignment.Right);
                else if (startsWithColon)
                    alignments.Add(ColumnAlignment.Left);
                else
                    alignments.Add(ColumnAlignment.Left); // По умолчанию
            }

            return alignments;
        }


        private static string nochange(string src)
        {
            return src;

        }
        /// <summary>
        /// Конвертирует таблицу в HTML
        /// </summary>
        public static string ToHtml(Table table, Func<string, string> contentParser, bool prettyPrint = false)
        {
            if (table == null)
                return string.Empty;

            if (contentParser == null)
                contentParser = nochange;

            var indent = prettyPrint ? "  " : "";
            var newline = prettyPrint ? "\n" : "\n";
            var html = new StringBuilder();

            html.Append($"<table style=\"border-collapse: collapse;\">{newline}");

            // Заголовок
            if (table.Headers.Count > 0)
            {
                // gd.ru doesn't support thead
                //html.Append($"{indent}<thead>{newline}");
                html.Append($"{indent}{indent}<tr>{newline}");

                foreach (var header in table.Headers)
                {
                    var align = GetAlignmentAttribute(header.Alignment);
                    var content = contentParser(header.Content);
                    html.Append($"{indent}{indent}{indent}<th{align}>{content}</th>{newline}");
                }

                html.Append($"{indent}{indent}</tr>{newline}");

                // gd.ru doesn't support
                //html.Append($"{indent}</thead>{newline}");
            }

            // Тело таблицы
            if (table.Rows.Count > 0)
            {
                html.Append($"{indent}<tbody>{newline}");

                foreach (var row in table.Rows)
                {
                    html.Append($"{indent}{indent}<tr>{newline}");

                    foreach (var cell in row)
                    {
                        var align = GetAlignmentAttribute(cell.Alignment);
                        var content = contentParser(cell.Content);
                        html.Append($"{indent}{indent}{indent}<td{align}>{content}</td>{newline}");
                    }

                    html.Append($"{indent}{indent}</tr>{newline}");
                }

                html.Append($"{indent}</tbody>{newline}");
            }

            html.Append("</table>");

            return html.ToString();
        }

        /// <summary>
        /// Получает HTML-атрибут для выравнивания
        /// </summary>
        private static string GetAlignmentAttribute(ColumnAlignment alignment)
        {
            switch (alignment)
            {
                case ColumnAlignment.Left:
                    //return " style=\"text-align: left;\"";
                    return "";
                case ColumnAlignment.Center:
                    return " style=\"text-align: center;\"";
                case ColumnAlignment.Right:
                    return " style=\"text-align: right;\"";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Экранирует HTML-специальные символы
        /// </summary>
        private static string EscapeHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Replace("&", "&amp;")
                        .Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("\"", "&quot;")
                        .Replace("'", "&#39;");
        }
    }

}
