using UnityEngine;

public class BreakVector2 : Operator<Vector2?,float?>
{
    public override void SetDefaultText()
    {
        textMesh.text = "Break Vector2";
    }
    public override void CheckOutput()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("BreakVector2 CheckOutput()");
        if (input[0] != null)
        {
            result[0] = input[0].Value.x;
            result[1] = input[0].Value.y;
            output[0].Input(result[0]);
            output[1].Input(result[1]);
            input[0] = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 벡터2를 스칼라값(실수) 둘로 쪼개서 각각 내보냅니다.";
    }
}
