using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : Event
{
    [SerializeField]
    private KeyCode key;
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(key))
        {
            Active();
            active_output.Active();
        }
    }
    public override void ExpandDisplay()
    {
        textMesh.text = $"{nodeName}\n{key.ToString()}";
    }
}
