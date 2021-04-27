using UnityEngine;
using System;
[System.Serializable]
public class StringCase : NotInputNotNodeActivatable,IExpandableDisplay
{
    public string case_value;
    public ActiveOutputModule active_output;
    public TextMesh textMesh;

	//stringCase에 값이 들어올 때마다 SwitchOnString의 CheckOutput 호출
	private  SwitchOnString myHead;

    public override void Awake()
    {
        base.Awake();
        textMesh = GetComponentInChildren<TextMesh>();
    }
    public override void Update()
    {
        base.Update();
        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
		case_value = null;
        if (isActive >= 1)
            isActive--;
    }
    public void Input(string input, int unused = 0)
    {
        if (input != null)
            case_value = input;
		myHead.CheckOutput ();
    }
	public bool isExpanded {
		get;
		set;
	}
	public void NormalDisplay()
	{
        if(textMesh != null)
		    textMesh.text = "";
	}
	public void ExpandDisplay()
    {
        if (textMesh != null)
        {
            if (case_value != null)
                textMesh.text = case_value;
            else
                textMesh.text = "값없음";
        }
	}
	public void SetSwitchHead(SwitchOnString head)
	{
		myHead = head;
	}
}