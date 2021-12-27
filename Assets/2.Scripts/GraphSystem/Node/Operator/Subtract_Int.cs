using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtract_Int : Operator<int?, int?>{
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0] - input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
}
