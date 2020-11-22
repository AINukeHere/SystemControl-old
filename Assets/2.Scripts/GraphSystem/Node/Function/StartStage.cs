using UnityEngine;

public class StartStage : ActivatableNode
{
    public int? value;
    public GameObject disappearText;

    public ActiveOutputModule active_output;

    public void Input(int? input, int unused = 0)
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
            AvoidGameManager.instance.StartStage(value.Value);
            DisappearText text = Instantiate(disappearText, transform.position, Quaternion.identity).GetComponent<DisappearText>();
            text.text = value.ToString();
            value = null;
            active_output.Active();
            isActive--;
        }
    }

    public override string GetInfoString()
    {
        return "입력된 값의 스테이지를 시작합니다.";
    }
}
