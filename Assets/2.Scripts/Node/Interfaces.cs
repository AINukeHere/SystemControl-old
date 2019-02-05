using System;
using UnityEngine;
using UnityEngine.Events;

//위치,방향이 리셋가능한 인터페이스입니다.
public interface IResetable
{
    Vector3 origin_pos
    {
        get; set;
    }
    Quaternion origin_rot
    {
        get; set;
    }
    void Awake();
    void ResetNode();
}
//설명을 보여줄수 있는 인터페이스입니다.
public interface IHasInfo
{
    string GetInfoString();
}
//모든 노드가 상속받는 클래스입니다.
public abstract class Node : MonoBehaviour, IResetable, IHasInfo
{
    public Vector3 origin_pos
    {
        get; set;
    }
    public Quaternion origin_rot
    {
        get; set;
    }
    public virtual void Awake()
    {
        origin_pos = transform.position;
        origin_rot = transform.rotation;
    }
    //노드를 리셋합니다. 폴더안에 있을 땐 무시됩니다.
    public void ResetNode()
    {
        //부모가 없을 때만 리셋 (폴더아래에 있을땐 무반응)
        if (transform.parent == null)
        {
            transform.position = origin_pos;
            transform.rotation = origin_rot;
        }
    }
    public abstract void CheckOutput();
    public abstract string GetInfoString();
}
//ArrowOutput이 Active시키지 않는 활성화가능한 노드클래스
public abstract class NotInputActivatable : Node
{
    public int isActive;

    protected SpriteRenderer myRenderer;
    public virtual void Active()
    {
		isActive = 2;
		if (TutorialManager.instance != null) {
			TutorialManager.instance.NodeActivated (gameObject.name);
		}
    }
    public override void Awake()
    {
        base.Awake();
        myRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Update()
    {
        Color temp = myRenderer.color;
        if (isActive >= 1)
            temp.a = 1;
        else
            temp.a = 0.5f;
        myRenderer.color = temp;
    }
}
//ArrowOutput이 Active시키지 않고 노드가 아닌 활성화클래스
public class NotInputNotNodeActivatable : MonoBehaviour
{
    public int isActive;

    protected SpriteRenderer myRenderer;
    public virtual void Active()
    {
        isActive = 2;
    }
    public virtual void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Update()
    {
        Color temp = myRenderer.color;
        if (isActive >= 1)
            temp.a = 1;
        else
            temp.a = 0.5f;
        myRenderer.color = temp;
    }
}
//ArrowOutput이 찾아서 Active하는 모듈클래스
public class Activatable : MonoBehaviour
{
    public int isActive;

    protected SpriteRenderer myRenderer;
    protected Rigidbody2D rigid2D;
    public virtual void Active()
    {
        isActive = 2;
    }
    public virtual void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        rigid2D = GetComponent<Rigidbody2D>();

        Color temp = myRenderer.color;
        temp.a = 1;
        myRenderer.color = temp;
    }
    public virtual void Update()
    {
        //Color temp = myRenderer.color;
        //if (isActive >= 1)
        //    temp.a = 1;
        //else
        //    temp.a = 0.5f;
        //myRenderer.color = temp;
        rigid2D.Sleep();
        rigid2D.WakeUp();
    }

}
public interface Movable
{
    bool isMoving
    {
        get; set;
    }
    void Move(Vector2 pos);
}
//ArrowOutput이 이걸 보고 전달합니다.
public interface InputParam<T>
{
    void Input(T input);
}
public interface ExpandableDisplay
{
    bool isExpanded
    {
        get; set;
    }
    void NormalDisplay();
    void ExpandDisplay();
}
//모든 연산자 노드 부모
/// <summary>
/// T : 인풋타입 R : 결과타입
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="R"></typeparam>
public abstract class Operator<T, R> : Node, Movable, ExpandableDisplay
{
    protected TextMesh textMesh;
    //인풋모듈에서 어싸인
    public int input_size = 0, result_size = 0;
    protected T[] input;
    protected R[] result;

    //Awake에서 어싸인
    protected OutputModule<R>[] output;


    public override void Awake()
    {
        base.Awake();
        input = new T[input_size];
        result = new R[result_size];
        textMesh = GetComponentInChildren<TextMesh>();
        output = GetComponentsInChildren<OutputModule<R>>();
        if (output == null)
            Debug.LogError("RelationalOperator can't find output module");
    }
    public virtual void Update()
    {
        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("Operator default(T)");
        for (int i = 0; i < input.Length; ++i)
        {
            input[i] = default(T);
        }
    }
    public abstract void SetDefaultText();


