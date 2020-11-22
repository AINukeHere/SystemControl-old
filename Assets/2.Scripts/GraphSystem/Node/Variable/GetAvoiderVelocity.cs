using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAvoiderVelocity : GetVariable
{
    [SerializeField]
    private Vector2OutputModule vector2_output;
    private Rigidbody2D AvoiderRigid2D;

    public override void Awake()
    {
        base.Awake();
        GameObject avoider = GameObject.FindGameObjectWithTag("Avoider");
        if (avoider != null)
            AvoiderRigid2D = avoider.GetComponent<Rigidbody2D>();
    }
    public override void CheckOutput()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("GetAvoiderVelocity CheckOutput() : "+AvoiderRigid2D.velocity);
        vector2_output.Input(AvoiderRigid2D.velocity);
    }
    public override string GetInfoString()
    {
        return "Avoider의 현재 속도(방향이 있는 값)를 벡터2로 내보냅니다.";
    }
}
