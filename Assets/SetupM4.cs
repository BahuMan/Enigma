using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(EnigmaM4))]
public class SetupM4 : MonoBehaviour
{

    private EnigmaM4 thisMachine;

    [SerializeField] private Button WalzenanlageButton;
    [SerializeField] private Button RingstellungButton;
    [SerializeField] private Button GrundstellungButton;
    [SerializeField] private Button SteckernButton;

    [SerializeField] private TMP_InputField WalzenanlageInput;
    [SerializeField] private TMP_InputField RingstellungInput;
    [SerializeField] private TMP_InputField GrundStellungInput;
    [SerializeField] private TMP_InputField SteckernInput;

    // Start is called before the first frame update
    void Start()
    {
        thisMachine = GetComponent<EnigmaM4>();
    }

    private void OnEnable()
    {
        WalzenanlageButton.onClick.AddListener(Walzenanlage_clicked);
        RingstellungButton.onClick.AddListener(Ringstellung_clicked);
        GrundstellungButton.onClick.AddListener(Grundstellung_clicked);
        SteckernButton.onClick.AddListener(Steckern_clicked);
    }

    private void OnDisable()
    {
        WalzenanlageButton.onClick.RemoveListener(Walzenanlage_clicked);
        RingstellungButton.onClick.RemoveListener(Ringstellung_clicked);
        GrundstellungButton.onClick.RemoveListener(Grundstellung_clicked);
        SteckernButton.onClick.RemoveListener(Steckern_clicked);
    }

    private void Steckern_clicked()
    {
        throw new NotImplementedException();
    }

    private void Grundstellung_clicked()
    {
        throw new NotImplementedException();
    }

    private void Ringstellung_clicked()
    {
        string rstellung = this.RingstellungInput.text;
        if (rstellung.Length != 4)
        {
            Debug.LogError("for M4 machine, we need exactly 4 letters of the alphabet instead of '" + rstellung + "'");
            return;
        }
        thisMachine.rotorTHIN.ringStellung = rstellung[0];
        thisMachine.positionLEFT.ringStellung = rstellung[1];
        thisMachine.positionMIDDLE.ringStellung=rstellung[2];
        thisMachine.positionRIGHT.ringStellung= rstellung[3];
    }

    private void Walzenanlage_clicked()
    {
        //we need a reference to each of the rings to pull this off
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
