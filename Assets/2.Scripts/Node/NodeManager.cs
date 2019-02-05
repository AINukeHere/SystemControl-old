using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    //Single Ton
    private static NodeManager _instance;
    public static NodeManager instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
			Destroy(gameObject);
		_isExpandDisplay = false;
		toogleEyeImg.sprite = eye_closed_img;
		StartCoroutine (UpdateNodeWithEyeState ());
    }

    //상수및 변수노드는 한 노드에서 한프레임에 여러번 출력할때 같이 재출력해야함.
    //ArrowInput은 스택오버플로우를 막기위해 1프레임에 1회만 데이터를 전송할 수 있게 만들었지만
    //위와같은 상황에는 다시 전송 할 수 있는 상태로 변경해줘야함.
    private GetVariable[] variable_nodes;
    private ArrowInput[] arrow_inputs;
    
    public void CheckNodeToReoutput()
    {
        variable_nodes = FindObjectsOfType<GetVariable>();
        arrow_inputs = FindObjectsOfType<ArrowInput>();
    }
    public void ProcessNodeToReoutput()
    {
        foreach (ArrowInput ai in arrow_inputs)
            ai.ActiveSendableState();
        foreach (GetVariable gv in variable_nodes)
            gv.CheckOutput();
    }
    public void Btn_ResetNodes()
    {
        //노드초기화
        Node[] all_nodes = FindObjectsOfType<Node>();
        foreach (Node node in all_nodes)
            node.ResetNode();

        //엣지중심초기화
        //input과 output만 정해주면 자동으로 위치 찾아가므로 생략함
        //ArrowConnecting[] all_arrowConnecting = FindObjectsOfType<ArrowConnecting>();
        //foreach (ArrowConnecting arrowConnecting in all_arrowConnecting)
        //    arrowConnecting.ResetNode();

        ArrowInput[] all_arrowInput = FindObjectsOfType<ArrowInput>();
        foreach (ArrowInput arrowInput in all_arrowInput)
            arrowInput.ResetNode();

        ArrowOutput[] all_arrowOuput = FindObjectsOfType<ArrowOutput>();
        foreach (ArrowOutput arrowOutput in all_arrowOuput)
            arrowOutput.ResetNode();
		if(TutorialManager.instance != null)
			TutorialManager.instance.ConditionSatisfied (TutorialManager.ConditionType.RESET_NODE);
    }
	public Image toogleEyeImg;
	private bool _isExpandDisplay;
	public bool isExpandDisplay
	{
		get
		{
			return _isExpandDisplay;
		}
	}
	public Sprite eye_closed_img;
	public Sprite eye_opened_img;
	public void Btn_ToogleEye()
	{
		_isExpandDisplay = !_isExpandDisplay;

		//이미지 교체
		toogleEyeImg.sprite = isExpandDisplay ? eye_opened_img : eye_closed_img;

		//확장형 디스플레이에서 확장디스플레이 호출
		CallDisplay();
	}
	void CallDisplay()
	{
		Node[] all_nodes = FindObjectsOfType<Node>();
		ArrowConnecting[] all_arrowConnecting = FindObjectsOfType<ArrowConnecting>();
		StringCase[] all_case = FindObjectsOfType<StringCase>();
		int maxCount = Mathf.Max(all_nodes.Length, all_arrowConnecting.Length,all_case.Length);
		for (int i = 0; i < maxCount; ++i)
		{
			Node node = null;
			ArrowConnecting arrowConnecting = null;
			StringCase stringCase = null;
			if (i < all_nodes.Length)
				node = all_nodes[i];
			if (i < all_arrowConnecting.Length)
				arrowConnecting = all_arrowConnecting[i];
			if (i < all_case.Length)
				stringCase = all_case[i];

			if (node != null)
			{
				ExpandableDisplay display = node.GetComponent<ExpandableDisplay>();
				if (display != null)
				{
					if (isExpandDisplay)
					{
						display.isExpanded = true;
						display.ExpandDisplay();
					}
					else
					{
						display.isExpanded = false;
						display.NormalDisplay();
					}
				}
			}
			if (arrowConnecting != null)
			{
				if (isExpandDisplay)
				{
					arrowConnecting.isExpanded = true;
					arrowConnecting.ExpandDisplay();
				}
				else
				{
					arrowConnecting.isExpanded = false;
					arrowConnecting.NormalDisplay();
				}
			}
			if (stringCase != null)
			{
				if (isExpandDisplay)
				{
					stringCase.isExpanded = true;
					stringCase.ExpandDisplay();
				}
				else
				{
					stringCase.isExpanded = false;
					stringCase.NormalDisplay();
				}
			}
		}
	}
	IEnumerator UpdateNodeWithEyeState()
	{
		while (true) {
			yield return new WaitForSeconds (1.0f);
			CallDisplay ();
		}
	}
    private void ShutDownAllNode()
    {
        Node[] all_nodes = FindObjectsOfType<Node>();
        ArrowConnecting[] all_arrowConnecting = FindObjectsOfType<ArrowConnecting>();
        Debug.Log(all_nodes.Length + "개의 노드 비활성화");
        Debug.Log(all_arrowConnecting.Length + "개의 연결선 비활성화");
        foreach (Node node in all_nodes)
            node.gameObject.SetActive(false);
        foreach (ArrowConnecting connect in all_arrowConnecting)
            connect.transform.parent.gameObject.SetActive(false);
    }
}