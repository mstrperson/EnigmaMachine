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
  
