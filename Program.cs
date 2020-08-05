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
            Generator generator = new Generator(reader);
            while (true)
            {
                Console.ReadLine();
                string text = generator.GetRandomText("S", 5);
                Console.WriteLine(text);
            }
        }
    }
}
