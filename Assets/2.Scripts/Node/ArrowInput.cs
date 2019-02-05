using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInput : Activatable, Movable, IResetable,
    InputParam<BigInt>,
    InputParam<bool?>,
    InputParam<float?>,
    InputParam<Vector2?>,
    InputParam<Vector3?>,
    InputParam<string>,
    InputParam<AudioClip>
{
    [SerializeField]
    private float autoAssignRadius = 0.5f;
    public ArrowConnecting connect;
    public ArrowOutput arrow_output;
    public bool isMoving
    {
        get; set;
    }

    //Stack Overflow를 막기위한 변수
    private bool isSendData = false;
    //전송가능한 상태로 강제로 바꿉니다.
    public void ActiveSendableState()
    {
        isSendData = false;
    }


    //----------Input처리시작----------//
    public override void Active()
    {
        base.Active();
        if (isActive >= 2)
        {
            if (!isSendData)
            {
                isSendData = true;

                connect.input_type = ArrowConnecting.ValueType.ACTIVE;
                connect.input_data.active_data = true;
                connect.SetTextByData();
                arrow_output.CheckOutput();

            }
        }
    }
    public void Input(BigInt input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.bigint_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.ToString();
            connect.input_type = ArrowConnecting.ValueType.BIG_INT;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(bool? input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.bool_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.Value.ToString();
            connect.input_type = ArrowConnecting.ValueType.BOOL;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(float? input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.float_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.Value.ToString("N");
            connect.input_type = ArrowConnecting.ValueType.FLOAT;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(Vector2? input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.vector2_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.Value.ToString("N");
            connect.input_type = ArrowConnecting.ValueType.VECTOR2;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(Vector3? input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.vector3_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.Value.ToString("N");
            connect.input_type = ArrowConnecting.ValueType.VECTOR3;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(string input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.string_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input;
            connect.input_type = ArrowConnecting.ValueType.STRING;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    public void Input(AudioClip input)
    {
        if (!isSendData)
        {
            isSendData = true;

            connect.input_data.audio_clip_data = input;
            if (connect.isExpanded && connect.textMesh != null)
                connect.textMesh.text = input.name;
            connect.input_type = ArrowConnecting.ValueType.AUDIO_CLIP;
            connect.SetTextByData();
            arrow_output.CheckOutput();
        }
    }
    //----------Input처리끝------------//

    
    public override void Awake()
    {
        base.Awake();
        origin_pos = transform.position;
        origin_rot = transform.rotation;
    }
    public override void Update()
    {
        base.Update();
        if (isActive >= 1)
        {
            isActive--;
        }

        isSendData = false;
    }
    public void Move(Vector2 pos)
    {
        bool flag;
        Collider2D[] colls;
        //회전을 생각한 오버랩박스
        colls = Physics2D.OverlapBoxAll(pos, transform.lossyScale * 2, transform.rotation.eulerAngles.z);
        flag = false;
        foreach (Collider2D coll in colls)
        {
            if (coll.CompareTag("LockField"))
            {
                flag = true;
                break;
            }
        }
        //이동하려는 pos가 LockField의 내부가 아닐 때
        if (!flag)
        {
            //output모듈에 자석기능

            flag = false;  //colls에서 OutputNode를 찾았는지 체크
            colls = Physics2D.OverlapCircleAll(pos, autoAssignRadius);
            if (colls.Length >0)
            {
                int min_dist_idx = -1;
                for (int i = 0; i < colls.Length; ++i)
                {
                    Collider2D coll = colls[i];
                    if (coll.CompareTag("OutputNode"))
                    {
                        //아직 최저가 결정되지 않았거나 최저보다 더 적을 때 갱신
                        if (min_dist_idx == -1 ||
                            Vector3.Distance(coll.transform.position, pos) < Vector3.Distance(colls[min_dist_idx].transform.position, pos))
                            min_dist_idx = i;
                    }
                }
                if (min_dist_idx != -1)
                {
                    transform.position = colls[min_dist_idx].transform.position;
                    flag = true;
                }
            }
            if (!flag)
            {
                transform.position = pos;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, autoAssignRadius);
        //Gizmos.DrawSphere(transform.position, Mathf.Max(transform.lossyScale.x, transform.lossyScale.y));
    }


    /// <summary>
    /// Resetable부분
    /// </summary>
    public Vector3 origin_pos
    {
        get; set;
    }
    public Quaternion origin_rot
    {
        get; set;
    }
    //노드를 리셋합니다. 폴더안에 있을 땐 무시됩니다.
    public void ResetNode()
    {
        //부모가 없을 때만 리셋 (폴더아래에 있을땐 무반응)
        if (transform.parent.parent == null)
        {
            transform.position = origin_pos;
            transform.rotation = origin_rot;
        }
    }
}