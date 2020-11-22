using UnityEngine;

public class SubtractScore : ActivatableNode,IExpandableDisplay
{
    private TextMesh myTextMesh;

    public BigInt value = null;
    public GameObject disappearText;

    public ActiveOutputModule active_output;

    public bool isExpanded { get; set; }
    public void NormalDisplay()
    {
        myTextMesh.text = "SubtractScore";
    }
    public void ExpandDisplay()
    {
        myTextMesh.text = value != null ? "SubtractScore\n" + value.ToString() : "SubtractScore\n";
    }

    public void Input(BigInt input, int unused = 0)
    {
        if (input != null)
        {
            value = input.Clone();
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
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value != null)
        {
            GameManager.instance.SubtractScore(value);
            DisappearText text = Instantiate(disappearText, transform.position, Quaternion.identity).GetComponent<DisappearText>();
            text.text = (-value).ToString();
            value = null;
            active_output.Active();
            isActive--;
        }
    }

    public override string GetInfoString()
    {
        return "입력된 값만큼 점수를 낮춥니다.";
    }
}
