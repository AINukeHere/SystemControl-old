using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantInt : GetVariable
{
    //nullable 변수가 직렬화될 수 없음.
    [SerializeField]
    private int value;
    [SerializeField]
    private bool HasValue = true;
    [SerializeField]
    private IntOutputModule intOutputModule;
    //소수점 2자리까지만 출력
    public override void CheckOutput()
    {
        intOutputModule.Input(value);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = value.ToString();
    }
}
