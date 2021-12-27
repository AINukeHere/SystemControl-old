using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int2Float : Operator<int?, float?>
{
    public override void CheckOutput()
    {
        if (input[0] != null)
        {
            result[0] = float.Parse(input[0].ToString());
            output[0].Input(result[0]);
            input[0] = null;
        }
    }
}
