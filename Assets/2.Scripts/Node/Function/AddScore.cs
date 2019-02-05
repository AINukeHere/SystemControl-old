using UnityEngine;

public class AddScore : NotInputActivatable,ExpandableDisplay,Movable
{
    private TextMesh myTextMesh;

    public BigInt value = null;
    public GameObject disappearText;

    public ActiveOutputModule active_output;

    public bool isExpanded { get; set; }
    public void NormalDisplay() {
        myTextMesh.text = "AddScore";
    }
    public void ExpandDisplay() {
        myTextMesh.text = value != null ? "AddScore\n" + value.ToString() : "AddScore\n";
    }

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
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value != null)
        {
            GameManager.instance.AddScore(value);
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
        return "현재 Score에 이 노드로 입력된 값만큼 더합니다.";
    }
}
