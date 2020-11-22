using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : Event,IExpandableDisplay
{
    [SerializeField]
    private KeyCode key;
    private TextMesh myTextMesh;
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(key))
        {
            Active();
            active_output.Active();
        }
        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
    }
    //ExpandableDisplay
    public bool isExpanded
    {
        get; set;
    }
    public void NormalDisplay()
    {
        myTextMesh.text = "Key\nEvent";
    }
    public void ExpandDisplay()
    {
        myTextMesh.text = "Key\nEvent";
        myTextMesh.text += "\n"+key.ToString();
    }
    public override string GetInfoString()
    {
        return "입력된 값이 "+ key.ToString() + "이라면 실행신호를 내보냅니다.";
    }
}
