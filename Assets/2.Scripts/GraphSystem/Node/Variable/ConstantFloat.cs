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
    [SerializeField]
    private FloatOutputModule floatOutputModule;
    //소수점 2자리까지만 출력
    public override void CheckOutput()
    {
        floatOutputModule.Input(value);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = value.ToString();
    }
}
