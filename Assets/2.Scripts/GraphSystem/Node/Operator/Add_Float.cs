using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Float : Operator<float?, float?>
{

    public override void SetDefaultText()
    {
        textMesh.text = "+";
    }
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0].Value + input[1].Value;
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 두 실수값을 더한 결과를 내보냅니다.";
    }
}
