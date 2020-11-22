using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : ActivatableNode, IExpandableDisplay
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
    public override string GetInfoString()
    {
        return "입력된 씬이름의 씬을 로드합니다.";
    }
}
