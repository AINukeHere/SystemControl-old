using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHorizontalInput : GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;

    public override void CheckOutput()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("GetHorizontalInput CheckOutput()");
        float_output.Input(Input.GetAxisRaw("Horizontal"));
    }
    public override string GetInfoString()
    {
        return "플레이어의 좌/우 키에 대한 입력값을 내보냅니다. (-1또는 0 또는 1)";
    }
}
