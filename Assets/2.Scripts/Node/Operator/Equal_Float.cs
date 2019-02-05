using UnityEngine;

public class Equal_Float : Operator<float?,bool?>
{
    public override void SetDefaultText()
    {
        textMesh.text = "==";
    }
    public override void CheckOutput()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("MakeVector2 CheckOutput()");
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0] == input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 두 실수가 같은지 판별하여 참 또는 거짓을 내보냅니다.";
    }
}
