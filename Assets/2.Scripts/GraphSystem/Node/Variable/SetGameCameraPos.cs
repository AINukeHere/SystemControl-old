using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameCameraPos : SetVariable<Vector3?>
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
        if (isActive >= 2 && value.HasValue)
        {
            cameraTr.localPosition = value.Value; ;
            vector3_output.Input(value.Value);
            active_output.Active();
            isActive--;
        }
    }
}
