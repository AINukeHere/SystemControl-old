using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brench : ActivatableNode, IExpandableDisplay
{ 
    private TextMesh myTextMesh;

    public bool? value;

    public ActiveOutputModule active_output1, active_output2;

    public bool isExpanded
    {
        get; set;
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
            isActive--;
        }

        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
        value = null;
    }

    public override void Active()
    {
        base.Active();
        if (name.EndsWith("(Test)"))
            Debug.Log("BrenchActive");
        CheckOutput();
    }
    public void Input(bool? input, int unused = 0)
    {
        if (input.HasValue)
        {
            if (name.EndsWith("(Test)"))
                Debug.Log("BrenchInput : " + input.ToString());
            value = input;
            CheckOutput();
        }
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value.HasValue)
        {
            if (name.EndsWith("(Test)"))
                Debug.Log("CheckOutput : " + value);
            if (value.Value)
                active_output1.Active();
            else
                active_output2.Active();
            isActive--;
        }
    }
    public void NormalDisplay()
    {
        if (myTextMesh != null)
            myTextMesh.text = "Brench";
    }
    public void ExpandDisplay()
    {
        if (myTextMesh != null)
        {
            myTextMesh.text = $"Brench\n{(value.HasValue ? value.ToString() : "값없음")}";
        }
    }
    public override string GetInfoString()
    {
        return "입력된 참또는 거짓에 따라 참일경우 실행신호를 오른쪽상단모듈로 보내고 거짓일 경우 오른쪽 하단모듈로 내보냅니다.";
    }
}
