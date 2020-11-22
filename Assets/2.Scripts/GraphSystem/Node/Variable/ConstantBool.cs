using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBool : GetVariable
{
    //nullable 변수가 직렬화될 수 없음.
    [SerializeField]
    private bool value = false;
    [SerializeField]
    private bool HasValue = true;

    private TextMesh myTextMesh;
    [SerializeField]
    private BoolOutputModule boolOutputModule;
    //소수점 2자리까지만 출력
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value.ToString();
            else
                myTextMesh.text = "NULL";
        }
    }
    public override void CheckOutput()
    {
        boolOutputModule.Input(value);
    }
    public override string GetInfoString()
    {
        return "실수값입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}
