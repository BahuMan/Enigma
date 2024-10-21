using UnityEngine;

public class SteckerControl : MonoBehaviour
{

    public string[] pairs;
    public string encryptionfinal;
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Use this for initialization
    void Start()
    {
        char[] encryptiontmp = ALPHABET.ToCharArray();

        foreach (var pair in pairs)
        {
            if (pair.Length != 2)
            {
                Debug.LogError("This is not a pair of letters: '" + pair + "'");
            }
            else
            {
                int from = ALPHABET.IndexOf(pair.ToUpper()[0]);
                int to = ALPHABET.IndexOf(pair.ToUpper()[1]);
                //double check that the letter hasn't already been switched by another pair:
                if (ALPHABET[from] != encryptiontmp[from])
                {
                    Debug.LogError("letter '" + ALPHABET[from] + "' has already been switched by another pair");
                }
                else if (ALPHABET[to] != encryptiontmp[to])
                {
                    Debug.LogError("letter '" + ALPHABET[to] + "' has already been switched by another pair");
                }
                else
                {
                    //OK, we can make the switch:
                    encryptiontmp[from] = ALPHABET[to];
                    encryptiontmp[to] = ALPHABET[from];
                }
            }
        }
        encryptionfinal = new string(encryptiontmp);
    }


    public char convert(char inChar)
    {
        int pos = ALPHABET.IndexOf(inChar);
        //Debug.Log("Stecker: " + inChar + " -> " + encryptionfinal[pos]);
        return encryptionfinal[pos];
    }

    /*
     * this was only for debug purposes
     * 
    private void Update()
    {
        foreach (var l in ALPHABET)
        {
            if (Input.GetKeyDown(l.ToString().ToLower()))
            {
                Debug.Log("Stecker converts " + l + " to " + convert(l));
            }
        }
    }
    */
}
