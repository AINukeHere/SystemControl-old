using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameCameraPos : GetVariable
{
    [SerializeField]
    private Vector3OutputModule vector3_output;
    private Transform cameraTr;

    public override void Awake()
    {
        base.Awake();
        cameraTr = GameObject.FindGameObjectWithTag("GameCamera").transform;
    }
    
    public override void CheckOutput()
    {
        vector3_output.Input(cameraTr.position);
    }
    public override string GetInfoString()
    {
        return "게임을 비추고 있는 카메라의 좌표를 벡터3로 내보냅니다.";
    }
}
