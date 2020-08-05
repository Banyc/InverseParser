using System;
using InverseParser.Services;

namespace InverseParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GrammarDefinitionReader reader = new GrammarDefinitionReader
            {
                RootDirectory = "./grammar"
            };
            TextGenerator generator = new TextGenerator(reader);
            while (true)
            {
                Console.ReadLine();
                string text;
                do
                {
                    text = generator.GetRandomText("S", 50);
                } while (text == null);
                Console.WriteLine(text);
            }
        }
    }
}
