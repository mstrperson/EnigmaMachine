using System;

namespace EnigmaMachine
{
    class Program
    {
        static void Main(string[] args)
        {

            EnigmaMachine enigmaMachine = new EnigmaMachine(Rotor.VIII, Rotor.VI, Rotor.II, reflector: Reflector.ETW);

            enigmaMachine.PlugBoard.Plug('A', 'T');
            enigmaMachine.PlugBoard.Plug('E', 'Y');
            enigmaMachine.PlugBoard.Plug('K', 'O');
            enigmaMachine.PlugBoard.Plug('N', 'P');

            enigmaMachine.Rotors[0].SetPosition('K');
            enigmaMachine.Rotors[1].SetPosition('F');
            enigmaMachine.Rotors[2].SetPosition('C');
            enigmaMachine.Rotors[0].RingSetting = 20;
            enigmaMachine.Rotors[1].RingSetting = 2;
            enigmaMachine.Rotors[2].RingSetting = 11;

            Console.WriteLine(enigmaMachine);

            Console.Write("Enter a Message:  ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string message = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Black;

            string encrypted = enigmaMachine.Process(message);

            Console.Write("Encrypted:  ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(encrypted);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();

            // Emulate a second machine with the same settings
            EnigmaMachine enigmaMachine2 = new EnigmaMachine(Rotor.VIII, Rotor.VI, Rotor.II, reflector: Reflector.ETW);

            enigmaMachine2.PlugBoard.Plug('A', 'T');
            enigmaMachine2.PlugBoard.Plug('E', 'Y');
            enigmaMachine2.PlugBoard.Plug('K', 'O');
            enigmaMachine2.PlugBoard.Plug('N', 'P');

            enigmaMachine2.Rotors[0].SetPosition('K');
            enigmaMachine2.Rotors[1].SetPosition('F');
            enigmaMachine2.Rotors[2].SetPosition('C');
            enigmaMachine2.Rotors[0].RingSetting = 20;
            enigmaMachine2.Rotors[1].RingSetting = 2;
            enigmaMachine2.Rotors[2].RingSetting = 11;

            string output = enigmaMachine2.Process(encrypted);

            Console.Write("Decrypted:  ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(output);
            Console.ReadLine();
        }

        static void TestRotors()
        {
            Rotor rotorIII = Rotor.III;
            Rotor rotorVI = Rotor.VI;
            Rotor rotorI = Rotor.I;
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                char ech = rotorIII.In(ch);
                char och = rotorIII.Out(ech);
                Console.WriteLine("{0} => {1} => {2}", ch, ech, och);
            }
            Console.WriteLine("________________________________");
            Console.ReadLine();

            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                char ech = rotorVI.In(ch);
                char och = rotorVI.Out(ech);
                Console.WriteLine("{0} => {1} => {2}", ch, ech, och);
            }
            Console.WriteLine("________________________________");
            Console.ReadLine();

            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                char ech = rotorI.In(ch);
                char och = rotorI.Out(ech);
                Console.WriteLine("{0} => {1} => {2}", ch, ech, och);
            }
            Console.WriteLine("________________________________");
            Console.ReadLine();
            Reflector reflector = Reflector.A;

            rotorIII.Increment();
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                char c1 = rotorIII.In(ch);
                char c2 = rotorVI.In(c1);
                char c3 = rotorI.In(c2);
                char r = reflector.Reflect(c3);
                char c4 = rotorI.Out(r);
                char c5 = rotorVI.Out(c4);
                char c6 = rotorIII.Out(c5);
                Console.WriteLine("{0} => {1} => {2} => {3} => {4} => {5} => {6} => {7}", ch, c1, c2, c3, r, c4, c5, c6);

                char dc1 = rotorIII.In(c6);
                char dc2 = rotorVI.In(dc1);
                char dc3 = rotorI.In(dc2);
                char dr = reflector.Reflect(dc3);
                char dc4 = rotorI.Out(dr);
                char dc5 = rotorVI.Out(dc4);
                char dc6 = rotorIII.Out(dc5);
                Console.WriteLine("{7} <= {6} <= {5} <= {4} <= {3} <= {2} <= {1} <= {0}", c6, dc1, dc2, dc3, dr, dc4, dc5, dc6);
                Console.WriteLine("______________________________________________________________________________________");
                Console.ReadLine();
            }
        }
    }
}
