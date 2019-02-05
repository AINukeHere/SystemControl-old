using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNodeCameraY :GetVariable
{
    [SerializeField]
    private FloatOutputModule float_output;
    private Transform cameraTr;

    public override void Awake()
    {
        base.Awake();
        cameraTr = GameObject.FindGameObjectWithTag("NodeCamera").GetComponent<Camera>().transform;
    }
    public override void CheckOutput()
    {
        float_output.Input(cameraTr.position.y);
    }
    public override string GetInfoString()
    {
        return "노드를 비추고있는 카메라의 Y좌표를 내보냅니다.";
    }
}
