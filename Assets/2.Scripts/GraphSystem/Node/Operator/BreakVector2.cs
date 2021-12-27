using UnityEngine;

public class BreakVector2 : Operator<Vector2?,float?>
{
    public override void CheckOutput()
    {
#if UNITY_EDITOR
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("BreakVector2 CheckOutput()");
#endif
        if (input[0] != null)
        {
            result[0] = input[0].Value.x;
            result[1] = input[0].Value.y;
            output[0].Input(result[0]);
            output[1].Input(result[1]);
            input[0] = null;
        }
    }
}
