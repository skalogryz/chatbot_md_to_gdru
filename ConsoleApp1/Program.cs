using System;
using System.IO;
using static Md2ChatToGd.MdToGdRu;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("please provide the file with chatgpc export");
                return;
            }
            string fn = args[0];
            var text = File.ReadAllText(fn);
            text = Convert(text);
            File.WriteAllText(fn+".gdru", text);
        }
    }
}
