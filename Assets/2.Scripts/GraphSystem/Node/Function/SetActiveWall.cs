﻿using UnityEngine;

public class SetActiveWall : ActivatableNode
{
    public ActiveOutputModule active_output;
    //values
    int?[] value = new int?[2];
    bool? boolValue = null;

    public void Input(int? input, int index = 0)
    {
        if (input != null)
        {
            value[index] = input;
            CheckOutput();
        }
    }
    public void Input(bool? input, int index = 0)
    {
        if (input != null)
        {
            boolValue = input;
            CheckOutput();
        }
    }

    public override void Update()
    {
        base.Update();
        value[0] = value[1] = null;
        boolValue = null;
    }
    public override void CheckOutput()
    {
		if (isActive >= 2 && value[0] != null && value[1] != null && boolValue.HasValue)
        {
            DotConnectManager.instance.SetActiveWall(value[0].Value, value[1].Value, boolValue.Value);
            active_output.Active();
            isActive--;
        }
    }
    public override void ExpandDisplay()
    {
        if (textMesh)
        {
            textMesh.text = $"{nodeName}\n" +
                            $"{(value[0].HasValue ? value[0].Value.ToString() : "값없음")}\n" +
                            $"{(value[1].HasValue ? value[1].Value.ToString() : "값없음")}\n" +
                            $"{(boolValue.HasValue ? boolValue.Value.ToString() : "값없음")}";
        }
    }
}
