﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNodeCameraX : GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;
    private Transform cameraTr;

    public override void Awake()
    {
        base.Awake();
        cameraTr = GameObject.FindGameObjectWithTag("NodeCamera").transform;
    }
    
    public override void CheckOutput()
    {
        float_output.Input(cameraTr.position.x);
    }
    public override string GetInfoString()
    {
        return "노드를 비추고있는 카메라의 X좌표를 내보냅니다.";
    }
}
