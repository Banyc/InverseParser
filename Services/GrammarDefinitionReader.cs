using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InverseParser.Services
{
    public class GrammarDefinitionReader
    {
        /// <summary>
        /// Dictionary<
        ///     string left-hand-side nonterminal, 
        ///     List<
        ///         string terminal
        ///     > derivation
        /// >
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GrammaticalDerivations { get; } = new Dictionary<string, List<string>>();
        /// <summary>
        /// Dictionary<
        ///     string left-hand-side nonterminal, 
        ///     List<
        ///         List<
        ///             string nonterminal
        ///         > derivation
        ///     > maxterms
        /// >
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<List<string>>> LexicalDerivations { get; } = new Dictionary<string, List<List<string>>>();

        // pre-order
        public void ReadGrammar(string rootDirectory = "./")
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(rootDirectory);
            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                if (file.Name.EndsWith(".gg"))
                {
                    ReadGrammatical(file.FullName);
                    continue;
                }
                if (file.Name.EndsWith(".gl"))
                {
                    ReadLexical(file.FullName);
                    continue;
                }
            }
            foreach (DirectoryInfo subDirectory in currentDirectory.GetDirectories())
            {
                ReadGrammar(subDirectory.FullName);
            }
        }

        private void ReadGrammatical(string filePath)
        {
            string fullText = File.ReadAllText(filePath);
            List<string> lines = new List<string>(fullText.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ));
            foreach (string line in lines)
            {
                string cleanLine = RemoveComment(line);
                List<string> oneLineOfNonTerminals = new List<string>(
                    cleanLine.Split()
                );
                if (oneLineOfNonTerminals.Count == 0)
                {
                    continue;
                }
                if (!this.LexicalDerivations.ContainsKey(oneLineOfNonTerminals[0]))
                {
                    this.LexicalDerivations[oneLineOfNonTerminals[0]] = new List<List<string>>();
                }
                this.LexicalDerivations[oneLineOfNonTerminals[0]].Add(oneLineOfNonTerminals.GetRange(1, oneLineOfNonTerminals.Count - 1));
            }
        }

        private void ReadLexical(string filePath)
        {
            string fullText = File.ReadAllText(filePath);
            List<string> lines = new List<string>(fullText.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ));
            string currentTerminal = null;
            foreach (string line in lines)
            {
                string cleanLine = RemoveComment(line);
                if (cleanLine?.Length == 0)
                {
                    currentTerminal = null;
                    continue;
                }
                if (currentTerminal == null)
                {
                    currentTerminal = cleanLine;
                    this.GrammaticalDerivations[currentTerminal] = new List<string>();
                    continue;
                }
                this.GrammaticalDerivations[currentTerminal].Add(cleanLine);
            }
        }

        private string RemoveComment(string str)
        {
            string newString = str;
            int index = str.IndexOf('#');
            if (index >= 0)
            {
                newString = str.Take(index).ToString();
            }
            return newString;
        }
    }
}
