using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDeltaTime : GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;

    public override void CheckOutput()
    {
        float_output.Input(Time.deltaTime);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = Time.deltaTime.ToString();
    }
}
