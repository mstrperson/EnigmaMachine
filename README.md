# EnigmaMachine

This is an example project.  The README.md file is an excellent place to put things like project instructions.

This project is an Emulator for a WW2 era German Enigma Machine.  You can mix and match components to build a virtual Enigma Machine and then use it to encrypt secret messages.

# Class Diagram

## EnigmaMachine
Emulate a WW2 Era Enigma Machine ([Source](https://en.wikipedia.org/wiki/Enigma_machine))
### Properties
* PlugBoard : PlugBoard
  PlugBoard Jumpers particular letters to add a layer of scrambling before entering the rotor system.
* Rotors : List<Rotor>
  Rotors in the order from Right to Left installed in the EnigmaMachine.
* *Reflector* : Reflector
  The Reflector module is not modifiable by the end user once it is installed.
* EncodeSpaceAs : char
  Character to encode a SPACE as.  Traditionally, this is the letter X.
### Constructor
* EnigmaMachine(Rotor rotor1, Rotor rotor2, Rotor rotor3, Rotor rotor4 = null, Rotor rotor5 = null, Reflector reflector = null) : 
  Takes either 3 or 5 Rotors and a Reflector. 
  Plugboard is initialized with no jumpers installed.
  If no reflector is provided, it uses the standard ETW reflector.
### Methods
* Process(char ch) : char
  Emulates a key press on the Enigma Machine
  Increments the rotors per the machine's configuration.
  Returns: enciphered character
* Process(string message) : string
  Iterates over each character in the message and enciphers it using the Process(char ch) method.
  Returns: enciphered message

## PlugBoard
Represents the plugboard on the front of the Enigma Machine.  This introduces a first layer of character scrambling before the keypress is passed through the rotors.  When a pair of letters are jummpered on the PlugBoard, the keypress swaps the characters before it goes to the rotors and again on the way out of the rotors to the display.
The PlugBoard is used as an Array where the index is the input and the result is the letter that the PlugBoard maps that character to.
### Properties
* *Mapping* : Dictionary<char, char>
  Contains the mappings of input-output characters.  This map is bi-directional by nature of other code in this function such that `Mapping[a] = b` implies `Mapping[b] = a` always.
* this[char ch] : char
  Array Index Operator returns the mapped character for the given input.
### Constructor
* PlugBoard()
  Default constructor only.  Initializes the Mapping to map all A-Z characters to themselves.
### Methods
* Plug(char a, char b) : void
  Checks rules and then applies Mapping between two characters.
  Throws: EnigmaRulesException if the requested Jumper is not allowed.
  
## Rotor
Rotors are the interesting part of the guts of the EnigmaMachine and are worth reading more about on the [Wikipedia Article](https://en.wikipedia.com/wiki/Enigma_machine).  They also contain the fatal flaw in the Enigma encryption algorithm:  No character can ever be enciphered to itself.
### Properties
* *Mapping* : string
  The capital letters A-Z re-ordered to the characters that A-Z map to.  See Standard Rotors for Historical Rotor settings.
* RingSetting : int
  Original Enigma Rotors triggered the next rotor to rollover after a particular letter.  In order to add additional complexity, later versions included a Ring which would move the rollover trigger notches around the Rotor.  This RingSetting represents the number of "clicks" that the Ring is rotated around this particular Rotor.
* Offset : int
  Counter for where in the rotation cycle the Rotor currently is.
* Number : string
  Label for which position this Rotor is installed in the EnigmaMachine.  This is for cosmetic purposes only and has no impact on the operation of the Rotor.
* Notch : string
  Original Rotors only had a single notch at "Z", Later rotors contained more than one Notch triggering rollovers more than once per rotation of the Rotor.  This, counter-intuitively, actually *weakend* the Encryption because it made changes to the pattern which reduced the number of possible solutions.
### Constructor
* Rotor(string mapping, string notch = "Z", string name = "", ushort ringSetting = 0)
  Initializes a Rotor with a given set of properties.
  Throws: EnigmaRulesException if the provided mapping is invalid.
### Methods
* In(char ch) : char
  Gets the character on the LEFT side of the Rotor when passing through this Rotor on the given character's electrical path *before* hitting the reflector.
  Returns: the mapped character
* Out(char ch) : char
  Gets the character on the RIGHT side of the Rotor when passing through this Rotor on the given character's electrical path *after* hitting the reflector.
  Returns: the reverse mapped character
* Increment() : bool
  Rotates the Rotor by one position.
  Returns: true if the Rotor has passed over a Notch in the Ring.
* SetPosition(char ch) : bool
  Rotates the rotor *forward* to the indicated character.
  Returns: true if the Rotor has passed over a Notch in the Ring. (this will not tell you if two or more notches are hit)
  
## Reflector
Logically, the reflector is just a Rotor that does not rotate.  It is the loopback for each of the electrical paths in the EnigmaMachine.  It contains the same fatal flaw as each other component, that is a character cannot map to itself.
### Properties
* *Mapping* : string
  The capital letters A-Z re-ordered to the characters that A-Z map to.  See Standard Rotors for Historical Rotor settings.
### Constructor
* Reflector(string mapping)
  Initailize the reflector with this mapping.
  Throws: EnigmaRulesException if the mapping is invalid.
### Methods
* Reflect(char ch) : char
  Returns: the character mapped by the reflector.
  
## Standard Equipment
The `StandardRotors.cs` and `StandardReflectors.cs` files add static definitions for Historical Rotors and Reflectors used with period Enigma Machines.  You can use these to mix and match to Emulate sending messages Encrypted on WW2 era hardware.

### Program.cs
The example program provides an initialized Enigma Machine using standard rotors and reflector as well as an arbitrary set of jumpers on the Board and initial settings for the machine.

This is how an operator would use the Enigma Machine to encrypt or decrypt a message.
1) Install Rotors and Reflector
```EnigmaMachine enigmaMachine = new EnigmaMachine(Rotor.VIII, Rotor.VI, Rotor.II, reflector: Reflector.ETW);```
2) Insert Jumper wires to particular settings
```
enigmaMachine.PlugBoard.Plug('A', 'T');
enigmaMachine.PlugBoard.Plug('E', 'Y');
enigmaMachine.PlugBoard.Plug('K', 'O');
enigmaMachine.PlugBoard.Plug('N', 'P');
```
3) Set Rotors to initial positions and rotate Rings to particular settings
```
enigmaMachine.Rotors[0].SetPosition('K');
enigmaMachine.Rotors[1].SetPosition('F');
enigmaMachine.Rotors[2].SetPosition('C');
enigmaMachine.Rotors[0].RingSetting = 20;
enigmaMachine.Rotors[1].RingSetting = 2;
enigmaMachine.Rotors[2].RingSetting = 11;
```
4) Encode the message `string encrypted = enigmaMachine.Process(message);`

The clever part of the Enigma Machine (being an electro-mechanical device) was that to decrypt the Message, you simply enter the encrypted string into an Enigma Machine with an identical starting configuration.
