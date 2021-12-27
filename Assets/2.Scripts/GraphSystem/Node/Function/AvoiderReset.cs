using UnityEngine;

public class AvoiderReset : ActivatableNode
{
    private Transform avoiderTr;

    public ActiveOutputModule active_output;
    public override void Awake()
    {
        base.Awake();
        avoiderTr = GameObject.FindGameObjectWithTag("Avoider").transform;
    }
    public override void CheckOutput()
    {
        if (isActive >= 2)
        {
            avoiderTr.position = AvoidGameManager.instance.GetStartPosition();
            avoiderTr.localScale = Vector3.one;
            avoiderTr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            active_output.Active();
            isActive--;
        }
    }

    public override void ExpandDisplay()
    {
        NormalDisplay();
    }
}
