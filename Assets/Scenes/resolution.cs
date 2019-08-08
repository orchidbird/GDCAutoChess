using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(1600, 900, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
