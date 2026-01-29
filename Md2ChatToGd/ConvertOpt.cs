using System;
using System.Collections.Generic;
using System.Text;

namespace Md2ChatToGd
{
    // замена случаев, когда перед звёздочкой идут пробелы
    public enum OffsetBullReplace
    { 
        // ничего не делает (без замены)
        // в гд.ру будет выглядеть как пробелы и звёздочка.
        // - вложенные списки смотрятся плохо
        None,

        // заменяет пробелы на &nbsp; а звёздочку на &bull;
        // + мало букаф
        // - звёздочка визуально отличается, от того, которую подставляет браузер для списка
        // - размер отсутпа не совпадает с предлагаемым браузером отсутпом
        NbSpBull,
        
        // Использует "ul" (для отступов) + "li" (для галочки)
        // + Результат получается визуально совместимым с gd.ru, потому что gd.ru создаёт список 
        //   но только, если звёзочка стоит первой списке
        // - многа букаф
        ItemsList
    }

    public class ConvertOpt
    {
        public string InlineCodeStart = @"<span style=""font-family:monospace;font-size:initial;"">";
        public string InclineCodeEnd = "</span>";

        public OffsetBullReplace ReplaceSpaceBull = OffsetBullReplace.ItemsList;

        public static ConvertOpt Default = new ConvertOpt();

        public static ConvertOpt NoChange = new ConvertOpt
        {
            InlineCodeStart = "`",
            InclineCodeEnd = "`",
            ReplaceSpaceBull = OffsetBullReplace.None
        };
    }
}