    public void Input(T input, int idx)
    {
        if (input != null)
        {
            if (gameObject.name.EndsWith("(Test)"))
                Debug.Log("Operator CheckOutput() : " + input.ToString());
            this.input[idx] = input;
            CheckOutput();
        }
    }


    public virtual void NormalDisplay()
    {
        if(textMesh != null)
            SetDefaultText();
    }
    public virtual void ExpandDisplay()
    {

        if (textMesh != null)
        {
            SetDefaultText();
            if (result != null)
            {
                foreach (R r in result)
                    if (r != null)
                        textMesh.text += ("\n" + r.ToString());
            }
        }
    }
    public bool isMoving { get; set; }
    public bool isExpanded { get; set; }
    public virtual void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }
}

/*
InputModule 부모는 만들 수 없음. Generic Programming Seriallize 지원안함 ParamEvent<T>가 직렬화 될 수 없음.
[System.Serializable]
public class ParamEvent<T> : UnityEvent<T, int> { }
public abstract class InputModule<T> : MonoBehaviour, InputParam<T>
{
    [SerializeField]
    public ParamEvent<T> destination;
    [SerializeField]
    protected int input_index;
    public abstract void Input(T input);
}
*/

//OutputModule 부모
public abstract class OutputModule<T> : MonoBehaviour, InputParam<T>
{
    //protected bool isCheckOutput = false;
    protected T input;
    public virtual void Input(T input)
    {
        if (input != null)
        {
            this.input = input;
            CheckOutput();
        }
    }
    public virtual void Update()
    {
    }
    public void CheckOutput()
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll(transform.position - transform.lossyScale, transform.position + transform.lossyScale);
        foreach (Collider2D coll in colls)
        {
            ArrowInput arrow_input = coll.GetComponent<ArrowInput>();
            if (arrow_input != null && input != null)
            {
                arrow_input.SendMessage("Input", input);
                //break;    //연결된 모든 엣지들에게 넘겨줌
            }
        }
        AfterInputCallBack();
    }

    public abstract void AfterInputCallBack();
}

//변수Get 노드 부모
public abstract class GetVariable : Node, Movable
{
    private bool bCheckOutput = false;
    public override void Awake()
    {
        base.Awake();
    }
	public virtual void Start()
	{
		if (gameObject.name.EndsWith("(Test)"))
			Debug.Log("GetVariable CheckOutput()");
		CheckOutput();
	}
    public virtual void Update()
    {
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("GetVariable CheckOutput()");
        CheckOutput();
    }
    public bool isMoving
    {
        get; set;
    }
    public void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }
}
//SetVariable 노드 부모
/// <summary>
/// T는 대입될 데이터형
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SetVariable<T> : NotInputActivatable, Movable, InputParam<T>
{
    protected T value;
    [SerializeField]
    protected ActiveOutputModule active_output;

    public bool isMoving
    {
        get;
        set;
    }
    public void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }
    public void Input(T input, int unused = 0)
    {
        if (input != null)
        {
			if (TutorialManager.instance != null) {
				TutorialManager.instance.NodeGotValue (gameObject.name,typeof(T));
			}
            value = input;
            CheckOutput();
        }
    }
    public void Input(T input)
	{
		Input (input, 0);
	}
    public override void Active()
    {
        base.Active();
        CheckOutput();
    }

    public override void Update()
    {
        base.Update();
        if (isActive >= 1)
        {
            //CheckOutput();
            isActive--;
        }
        value = default(T);
        isActive = 0;
    }
}

//이벤트 노드 부모
public abstract class Event : Node, Movable
{
    public bool isMoving
    {
        get; set;
    }

    public int isActive;
    public ActiveOutputModule active_output;
    private SpriteRenderer myRenderer;
    public void Active()
    {
        isActive = 2;
    }
    public override void Awake()
    {
        base.Awake();
        myRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Update()
    {
        Color temp = myRenderer.color;


        if (isActive >= 1)
            temp.a = 1;
        else
            temp.a = 0.5f;
        myRenderer.color = temp;
    }
    public void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
    }
    //Event노드는 CheckOutput이 필요하지 않습니다.
    public override void CheckOutput()
    { }
}