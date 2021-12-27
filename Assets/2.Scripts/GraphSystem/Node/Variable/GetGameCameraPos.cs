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
    public override void ExpandDisplay()
    {
        textMesh.text = cameraTr.position.ToString();
    }
}
