using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOutput : NotInputNotNodeActivatable, Movable,IResetable
{
    [SerializeField]
    private float autoAssignRadius = 0.5f;
    public ArrowConnecting connect;

    public bool isMoving
    {
        get; set;
    }
    public override void Active()
    {
        //아무것도 하지 않아야함.
        //base.Active();
    }
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
            --isActive;
        //CheckOutput();
    }
    public void CheckOutput()
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll(transform.position - transform.lossyScale, transform.position + transform.lossyScale);
        foreach (Collider2D coll in colls)
        {
            if (coll.transform != transform)
                ProcessOutput(coll);
        }
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
            flag = false;  //colls에서 InputNode를 찾았는지 체크
            colls = Physics2D.OverlapCircleAll(pos, autoAssignRadius);
            if (colls.Length > 0)
            {
                int min_dist_idx = -1;
                for(int i =0; i < colls.Length; ++i)
                {
                    Collider2D coll = colls[i];
                    if (coll.CompareTag("InputNode"))
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
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, autoAssignRadius);
    }

    void ProcessOutput(Collider2D coll)
    {
        switch (connect.input_type)
        {
            case ArrowConnecting.ValueType.ACTIVE:
                Activatable activatable = coll.GetComponent<Activatable>();
                if (activatable != null)
                {
                    if (connect.input_data.active_data)
                    {
                        connect.input_data.active_data = false;
                        base.Active();
                        activatable.Active();
                    }
                    if (isActive >= 1)
                        isActive--;
                }
                break;
            case ArrowConnecting.ValueType.BIG_INT:
                InputParam<BigInt> intparams = coll.GetComponent<InputParam<BigInt>>();
                if (intparams != null && connect.input_data.bigint_data != null)
                {
                    intparams.Input(connect.input_data.bigint_data);
                    connect.input_data.bigint_data = null;
                }
                break;
            case ArrowConnecting.ValueType.BOOL:
                InputParam<bool?> boolParams = coll.GetComponent<InputParam<bool?>>();
                if (boolParams != null && connect.input_data.bool_data.HasValue)
                {
                    boolParams.Input(connect.input_data.bool_data);
                    connect.input_data.bool_data = null;
                }
                break;
            case ArrowConnecting.ValueType.FLOAT:
                InputParam<float?> floatParams = coll.GetComponent<InputParam<float?>>();
                if (floatParams != null && connect.input_data.float_data.HasValue)
                {
                    floatParams.Input(connect.input_data.float_data);
                    connect.input_data.float_data = null;
                }
                break;
            case ArrowConnecting.ValueType.VECTOR2:
                InputParam<Vector2?> vector2Params = coll.GetComponent<InputParam<Vector2?>>();
                if (vector2Params != null && connect.input_data.vector2_data.HasValue)
                {
                    vector2Params.Input(connect.input_data.vector2_data);
                    connect.input_data.vector2_data = null;
                }
                break;
            case ArrowConnecting.ValueType.VECTOR3:
                InputParam<Vector3?> vector3Params = coll.GetComponent<InputParam<Vector3?>>();
                if (vector3Params != null && connect.input_data.vector3_data.HasValue)
                {
                    vector3Params.Input(connect.input_data.vector3_data);
                    connect.input_data.vector3_data = null;
                }
                break;
            case ArrowConnecting.ValueType.STRING:
                InputParam<string> stringParams = coll.GetComponent<InputParam<string>>();
                if (stringParams != null && connect.input_data.string_data != null)
                {
                    stringParams.Input(connect.input_data.string_data);
                    connect.input_data.string_data = null;
                }
                break;
            case ArrowConnecting.ValueType.AUDIO_CLIP:
                InputParam<AudioClip> audioParams = coll.GetComponent<InputParam<AudioClip>>();
                if (audioParams != null && connect.input_data.audio_clip_data != null)
                {
                    audioParams.Input(connect.input_data.audio_clip_data);
                    connect.input_data.audio_clip_data = null;
                }
                break;
            default:
                Debug.LogError("unknown input type");
                break;
        }
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
