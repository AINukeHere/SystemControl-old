using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFloat : GetVariable
{
    //nullable 변수가 직렬화될 수 없음.
    [SerializeField]
    private float value;
    [SerializeField]
    private bool HasValue = true;

    private TextMesh myTextMesh;
    //소수점 2자리까지만 출력
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value.ToString("N");
            else
                myTextMesh.text = "NULL";
        }
    }
    public override void CheckOutput()
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll(transform.position - transform.lossyScale, transform.position + transform.lossyScale);
        foreach (Collider2D coll in colls)
        {
            ArrowInput arrow_input = coll.GetComponent<ArrowInput>();
            if (arrow_input != null && HasValue)
            {
                if (gameObject.name.EndsWith("(Test)"))
                    Debug.Log("ConstantFloat CheckOutput()" + value);
                arrow_input.SendMessage("Input", new float?(value));
                //break;    //연결된 모든 엣지들에게 넘겨줌
            }
        }
    }
    public override string GetInfoString()
    {
        return "실수값입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}
