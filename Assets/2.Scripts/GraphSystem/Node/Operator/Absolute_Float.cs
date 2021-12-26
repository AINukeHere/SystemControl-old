using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absolute_Float : Operator<float?, float?>
{

    public override void SetDefaultText()
    {
        textMesh.text = "+";
    }
    public override void CheckOutput()
    {
        if (input[0] != null)
        {
            result[0] = Mathf.Abs(input[0].Value);
            output[0].Input(result[0]);
            input[0] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 실수값의 절대값을 내보냅니다.";
    }
}
