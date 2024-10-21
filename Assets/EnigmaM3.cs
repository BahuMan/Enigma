using System;
using UnityEngine;

public class EnigmaM3: MonoBehaviour {

    [Tooltip("This is the RIGHT most rotor, so the fastest rotating rotor")]
    public RotorControl positionRIGHT;
    [Tooltip("This is the MIDDLE rotor, it rotates only once every 26 times the right-hand rotor rotates")]
    public RotorControl positionMIDDLE;
    [Tooltip("This the LEFT most rotor in the commercial and Wehrmacht configuration. It is the slowest rotating rotor")]
    public RotorControl positionLEFT;
    [Tooltip("In the commercial and Wehrmacht configuration, the reflector does no substitution")]
    public RotorControl reflector;

    //M3 did not have steckern
    //public SteckerControl _stecker;

    // Use this for initialization
    void Start() {
        positionRIGHT.OnCharacterOut.AddListener(c => { positionMIDDLE.InputCharacter(c); });
        positionRIGHT.OnCharacterOutReverse.AddListener(c => { MessageText.SetText("output from rotor1 = " + c); });

        positionMIDDLE.OnCharacterOut.AddListener(c => { positionLEFT.InputCharacter(c); });
        positionMIDDLE.OnCharacterOutReverse.AddListener(c => { positionRIGHT.InputCharacterReverse(c); });

        positionLEFT.OnCharacterOut.AddListener(c => { reflector.InputCharacter(c); });
        positionLEFT.OnCharacterOutReverse.AddListener(c => { positionMIDDLE.InputCharacterReverse(c); });

        reflector.OnCharacterOut.AddListener(c => { positionLEFT.InputCharacterReverse(c); });

        //the reflector should only be connected once; the normal and reverse encryption will be the same
        //reflector.OnCharacterOutReverse.AddListener(c => { rotor2.InputCharacterReverse(c); });
    }

    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            bool next = positionRIGHT.Step(true);
            next = positionMIDDLE.Step(next);
            next = positionLEFT.Step(next);

            Debug.Log("new rotor positions: " + positionLEFT.ringPosition + positionMIDDLE.ringPosition + positionRIGHT.ringPosition);
        }

        foreach (var l in alphabet)
        {
            if (Input.GetKeyDown(l))
            {
                char beforeStecker = l.ToUpper()[0];
                //char afterStecker = _stecker.convert(beforeStecker);
                positionRIGHT.InputCharacter(beforeStecker);
            }
        }
    }


}
