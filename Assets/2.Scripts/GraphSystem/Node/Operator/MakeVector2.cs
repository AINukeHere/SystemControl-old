using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeVector2 : Operator<float?,Vector2?>
{
    public override void SetDefaultText()
    {
        textMesh.text = "Make Vector2";
    }
    public override void CheckOutput()
    {
        if (input[0] != null && input[1] != null)
        {
            if (gameObject.name.EndsWith("(Test)"))
                Debug.Log("MakeVector2 CheckOutput() : made vector");
            result[0] = new Vector2(input[0].Value, input[1].Value);
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 두 실수값으로 벡터2를 만들어 내보냅니다.";
    }
}
