using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAvoiderScale : GetVariable
{
    [SerializeField]
    private Vector2OutputModule vector2_output;
    private Transform AvoiderTr;

    public override void Awake()
    {
        base.Awake();
        GameObject avoider = GameObject.FindGameObjectWithTag("Avoider");
        if (avoider != null)
            AvoiderTr = avoider.GetComponent<Transform>();
    }

    public override void CheckOutput()
    {
        vector2_output.Input(AvoiderTr.localScale);
    }
    public override string GetInfoString()
    {
        return "Avoider의 크기를 벡터2로 내보냅니다.";
    }
}
