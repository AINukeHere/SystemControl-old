using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakVector3 : Operator<Vector3?,float?>
{
    public override void CheckOutput()
    {
        if (input[0] != null)
        {
            result[0] = input[0].Value.x;
            result[1] = input[0].Value.y;
            result[2] = input[0].Value.z;
            output[0].Input(result[0]);
            output[1].Input(result[1]);
            output[2].Input(result[2]);
            input[0] = null;
        }
    }
}
