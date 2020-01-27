using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    /// <summary>
    /// Represents the Plugboard Component of the EnigmaMachine.
    /// The Plugboard left unconfigured has no cipher component; it leaves
    /// each character mapped to itself.
    /// </summary>
    public class PlugBoard
    {
        /// <summary>
        /// Bi-directional mapping for the plug board.
        /// </summary>
        private Dictionary<char, char> Mapping;

        public override string ToString()
        {
            string output = "";
            bool first = true;
            string hit = "";
            foreach(char ch in Mapping.Keys)
            {
                if(ch != Mapping[ch] && !hit.Contains(ch))
                {
                    output += string.Format("{0}{1}-{2}", first ? "PlugBoard:  " : ", ", ch, Mapping[ch]);
                    hit += Mapping[ch];
                    hit += ch;
                    first = false;
                }
            }
            
            return output;
        }

        /// <summary>
        /// Initialize a PlugBoard with nothing patched.
        /// </summary>
        public PlugBoard()
        {
            Mapping = new Dictionary<char, char>();
            for(char ch = 'A'; ch <= 'Z'; ch++)
            {
                Mapping.Add(ch, ch);
            }
        }

        /// <summary>
        /// Plug /a/ and /b/ together using the PlugBoard.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Plug(char a, char b)
        {
            EnigmaRulesException.AssertValidCharacter(a);
            EnigmaRulesException.AssertValidCharacter(b);

            if (Mapping[a] != a || Mapping[b] != b)
                throw new EnigmaRulesException("You can only plug a character to another character once per plugboard.");

            Mapping[a] = b;
            Mapping[b] = a;
        }

        /// <summary>
        /// Get the Plugboard Enciphered character.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public char this[char ch] => Mapping.ContainsKey(ch) ? Mapping[ch] : ch;
    }
}
