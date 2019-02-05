using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[ExecuteInEditMode]
public class ArrowConnecting : MonoBehaviour, Movable, ExpandableDisplay, IResetable
{
    //유니티가 이 기능을 허용하지 않음 (개같은 것. 이거 유니티 크래쉬 버그 나는거 잡느라. ㅎ맘ㅎㄴㄷㅎㅁㄴㄷㅎ)
    //[StructLayout(LayoutKind.Explicit)]
    public struct InputData
    {
        //[FieldOffset(0)]
        public bool active_data;
        //[FieldOffset(0)]
        public BigInt bigint_data;
        //[FieldOffset(0)]
        public bool? bool_data;
        //[FieldOffset(0)]
        public float? float_data;
        //[FieldOffset(0)]
        public Vector2? vector2_data;
        //[FieldOffset(0)]
        public Vector3? vector3_data;
        //[FieldOffset(0)]
        public string string_data;
        //[FieldOffset(0)]
        public AudioClip audio_clip_data;
    }

    //Arrow에 관한 속성값
    public GameObject input_node, output_node;
    public enum ValueType
    {
        ACTIVE,
        BIG_INT,
        BOOL,
        FLOAT,
        VECTOR2,
        VECTOR3,
        STRING,
        AUDIO_CLIP
    }
    public ValueType input_type;
    public InputData input_data = new InputData();
    private SpriteRenderer myRenderer;

    //확장형 디스플레이에서 사용할 TextMesh
    public TextMesh textMesh;
    public string visualize_text;


    /// <summary>
    /// 이 메소드는 ArrowInput에서 호출됩니다.
    /// </summary>
    public void SetTextByData()
    {
        switch (input_type)
        {
            case ValueType.ACTIVE:
                visualize_text = input_data.active_data ? "On" : "Off";
                break;
            case ValueType.BIG_INT:
                visualize_text = input_data.bigint_data != null ? input_data.bigint_data.ToString() : "";
                break;
            case ValueType.BOOL:
                visualize_text = input_data.bool_data.HasValue ? input_data.bool_data.Value.ToString() : "";
                break;
            case ValueType.FLOAT:
                visualize_text = input_data.float_data.HasValue ? input_data.float_data.Value.ToString("N") : "";
                break;
            case ValueType.VECTOR2:
                visualize_text = input_data.vector2_data.HasValue ? input_data.vector2_data.Value.ToString("N") : "";
                break;
            case ValueType.VECTOR3:
                visualize_text = input_data.vector3_data.HasValue ? input_data.vector3_data.Value.ToString("N") : "";
                break;
            case ValueType.STRING:
                visualize_text = input_data.string_data != null ? input_data.string_data : "";
                break;
            case ValueType.AUDIO_CLIP:
                visualize_text = input_data.audio_clip_data != null ? input_data.audio_clip_data.name : "";
                break;
            default:
                Debug.LogError("unknown type");
                break;
        }
        TextUpdate();
        ColorCange();
    }
    public bool isMoving { get; set; }
    public void Move(Vector2 pos)
    {
        //회전을 생각한 오버랩박스
        Collider2D[] colls = Physics2D.OverlapBoxAll(pos, transform.lossyScale * 2, transform.rotation.eulerAngles.z);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }

    public void Awake()
    {
        origin_pos = transform.position;
        origin_rot = transform.rotation;
        myRenderer = GetComponent<SpriteRenderer>();
        TextMesh mesh = GetComponentInChildren<TextMesh>();
        if(mesh != null)
            mesh.GetComponent<Renderer>().sortingLayerID = GetComponent<Renderer>().sortingLayerID;
    }
    void TextUpdate()
    {
        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
    }
    void Update()
    {
        ProcessMovement();

        visualize_text = input_type == ValueType.ACTIVE ? "Off" : "";
        
        TextUpdate();
        ColorCange();
    }

    //Arrow의 움직임 처리
    void ProcessMovement()
    {


        //사용자에 의해 평행이동되지않을 때
        if (!isMoving || (input_node.GetComponent<ArrowInput>().isMoving || output_node.GetComponent<ArrowOutput>().isMoving))
        {
            //input과 ouput의 위치만 보고 방향 스케일 위치를 맞춤
            Vector2 v = output_node.transform.position - input_node.transform.position;
            input_node.transform.rotation = output_node.transform.rotation = transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(v.y, v.x) / Mathf.Deg2Rad);
            Vector3 new_pos = Vector2.Lerp(output_node.transform.position, input_node.transform.position, 0.5f);
            new_pos.z = input_node.transform.position.z;
            transform.position = new_pos;
            float value = Vector2.Distance(input_node.transform.position, transform.position) - input_node.transform.localScale.x;
            transform.localScale = new Vector3(value, 1, 1);
            if(textMesh != null)
                textMesh.gameObject.transform.localScale = new Vector3(1 / value, 1, 1);

        }
        else
        {
            //connect의 위치와 스케일을 보고 input과 output의 transform을 설정
            input_node.transform.position = transform.position + (transform.localScale.x + 0.5f) * transform.right * -1;
            output_node.transform.position = transform.position + (transform.localScale.x + 0.5f) * transform.right;
        }
    }
    //value type color 변경
    void ColorCange()
    {
        switch (input_type)
        {
            case ValueType.ACTIVE:
                myRenderer.color = Color.white;
                break;
            case ValueType.BIG_INT:
                myRenderer.color = Color.cyan;
                break;
            case ValueType.BOOL:
                myRenderer.color = Color.red;
                break;
            case ValueType.FLOAT:
                myRenderer.color = Color.green;
                break;
            case ValueType.VECTOR2:
            case ValueType.VECTOR3:
                myRenderer.color = Color.yellow;
                break;
            case ValueType.STRING:
                myRenderer.color = Color.magenta;
                break;
            case ValueType.AUDIO_CLIP:
                myRenderer.color = new Color(210 / 255f, 105 / 255f, 30 / 255f, 1); //chocoate
                break;
            default:
                Debug.LogError("unknown input type");
                break;
        }
    }
    public void FixParentPosition()
    {
        //Arrow 를 connect로 옮기는 작업
        Vector3 delta = transform.localPosition;
        transform.localPosition = Vector3.zero;
        transform.parent.transform.position += delta;
        //connect의 위치와 스케일을 보고 input과 output의 transform을 설정
        input_node.transform.position = transform.position + (transform.localScale.x + 0.5f) * transform.right * -1;
        output_node.transform.position = transform.position + (transform.localScale.x + 0.5f) * transform.right;
    }


    public bool isExpanded { get; set; }
    public void NormalDisplay()
    {
        if(textMesh != null)
            textMesh.text = visualize_text = "";
    }
    public void ExpandDisplay()
    {
        if (textMesh != null)
            textMesh.text = visualize_text;
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
    public void ResetNode()
    {
        transform.position = origin_pos;
        transform.rotation = origin_rot;
    }
}
