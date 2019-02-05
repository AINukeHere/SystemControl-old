using UnityEngine;

public class SetActiveWall : NotInputActivatable,ExpandableDisplay,Movable
{
    private TextMesh myTextMesh;
    public ActiveOutputModule active_output;

    //values
    BigInt[] value = new BigInt[2];
    bool? boolValue = null;


    public void NormalDisplay()
    {
        if (myTextMesh)
            myTextMesh.text = "Set\nActiveWall";
    }
    public void ExpandDisplay()
    {
        if (myTextMesh)
            myTextMesh.text = "Set\nActiveWall";
    }

    public void Input(BigInt input, int index = 0)
    {
        if (input != null)
        {
            value[index] = input;
            CheckOutput();
        }
    }
    public void Input(bool? input, int index = 0)
    {
        if (input != null)
        {
            boolValue = input;
            CheckOutput();
        }
    }

    public override void Active()
    {
        base.Active();
        CheckOutput();
    }
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
    }

    public override void Update()
    {
        base.Update();

        if (isActive >= 1)
        {
            //CheckOutput();
            isActive--;
        }
        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
        value[0] = value[1] = null;
        boolValue = null;
    }
    public override void CheckOutput()
    {
		if (isActive >= 2 && value[0] != null && value[1] != null && boolValue.HasValue)
        {
            DotConnectManager.instance.SetActiveWall(value[0], value[1], boolValue.Value);
            active_output.Active();
            isActive--;
        }
    }
    public bool isExpanded { get; set; }
    public bool isMoving
    {
        get; set;
    }
    public void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }
    public override string GetInfoString()
    {
        return "입력된 두 점을 잇는 벽을 입력된 참또는 거짓에 따라 생성하거나 제거합니다.";
    }
}
