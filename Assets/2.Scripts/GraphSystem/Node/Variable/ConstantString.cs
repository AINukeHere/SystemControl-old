using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantString : GetVariable
{
    [SerializeField]
    private string value;
    [SerializeField]
    private bool HasValue = true;
    [SerializeField]
    private StringOutputModule stringOutputModule;
    //소수점 2자리까지만 출력
    public override void CheckOutput()
    {
        stringOutputModule.Input(value);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = value;
    }
}
