using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterEvent : UnityEvent<char> { }


public class RotorControl: MonoBehaviour {

    [Tooltip("put all 26 letters of the alphabet in here. 'A' gets encrypted in the first letter of your string; 'B' gets encrypted into the second letter of your string. This wiring never changes and is unique to the rotor. The encryption key will tell you which rotor to use")]
    public string wiring = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
    [Tooltip("changes the labels on the ring. So when originally 'A' was going to be encrypted into the first letter of the wiring, now this letter will get encrypted into the first letter of the wiring. You should get this information as part of the encryption key")]
    public char ringStellung = 'A';
    public int _ringStellungIndex = 0;
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [Tooltip("pre-rotate the rotor before encryption. The position should be part of the encryption key")]
    public char ringPosition = 'A';
    public int _ringPositionIndex = 0;
    [Tooltip("position(s) where, if this rotor is rotated, the next rotor will also rotate. Fixed property of a drum.")]
    public string notches = "Q";
    
    public CharacterEvent OnCharacterOut;
    public CharacterEvent OnCharacterOutReverse;
    public UnityEvent OnNotch;

    private void OnValidate()
    {
        if (wiring == null) Debug.LogError("Rotor Configuration for " + gameObject.name + " is incorrect (cannot be null)");
        if (wiring.Length != ALPHABET.Length) Debug.LogError("Rotor Configuration for " + gameObject.name + " is incorrect (should be exactly 26 characters): " + wiring);

        //ringStelling moves the alphabet labels relative to the internal mechanics
        ringStellung = char.ToUpper(ringStellung);
        _ringStellungIndex = ringStellung - 'A';
        if (_ringStellungIndex < 0 || _ringStellungIndex >= ALPHABET.Length) Debug.LogError("ringStellung '" + ringStellung + "' is invalid; should be a letter A-Z");

        //ring position moves the entire ring, so it also determines when the next rotor is going to rotate
        ringPosition = char.ToUpper(ringPosition);
        _ringPositionIndex = ringPosition - 'A';
        if (_ringPositionIndex < 0 || _ringPositionIndex >= ALPHABET.Length) Debug.LogError("ringPosition '" + ringPosition + "' is invalid; should be a letter A-Z");
    }

    private void Start()
    {
        wiring = wiring.ToUpper();
        ringStellung = char.ToUpper(ringStellung);
        _ringStellungIndex = ringStellung - 'A';
    }

    public void InputCharacter(char inChar)
    {
        //character comes in at position 'inChar'
        int i = char.ToUpper(inChar) - 'A';
        Debug.Assert(i >= 0 && i < ALPHABET.Length, "input character not part of alphabet: '" + inChar + "'", gameObject);

        //because of rotor position, recalculate position:
        i = (i + _ringPositionIndex) % ALPHABET.Length;

        //because of ringstelling, the rotor position is different from what you might "see":
        i = (i + ALPHABET.Length - _ringStellungIndex) % ALPHABET.Length;

        //now use wiring to substitute character:
        char tmpresult = wiring[i];

        //when encrypted with "normal" wiring, now translate the encrypted character according to ringstellung:
        tmpresult = ALPHABET[(tmpresult - 'A' + ALPHABET.Length - _ringPositionIndex + _ringStellungIndex) % ALPHABET.Length];
        //Debug.Log("on rotor " + gameObject.name + ": from " + inChar + " to " + tmpresult);

        OnCharacterOut.Invoke(tmpresult);
    }


    public void InputCharacterReverse(char inCharReverse)
    {
        //first, shift input character with ringstellung and ringposition:
        inCharReverse = ALPHABET[(char.ToUpper(inCharReverse) - 'A' + _ringPositionIndex - _ringStellungIndex + ALPHABET.Length) % ALPHABET.Length];
        
        //then, decrypt it with wiring settings:
        int i = wiring.IndexOf(char.ToUpper(inCharReverse));
        Debug.Assert(i != -1, "can't reverse character '" + inCharReverse + "', probably because it was not part of the alphabet", gameObject);

        //shift position with ringstellung:
        i = (i + _ringStellungIndex) % ALPHABET.Length;

        //shift position with rotor position (which is visible in the window)
        i = (i + ALPHABET.Length - _ringPositionIndex) % ALPHABET.Length;

        //Debug.Log("on rotor " + gameObject.name + " (reverse): from " + inCharReverse + " to " + ALPHABET[i]);
        OnCharacterOutReverse.Invoke(ALPHABET[i]);
    }

    //returns true of the notch was engaged (that means the next rotor should also step)
    //TODO: take _ringStellingIndex into account,
    //      SEE file:///G:/Projects/Unity/Enigma/example/The%20Enigma%20-%202.htm bottom of the page
    public bool Step(bool previousRingCaught)
    {
        bool pushNextRotor = notches.IndexOf(ringPosition) != -1;
        if (pushNextRotor)
        {
            Debug.Log("Double step for " + this.gameObject.name);
        }
        if (previousRingCaught || pushNextRotor) //this rotor will also rotate when the next rotor should push because of double-step
        {
            _ringPositionIndex = (_ringPositionIndex + 1) % ALPHABET.Length;
            ringPosition = (char)(_ringPositionIndex + 'A');
        }
        return pushNextRotor;
    }
}
