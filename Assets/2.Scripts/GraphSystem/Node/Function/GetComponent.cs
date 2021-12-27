using UnityEngine;
using System;

public enum GetComponentTypes
{
    AudioSource,
    Transform,
    Rigidbody
}

public class GetComponent : ActivatableNode
{
    public GameObject value = null;

    public ComponentOutputModule component_output;
    public GetComponentTypes componentType;

    public void Input(GameObject input, int unused = 0)
    {
        if (input != null)
        {
            value = input;
            CheckOutput();
        }
    }
    public override void Update()
    {
        base.Update();
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

    public override void ExpandDisplay()
    {
        if (textMesh)
            textMesh.text = value != null ? $"{nodeName}\n{value.name}" : nodeName;
    }
}
