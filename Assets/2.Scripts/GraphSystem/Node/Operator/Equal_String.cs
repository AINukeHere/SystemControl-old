﻿using UnityEngine;
public class Equal_String : Operator<string,bool?>
{
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
#if UNITY_EDITOR
            if (gameObject.name.EndsWith("(Test)"))
                Debug.Log(input[0] + "과" + input[1] + "비교");
#endif
            result[0] = input[0] == input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
}
