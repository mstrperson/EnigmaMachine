using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    public partial class Rotor
    {
        #region Comercial Enigma
        public static Rotor IC =>
            new Rotor("DMTWSILRUYQNKFEJCAZBPGXOHV", name: "IC");

        public static Rotor IIC =>
            new Rotor("HQZGPJTMOBLNCIFDYAWVEUSRKX", name: "IIC");

        public static Rotor IIIC =>
            new Rotor("UQNTLSZFMREHDPXKIBVYGJCWOA", name: "IIIC");
        #endregion
        #region German Railway (Rocket)
        public static Rotor ROCKET_I =>
            new Rotor("JGDQOXUSCAMIFRVTPNEWKBLZYH", name: "Rocket-I");

        public static Rotor ROCKET_II =>
            new Rotor("NTZPSFBOKMWRCJDIVLAEYUXHGQ", name: "Rocket-II");

        public static Rotor ROCKET_III =>
            new Rotor("JVIUBHTCDYAKEQZPOSGXNRMWFL", name: "Rocket-III");

        public static Rotor ROCKET_UKW =>
            new Rotor("QYHOGNECVPUZTFDJAXWMKISRBL", name: "Rocket-UKW");

        public static Rotor ROCKET_ETW =>
            new Rotor("QWERTZUIOASDFGHJKPYXCVBNML", name: "Rocket-ETW");
        #endregion

        #region WWII German Military
        /// <summary>
        /// Triggers turnover at Q
        /// </summary>
        public static Rotor I =>
            new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", name: "I");

        /// <summary>
        /// Triggers turnover at E
        /// </summary>
        public static Rotor II =>
            new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", "E", name: "II");

        /// <summary>
        /// Triggers turnover at V
        /// </summary>
        public static Rotor III =>
            new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", "V", name: "III");

        /// <summary>
        /// Triggers turnover at J
        /// </summary>
        public static Rotor IV =>
            new Rotor("ESOVPZJAYQUIRHXLNFTGKDCMWB", "J", name: "IV");

        /// <summary>
        /// Triggers turnover at Z
        /// </summary>
        public static Rotor V =>
            new Rotor("VZBRGITYUPSDNHLXAWMJQOFECK", name: "V");
        
        /// <summary>
        /// Triggers turnover at Z and M
        /// (Actually weakend the encryption!)
        /// </summary>
        public static Rotor VI =>
            new Rotor("JPGVOUMFYQBENHZRDKASXLICTW", "ZM", name: "VI");

        /// <summary>
        /// Triggers turnover at Z and M
        /// (Actually weakend the encryption!)
        /// </summary>
        public static Rotor VII =>
            new Rotor("NZJHGRCXMYSWBOUFAIVLPEKQDT", "ZM", name: "VII");

        /// <summary>
        /// Triggers turnover at Z and M
        /// (Actually weakend the encryption!)
        /// </summary>
        public static Rotor VIII =>
            new Rotor("FKQHTLXOCBJSPDZRAMEWNIUYGV", "ZM", name: "VIII");

        #endregion

    }

}
