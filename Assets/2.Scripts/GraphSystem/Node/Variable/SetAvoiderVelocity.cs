using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAvoiderVelocity : SetVariable<Vector2?>
{
    [SerializeField]
    private Vector2OutputModule vector2_output;

    private Rigidbody2D AvoiderRigid2D;

    public override void Awake()
    {
        base.Awake();
        GameObject avoider = GameObject.FindGameObjectWithTag("Avoider");
        if (avoider != null)
            AvoiderRigid2D = avoider.GetComponentInChildren<Rigidbody2D>();
    }
    public override void CheckOutput()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("SetAvoiderVelocity CheckOutput()");
        if (isActive >= 2 && value != null)
        {
            if (gameObject.name.EndsWith("(Test)"))
                Debug.Log("SetAvoiderVelocity CheckOutput() : " + value.Value);
            AvoiderRigid2D.velocity = value.Value;
            vector2_output.Input(AvoiderRigid2D.velocity);
            active_output.Active();
            isActive--;

            value = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 벡터2로 Avoider의 속도(방향이 있는 값)를 새로 설정합니다.";
    }
}