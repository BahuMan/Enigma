using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardToEnigma : MonoBehaviour
{

    [SerializeField] private EnigmaM4 EnigmaM4;
    [SerializeField] private EnigmaM3 EnigmaM3;

    string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    void Update()
    {
        foreach (var ch in alphabet) {
            if (Input.GetKeyDown(ch))
            {
                if (EnigmaM4 != null && EnigmaM4.gameObject.activeSelf) EnigmaM4.Convert(ch[0]);
                if (EnigmaM3 != null && EnigmaM3.gameObject.activeSelf) EnigmaM3.Convert(ch[0]);
            }
        }
    }
}
