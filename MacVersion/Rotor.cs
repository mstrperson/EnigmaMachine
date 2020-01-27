using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnigmaMachine.EnigmaMachine;

namespace EnigmaMachine
{
    /// <summary>
    /// Represents a Rotor in the Enigma Machine.
    /// The exceptional flaw in the Enigma Machine was that no
    /// character could ever encipher to itself.
    /// </summary>
    public partial class Rotor
    {
        protected string Mapping;

        public int RingSetting;

        public int Offset
        {
            get;
            protected set;
        }

        public string Number
        {
            get;
            protected set;
        }

        protected string Notch;

        public override string ToString()
        {
            string output = string.Format("Rotor {0} [{1}]:  ", Number, CHARACTERS[Offset]);

            for (int i = 0; i < 26; i++)
            {
                output += Mapping[i];

                if (Notch.Contains(CHARACTERS[i]))
                    output += "^";
            }

            return output;
        }

        public Rotor(string mapping, string notch = "Z", string name = "", ushort ringSetting = 0)
        {
            RingSetting = ringSetting % 26;
            Number = name;
            if (mapping.Length != CHARACTERS.Length)
                throw new EnigmaRulesException("Invalid mapping for Rotor.");
            foreach(char ch in CHARACTERS)
            {
                if (mapping.Where(mc => mc == ch).Count() != 1)
                    throw new EnigmaRulesException("Invalid mapping for Rotor.");
            }

            Mapping = mapping;
            Notch = notch;
        }

        /// <summary>
        /// Get the Encyphered character from the input pass through this rotor.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public char In(char ch)
        {
            int i = (CHARACTERS.IndexOf(ch) + Offset) % 26;
            return Mapping[i];
        }

        /// <summary>
        /// Get the Encyphered character from the input pass through this rotor.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public char Out(char ch)
        {
            int j = (Mapping.IndexOf(ch) - Offset + 26) % 26;
            return CHARACTERS[j];
        }

        /// <summary>
        /// Increment this Rotor.
        /// Returns true if the rotor rolled over.
        /// </summary>
        /// <returns>True if this rotor rolled over</returns>
        public bool Increment()
        {
            bool rollover = Notch.Contains(CHARACTERS[(Offset + RingSetting) % 26]);
            Offset = (++Offset) % 26;
            
            return rollover;
        }

        /// <summary>
        /// Set the position of this rotor.  
        /// This may trigger a rotation (if you choose to implement it!).
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>True if the notch has been passed.</returns>
        public bool SetPosition(char ch)
        {
            if (!CHARACTERS.Contains(ch)) throw new EnigmaRulesException("Invalid character to set position.");

            int i = CHARACTERS.IndexOf(ch);
            bool rollover = false;
            while(Offset != i)
            {
                rollover = rollover || this.Increment();
            }

            return rollover;
        }
    }
}
