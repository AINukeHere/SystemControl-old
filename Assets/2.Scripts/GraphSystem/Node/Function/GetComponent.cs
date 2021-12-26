using UnityEngine;
using System;

public enum GetComponentTypes
{
    AudioSource,
    Transform,
    Rigidbody
}

public class GetComponent : ActivatableNode,IExpandableDisplay
{
    private TextMesh myTextMesh;
    private string nodeName = "GetComponent";

    public GameObject value = null;

    public ComponentOutputModule component_output;
    public GetComponentTypes componentType;

    public bool isExpanded { get; set; }
    public void NormalDisplay() {
        if(myTextMesh)
            myTextMesh.text = nodeName;
    }
    public void ExpandDisplay()
    {
        if (myTextMesh)
            myTextMesh.text = value != null ? $"{nodeName}\n{value.name}": nodeName;
    }

    public void Input(GameObject input, int unused = 0)
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

        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();

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
            component_output.Input(value.GetComponent(componentType.ToString()));
            component_output.CheckOutput();
            isActive--;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 음원이 현재 실행중인지를 반환합니다.";
    }
}
