using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnigmaMachine.EnigmaMachine;

namespace EnigmaMachine
{

    /// <summary>
    /// A reflector is (logically speaking) a rotor that does not rotate.
    /// </summary>
    public partial class Reflector
    {
        protected string Mapping;

        public override string ToString()
        {
            return string.Format("Reflector:  {0}", Mapping);
        }

        public Reflector(string mapping)
        {
            if (mapping.Length != CHARACTERS.Length)
                throw new EnigmaRulesException("Invalid mapping for Rotor.");
            foreach (char ch in CHARACTERS)
            {
                if (mapping.Where(mc => mc == ch).Count() != 1)
                    throw new EnigmaRulesException("Invalid mapping for Rotor.");
            }

            Mapping = mapping;
        }

        public char Reflect(char ch)
        {
            int i = CHARACTERS.IndexOf(ch);
            return Mapping[i];
        }
    }
}
