﻿using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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
//마우스로 끌어서 움직일 수 있는 인터페이스입니다.
public interface IMovable
{
    bool isMoving
    {
        get; set;
    }
    void Move(Vector2 pos, bool bGroup = false);
    void MoveEnd(Vector2 pos, bool bGroup = false);
}
//NewEdge가 이것에 연결됩니다. 모든 InputModule이 상속받습니다.
public interface IInputParam<T>
{
    void Input(T input);
}
//눈 아이콘을 눌러서 세부적인 정보를 출력할 수 있는 인퍼에시으입니다.
public interface IExpandableDisplay
{
    bool isExpanded
    {
        get; set;
    }
    void NormalDisplay();
    void ExpandDisplay();
}
//모든 노드가 상속받는 클래스입니다.
public abstract class Node : MonoBehaviour, IResetable, IHasInfo, IMovable
{
    public Vector3 origin_pos{get; set;}
    public Quaternion origin_rot{get; set;}
    //input, output module의 transform for update to connected Edge
    protected List<Transform> allInputModules;
    public virtual void Awake()
    {
        origin_pos = transform.position;
        origin_rot = transform.rotation;
        allInputModules = new List<Transform>();
        CheckAllModules();
    }
    public virtual void CheckAllModules()
    {
        var trs = GetComponentsInChildren<Transform>();
        foreach(var tr in trs)
        {
            object obj;

            //InputMoudles
            obj = tr.GetComponent<IInputParam<ActivateType?>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<AudioClip>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<bool?>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<float?>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<int?>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<string>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<Vector2?>>();
            if (obj != null)
                allInputModules.Add(tr);
            obj = tr.GetComponent<IInputParam<Vector3?>>();
        }
    }
    //노드를 리셋합니다. 폴더안에 있을 땐 무시됩니다.
    public virtual void ResetNode()
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

    //Movable 인터페이스 구현
    public bool isMoving { get; set; }
    public virtual void Move(Vector2 pos, bool bGroup = false)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for (i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if (i == colls.Length)
            transform.position = pos;
        EdgeManager.instance.onNodeMoving(allInputModules, transform);
    }
    public virtual void MoveEnd(Vector2 pos, bool bGroup = false) { }
}
//활성화가능한(실행 가능한) 노드클래스 = 함수노드, 제어노드, SetVariable
public abstract class ActivatableNode : Node
{
    public int isActive;

    protected SpriteRenderer myRenderer;
    public virtual void Active()
    {
        isActive = 2;
        if (TutorialManager.instance != null)
        {
            TutorialManager.instance.NodeActivated(gameObject.name);
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
//ArrowOutput이 Active시키지 않고 노드가 아닌 활성화클래스 (SwitchCase, Folder)
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
//모든 연산자 노드 부모
/// <summary>
/// T : 인풋타입 R : 결과타입
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="R"></typeparam>
public abstract class Operator<T, R> : Node, IExpandableDisplay
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
            input[i] = default;
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
            for(int i =0; i < result.Length; ++i)
                result[i] = default;
            CheckOutput();
        }
    }


