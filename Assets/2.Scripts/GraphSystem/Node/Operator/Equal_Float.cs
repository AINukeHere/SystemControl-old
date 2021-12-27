using UnityEngine;

public class Equal_Float : Operator<float?,bool?>
{
    public override void CheckOutput()
    {
#if UNITY_EDITOR
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("MakeVector2 CheckOutput()");
#endif
        if (input[0] != null && input[1] != null)
        {
            result[0] = input[0] == input[1];
            output[0].Input(result[0]);
            input[0] = null;
            input[1] = null;
        }
    }
}
