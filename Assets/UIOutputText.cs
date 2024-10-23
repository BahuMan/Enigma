using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOutputText : MonoBehaviour
{

    [SerializeField] private Button ClearOutputButton;
    [SerializeField] private TMP_Text OutputText;
    [SerializeField] private EnigmaM4 EnigmaM4;
    [SerializeField] private EnigmaM3 EnigmaM3;

    private void OnEnable()
    {
        EnigmaM3.OnCharacterDecoded.AddListener(OnCharacterDecoded);
        EnigmaM4.OnCharacterDecoded.AddListener(OnCharacterDecoded);
    }
    private void OnDisable()
    {
        EnigmaM3.OnCharacterDecoded.RemoveListener(OnCharacterDecoded);
        EnigmaM4.OnCharacterDecoded.RemoveListener(OnCharacterDecoded);
    }

    public void OnCharacterDecoded(char arg0)
    {
        OutputText.text += arg0;
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearOutputButton.onClick.AddListener(ClearOutputButton_Clicked);
    }

    public void ClearOutputButton_Clicked()
    {
        OutputText.text = string.Empty;
    }

}
