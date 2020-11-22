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
    public override string GetInfoString()
    {
        return "이전프레임에서 현재프레임사이에 흐른 시간을 내보냅니다.";
    }
}
