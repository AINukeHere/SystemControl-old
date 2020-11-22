using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFloat : GetVariable
{
    //nullable 변수가 직렬화될 수 없음.
    [SerializeField]
    private float value;
    [SerializeField]
    private bool HasValue = true;

    private TextMesh myTextMesh;
    [SerializeField]
    private FloatOutputModule floatOutputModule;
    //소수점 2자리까지만 출력
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value.ToString("N");
            else
                myTextMesh.text = "NULL";
        }
    }
    public override void CheckOutput()
    {
        floatOutputModule.Input(value);
    }
    public override string GetInfoString()
    {
        return "실수값입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}
