using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System;

namespace InverseParser.Services
{
    public class TextGenerator
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
        private readonly Dictionary<string, List<string>> _grammaticalDerivations;
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
        private readonly Dictionary<string, List<List<string>>> _lexicalDerivations;
        private readonly Random _random = new Random();

        public TextGenerator(GrammarDefinitionReader reader)
        {
            reader.ReadGrammar();
            _grammaticalDerivations = reader.GrammaticalDerivations;
            _lexicalDerivations = reader.LexicalDerivations;
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
            if (!_lexicalDerivations.ContainsKey(grammarRoot))
            {
                // `grammarRoot` is a literal
                if (!_grammaticalDerivations.ContainsKey(grammarRoot))
                {
                    return grammarRoot;
                }
                // `grammarRoot` has following terminals
                int selectedTerminalIndex = _random.Next(_grammaticalDerivations[grammarRoot].Count);
                return _grammaticalDerivations[grammarRoot][selectedTerminalIndex];
            }

            // `grammarRoot` is a non-terminal
            StringBuilder result = new StringBuilder();

            // select one derivation from possible derivations
            int selectedIndex = _random.Next(_lexicalDerivations[grammarRoot].Count);
            List<string> selectedGrammar = _lexicalDerivations[grammarRoot][selectedIndex];

            foreach (string nonTerminal in selectedGrammar)
            {
                // WORK AROUND
                int possibleBranches = 0;
                if (_lexicalDerivations.ContainsKey(nonTerminal))
                {
                    possibleBranches = _lexicalDerivations[nonTerminal].Count;
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
    }
}
