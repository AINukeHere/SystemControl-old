using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : NotInputActivatable, ExpandableDisplay, Movable
{
    private TextMesh myTextMesh;
    public ActiveOutputModule active_output;

    //values
    string value = null;


    public void NormalDisplay()
    {
        if (myTextMesh != null)
            myTextMesh.text = "LoadScene";
    }
    public void ExpandDisplay()
    {
        if (myTextMesh != null)
        {
            myTextMesh.text = "LoadScene" + (value != null ? ("\n" + value) : "");
        }
    }

    public void Input(string input, int index = 0)
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
        else
            NormalDisplay();
        value = null;
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value != null)
        {
            SceneManager.LoadScene(value);
            active_output.Active();
            isActive--;
        }
    }
    public bool isExpanded { get; set; }
    public bool isMoving
    {
        get;
        set;
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
        return "입력된 씬이름의 씬을 로드합니다.";
    }
}
