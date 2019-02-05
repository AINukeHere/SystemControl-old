using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Int : Operator<BigInt, BigInt>
{

    public override void SetDefaultText()
    {
        textMesh.text = "+";
    }
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0] + input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 두 정수값을 더한 결과를 내보냅니다.";
    }
}
