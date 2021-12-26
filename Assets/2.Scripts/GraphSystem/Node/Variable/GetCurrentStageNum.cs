using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentStageNum : GetVariable
{
    [SerializeField]
    private IntOutputModule int_output;

    public override void CheckOutput()
    {
        int_output.Input(AvoidGameManager.instance?.GetCurrentStageNum(gameObject));
    }
    public override string GetInfoString()
    {
        return "현재 스테이지 값을 내보냅니다.";
    }
}
