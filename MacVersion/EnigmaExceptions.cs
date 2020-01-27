using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    public class EnigmaRulesException : Exception
    {
        public EnigmaRulesException(string message, Exception innerException = null) : base(message, innerException)
        {

        }

        public static void AssertValidCharacter(char ch)
        {
            if ('A' <= ch && ch <= 'Z') return;

            throw new EnigmaRulesException(string.Format("'{0}' is not a valid character for the Engima cipher.", ch));
        }
    }
}
