using UnityEngine;
using UnityEngine.UI;

public class MessageText : MonoBehaviour
{

    private static Text _instance;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = GetComponent<Text>();
        }
        else
        {
            Debug.LogError("double MessageTextInstantiation in " + gameObject.name);
            _instance.text = "double MessageTextInstantiation in " + gameObject.name;
        }
    }

    public static void SetText(string txt)
    {
        if (_instance != null) _instance.text = txt;
    }

}
