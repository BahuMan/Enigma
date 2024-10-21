using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRotorForDebug : MonoBehaviour
{

    public RotorControl singleRotor;
    public bool useReflector;
    public RotorControl reflector;
    public bool useSteckern;
    public SteckerControl steckern;
    private string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private char lastDecoded;

    // Start is called before the first frame update
    void Start()
    {
        singleRotor.OnCharacterOut.AddListener(c => { rotorForward(c); });
        singleRotor.OnCharacterOutReverse.AddListener(c => { rotorReverse(c); });

        reflector.OnCharacterOut.AddListener(c => { reflectorNoDirection(c); });

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            bool next = singleRotor.Step(true);
            if (next)
            {
                Debug.Log("next rotor should rotate (not in this debug machine)");
            }
        }

        foreach (var l in alphabet)
        {
            if (Input.GetKeyDown(l))
            {
                Convert(l[0]);
            }
        }

    }

    private void rotorForward(char c)
    {
        if (useReflector) reflector.InputCharacter(c);
        else singleRotor.InputCharacterReverse(c);
    }

    private void rotorReverse(char c)
    {
        lastDecoded = c;
    }

    private void reflectorNoDirection(char c)
    {
        singleRotor.InputCharacterReverse(c);
    }

    public char Convert(char ch)
    {
        char d = Char.ToUpper(ch);

        if (useSteckern) d = steckern.convert(d);
        singleRotor.InputCharacter(d);
        if (useSteckern) d = steckern.convert(lastDecoded);

        Debug.Log(ch + " -> " + d);
        return lastDecoded;
    }
}
