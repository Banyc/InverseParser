using System.Collections.Generic;

namespace InverseParser.Models
{
    public class GrammarModel
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
    }
}
