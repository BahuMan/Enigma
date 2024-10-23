using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}
