using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ConstantString : ConstantNmber<string>
//{
//}

public class ConstantString : GetVariable
{
    [SerializeField]
    private string value;
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
                myTextMesh.text = value;
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
                arrow_input.SendMessage("Input", value);
                //break;    //연결된 모든 엣지들에게 넘겨줌
            }
        }
    }
    public override string GetInfoString()
    {
        return "문장입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}
