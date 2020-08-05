using System;
using InverseParser.Services;

namespace InverseParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GrammarDefinitionReader reader = new GrammarDefinitionReader();
            TextGenerator generator = new TextGenerator(reader);
            while (true)
            {
                Console.ReadLine();
                string text = generator.GetRandomText("S", 10);
                Console.WriteLine(text);
            }
        }
    }
}
