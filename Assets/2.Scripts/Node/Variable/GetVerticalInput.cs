using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVerticalInput : GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;
    public override void CheckOutput()
    {
        float_output.Input(Input.GetAxisRaw("Vertical"));
    }
    public override string GetInfoString()
    {
        return "플레이어의 상/하 키에 대한 입력값을 내보냅니다. (-1또는 0 또는 1)";
    }
}
