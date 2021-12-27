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
    [SerializeField]
    private BoolOutputModule boolOutputModule;
    //소수점 2자리까지만 출력
    public override void CheckOutput()
    {
        boolOutputModule.Input(value);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = value.ToString();
    }
}
