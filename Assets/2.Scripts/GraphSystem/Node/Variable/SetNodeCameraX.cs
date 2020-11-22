using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNodeCameraX : SetVariable<float?>
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
        if (isActive >= 2 && value.HasValue)
        {
            Vector3 temp = cameraTr.position;
            temp.x = value.Value;
            cameraTr.position = temp;
            float_output.Input(temp.x);
            active_output.Active();
            isActive--;

            value = null;
        }
    }
    public override string GetInfoString()
    {
        return "입력된 실수값으로 노드를 비추고 있는 카메라의 x좌표를 새로 설정합니다.";
    }
}
