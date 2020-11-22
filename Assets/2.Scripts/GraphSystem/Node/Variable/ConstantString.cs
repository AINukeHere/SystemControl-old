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

    private TextMesh myTextMesh;
    [SerializeField]
    private StringOutputModule stringOutputModule;
    //소수점 2자리까지만 출력
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value;
            else
                myTextMesh.text = "NULL";
        }
    }
    public override void CheckOutput()
    {
        stringOutputModule.Input(value);
    }
    public override string GetInfoString()
    {
        return "문장입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}
