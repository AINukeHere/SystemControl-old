using UnityEngine;

public class StartStage : NotInputActivatable,Movable
{
    public BigInt value;
    public GameObject disappearText;

    public ActiveOutputModule active_output;

    public void Input(BigInt input, int unused = 0)
    {
        if (input != null)
        {
            value = input;
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
        value = null;
        
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        if (isActive >= 1)
        {
            //CheckOutput();
            isActive--;
        }
        value = null;
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value != null)
        {
            Debug.Log("StageStart(" + value.ToString() + ")");
            AvoidGameManager.instance.StartStage(value);
            DisappearText text = Instantiate(disappearText, transform.position, Quaternion.identity).GetComponent<DisappearText>();
            text.text = value.ToString();
            value = null;
            active_output.Active();
            isActive--;
        }
    }

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
        return "입력된 값의 스테이지를 시작합니다.";
    }
}
