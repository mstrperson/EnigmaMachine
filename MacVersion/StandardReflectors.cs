using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnigmaMachine.EnigmaMachine;

namespace EnigmaMachine
{
    public partial class Reflector
    {
        /// <summary>
        /// Null Reflector. From the Comercial version of Engima I.
        /// </summary>
        public static Reflector ETW =>
            new Reflector(new string(CHARACTERS.Reverse().ToArray()));

        public static Reflector A =>
            new Reflector("EJMZALYXVBWFCRQUONTSPIKHGD");

        public static Reflector B =>
            new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");

        public static Reflector C =>
            new Reflector("FVPJIAOYEDRZXWGCTKUQSBNMHL");
    }
}
