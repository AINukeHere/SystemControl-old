using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAvoiderX : GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;
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
        float_output.Input(AvoiderTr.localPosition.x);
    }
    public override string GetInfoString()
    {
        return "Avoider의 x좌표를 내보냅니다.";
    }
}
