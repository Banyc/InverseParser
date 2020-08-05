using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System;
using InverseParser.Models;

namespace InverseParser.Services
{
    public class TextGenerator
    {
        private readonly GrammarModel _grammar;
        private readonly Random _random = new Random();

        public TextGenerator(GrammarDefinitionReader reader)
        {
            reader.ReadGrammar();
            _grammar = reader.Grammar;
        }

        // post-order tree traversal
        public string GetRandomText(string grammarRoot, int maxDepth)
        {
            // run out of depth
            if (maxDepth == 0)
            {
                return null;
            }
            // `grammarRoot` is a terminal
            if (!_grammar.LexicalDerivations.ContainsKey(grammarRoot))
            {
                // `grammarRoot` is a literal
                if (!_grammar.GrammaticalDerivations.ContainsKey(grammarRoot))
                {
                    return FinalProcess(grammarRoot);
                }
                // `grammarRoot` has following terminals
                int selectedTerminalIndex = _random.Next(_grammar.GrammaticalDerivations[grammarRoot].Count);
                return FinalProcess(_grammar.GrammaticalDerivations[grammarRoot][selectedTerminalIndex]);
            }

            // `grammarRoot` is a non-terminal
            StringBuilder result = new StringBuilder();

            // select one derivation from possible derivations
            int selectedIndex = _random.Next(_grammar.LexicalDerivations[grammarRoot].Count);
            List<string> selectedGrammar = _grammar.LexicalDerivations[grammarRoot][selectedIndex];

            foreach (string nonTerminal in selectedGrammar)
            {
                // WORK AROUND
                int possibleBranches = 0;
                if (_grammar.LexicalDerivations.ContainsKey(nonTerminal))
                {
                    possibleBranches = _grammar.LexicalDerivations[nonTerminal].Count;
                }
                int tryCount = 0;
                string partialText;
                // try a few times
                do
                {
                    partialText = GetRandomText(nonTerminal, maxDepth - 1);
                    tryCount++;
                } while (partialText == null && tryCount < possibleBranches / 2 && maxDepth > 1);
                // still no result
                if (partialText == null)
                {
                    return null;
                }
                // collect valid result
                result.Append(partialText);
            }
            return result.ToString();
        }

        private string FinalProcess(string theString)
        {
            if (theString == "\\n")
                return "\n";
            return theString;
        }
    }
}
