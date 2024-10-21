using UnityEngine;
using System.Collections;
using System;

public class EnigmaM4 : MonoBehaviour
{
    [Tooltip("This is the RIGHT most rotor, so the fastest rotating rotor")]
    public RotorControl positionRIGHT;
    [Tooltip("This is the MIDDLE rotor, it rotates (steps) only once every 26 times the right-hand rotor rotates")]
    public RotorControl positionMIDDLE;
    [Tooltip("This the LEFT most rotor in the commercial and Wehrmacht configuration. It is the slowest rotating (stepping) rotor")]
    public RotorControl positionLEFT;
    [Tooltip("In the M4 navy machine, an extra thin rotor was slotted next to the thin reflector. It could be rotated but did not advance automatically")]
    public RotorControl rotorTHIN;
    [Tooltip("In the commercial and Wehrmacht configuration, the reflector does no substitution. In all others, it performs a pair-wise switch")]
    public RotorControl reflectorTHIN;

    public SteckerControl _stecker;
    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    private char lastDecoded; //in the M4 machine, this should be set AFTER SteckerControl
    public char LastDecoded { get { return lastDecoded; } private set {lastDecoded = value; OnCharacterDecoded.Invoke(value); }}
    public CharacterEvent OnCharacterDecoded;

    // Use this for initialization
    void Start()
    {
        positionRIGHT.OnCharacterOut.AddListener(c => { positionMIDDLE.InputCharacter(c); });
        positionRIGHT.OnCharacterOutReverse.AddListener(c => { lastDecoded = _stecker.convert(c);  MessageText.SetText("output from rotor1 = " + lastDecoded); });

        positionMIDDLE.OnCharacterOut.AddListener(c => { positionLEFT.InputCharacter(c); });
        positionMIDDLE.OnCharacterOutReverse.AddListener(c => { positionRIGHT.InputCharacterReverse(c); });

        positionLEFT.OnCharacterOut.AddListener(c => { rotorTHIN.InputCharacter(c); });
        positionLEFT.OnCharacterOutReverse.AddListener(c => { positionMIDDLE.InputCharacterReverse(c); });

        rotorTHIN.OnCharacterOut.AddListener(c => { reflectorTHIN.InputCharacter(c); });
        rotorTHIN.OnCharacterOutReverse.AddListener(c => { positionLEFT.InputCharacterReverse(c); });

        reflectorTHIN.OnCharacterOut.AddListener(c => { rotorTHIN.InputCharacterReverse(c); });

        //the reflector should only be connected once; the normal and reverse encryption will be the same
        //reflector.OnCharacterOutReverse.AddListener(c => { rotor2.InputCharacterReverse(c); });
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            bool next = positionRIGHT.Step(true);
            next = positionMIDDLE.Step(next);
            next = positionLEFT.Step(next);
            //rotorTHIN does not get rotated
            //reflectTHIN does not get rotated

            Debug.Log("new rotor positions: " + positionLEFT.ringPosition + positionMIDDLE.ringPosition + positionRIGHT.ringPosition);
        }

        foreach (var l in alphabet)
        {
            if (Input.GetKeyDown(l))
            {
                Convert(l[0]);
            }
        }
    }

    public char Convert(char ch)
    {
        char beforeStecker = Char.ToUpper(ch);
        char afterStecker = _stecker.convert(beforeStecker);
        positionRIGHT.InputCharacter(afterStecker);

        Debug.Log(ch + " -> " + lastDecoded);
        return lastDecoded;
    }

    //
    public void RotateRightMost()
    {

    }
}
