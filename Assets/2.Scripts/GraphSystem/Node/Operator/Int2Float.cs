using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int2Float : Operator<int?, float?>
{

    public override void SetDefaultText()
    {
        textMesh.text = "Int2Float";
    }
    public override void CheckOutput()
    {
        if (input[0] != null)
        {
            result[0] = float.Parse(input[0].ToString());
            output[0].Input(result[0]);
            input[0] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 정수값을 실수값으로 변환하여 내보냅니다.";
    }
}
