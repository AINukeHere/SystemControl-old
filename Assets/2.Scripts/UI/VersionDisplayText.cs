using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionDisplayText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Text textUI = GetComponent<Text>();
        textUI.text = $"Version {Application.version}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