    public virtual void NormalDisplay()
    {
        if (textMesh != null)
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
                {
                    if (r != null)
                        textMesh.text += ("\n" + r.ToString());
                    else
                        textMesh.text += "\n값없음";
                }
            }
        }
    }
    public bool isExpanded { get; set; }
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
public abstract class OutputModule<T> : MonoBehaviour, IInputParam<T>, IMovable
{
    private GameObject edgePrefab;
    [SerializeField] private List<NewEdge> edges;
    private NewEdge currentEdge;
    [SerializeField]
    private static float autoAssignRadius = 0.5f;
    [SerializeField] protected List<IInputParam<T>> connectedInputModules = new List<IInputParam<T>>();
    [SerializeField] private Transform[] defaultEdge;
    public Color nodeColor;
    //protected bool isCheckOutput = false;
    protected T input;
    protected virtual void Awake()
    {
        edgePrefab = Resources.Load<GameObject>("NewEdge");
        nodeColor = GetComponentInParent<SpriteRenderer>().color;
        foreach(Transform targetTr in defaultEdge)
            CreateEdge(targetTr);
    }
    public virtual void Input(T input)
    {
        if (input != null)
        {
            this.input = input;
            CheckOutput();
        }
    }
    public void CheckOutput()
    {
        foreach (IInputParam<T> connectedInputModule in connectedInputModules)
        {
            connectedInputModule.Input(input);
        }
        AfterInputCallBack();
    }
    public bool isMoving { get; set; }
    public virtual void MoveBegin()
    {
        currentEdge = Instantiate(edgePrefab).GetComponent<NewEdge>();
        edges.Add(currentEdge);
        currentEdge.SetStartTarget(this);
        currentEdge.SetEndTarget(GameObject.Find("Controller").transform);

        //Ordering
        SpriteRenderer edgePointRenderer = GetComponentInChildren<SpriteRenderer>();
        string sortingLayerName = edgePointRenderer.sortingLayerName;
        int sortingOrder = edgePointRenderer.sortingOrder;
        currentEdge.SetOrdering(sortingLayerName, sortingOrder);
    }
    public virtual void Move(Vector2 pos, bool bGroup = false)
    {
        if (bGroup)
            return;
        if (!isMoving)
        {
            isMoving = true;
            MoveBegin();
        }
        if (currentEdge != null)
            currentEdge.LineRendererUpdate();
    }
    public virtual void MoveEnd(Vector2 pos, bool bGroup=false)
    {
        if (bGroup)
            return;
        bool bFoundInputModule = false;  //colls에서 InputNode를 찾았는지 체크
        Collider2D[] colls;
        colls = Physics2D.OverlapCircleAll(pos, autoAssignRadius);
        if (colls.Length > 0)
        {
            int min_dist_idx = -1;
            for (int i = 0; i < colls.Length; ++i)
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
                IInputParam<T> nearestInputModule = colls[min_dist_idx].GetComponent<IInputParam<T>>();
                if (nearestInputModule != null)
                {
                    bool bExist = false;
                    foreach (IInputParam<T> target in connectedInputModules)
                    {
                        if (target.Equals(nearestInputModule))
                        {
                            bExist = true;
                            break;
                        }
                    }
                    if (!bExist)
                    {
                        if (EdgeManager.instance.registerEdge(currentEdge, colls[min_dist_idx].transform, transform))
                        {
                            connectedInputModules.Add(nearestInputModule);
                            currentEdge.SetEndTarget(colls[min_dist_idx].transform);
                            Debug.Log(colls[min_dist_idx].gameObject.name + " connected");
                            bFoundInputModule = true;
                        }
                        else
                            Debug.LogWarning("Edge Register Failed : " + name + " -> " + colls[min_dist_idx].gameObject.name);
                    }
                }
            }
        }
        if (!bFoundInputModule)
        {
            edges.Remove(currentEdge);
            Destroy(currentEdge.gameObject);
            currentEdge = null;
        }

        isMoving = false;
    }
    public virtual void CreateEdge(Transform targetTr)
    {
        currentEdge = Instantiate(edgePrefab).GetComponent<NewEdge>();
        if (EdgeManager.instance.registerEdge(currentEdge, targetTr, transform))
        {
            IInputParam<T> targetInputModule = targetTr.GetComponent<IInputParam<T>>();
            edges.Add(currentEdge);
            connectedInputModules.Add(targetInputModule);
            currentEdge.SetStartTarget(this);
            currentEdge.SetEndTarget(targetTr);

            //Ordering
            SpriteRenderer edgePointRenderer = GetComponentInChildren<SpriteRenderer>();
            string sortingLayerName = edgePointRenderer.sortingLayerName;
            int sortingOrder = edgePointRenderer.sortingOrder;
            currentEdge.SetOrdering(sortingLayerName, sortingOrder);
        }
        else
        {
            Debug.LogWarning("Edge Register Failed : " + name + " -> " + targetTr.gameObject.name);
            Destroy(currentEdge.gameObject);
        }
    }
    public virtual void RemoveEdge(Transform edgeTr)
    {
        IInputParam<T> inputParamT = edgeTr.GetComponent<IInputParam<T>>();
        for (int i=0; i < connectedInputModules.Count; ++i)
        {
            if (connectedInputModules[i].Equals(inputParamT))
            {
                connectedInputModules.RemoveAt(i);
                break;
            }
        }
    }
    public abstract void AfterInputCallBack();
}

//변수Get 노드 부모
public abstract class GetVariable : Node
{
    private bool bCheckOutput = false;
    public override void Awake()
    {
        base.Awake();
    }
    public virtual void Start()
    {
#if UNITY_EDITOR
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("GetVariable CheckOutput()");
#endif
        CheckOutput();
    }
    public virtual void Update()
    {
#if UNITY_EDITOR
        if (gameObject.name.EndsWith("(Test)"))
            Debug.Log("GetVariable CheckOutput()");
#endif
        CheckOutput();
    }
}
//SetVariable 노드 부모
/// <summary>
/// T는 대입될 데이터형
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SetVariable<T> : ActivatableNode, IInputParam<T>
{
    protected T value;
    [SerializeField]
    protected ActiveOutputModule active_output;
    
    public void Input(T input, int unused = 0)
    {
        if (input != null)
        {
            if (TutorialManager.instance != null)
            {
                TutorialManager.instance.NodeGotValue(gameObject.name, typeof(T));
            }
            value = input;
            CheckOutput();
        }
    }
    public void Input(T input)
    {
        Input(input, 0);
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
public abstract class Event : Node
{
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
    //Event노드는 CheckOutput이 필요하지 않습니다.
    public override void CheckOutput()
    { }
}
