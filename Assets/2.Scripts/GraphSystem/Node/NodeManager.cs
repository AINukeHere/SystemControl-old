using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    
    public void CheckNodeToReoutput()
    {
        variable_nodes = FindObjectsOfType<GetVariable>();
    }
    public void ProcessNodeToReoutput()
    {
        foreach (GetVariable gv in variable_nodes)
            gv.CheckOutput();
    }
    public void Btn_ResetNodes()
    {
        //노드초기화
        Node[] all_nodes = FindObjectsOfType<Node>();
        foreach (Node node in all_nodes)
            node.ResetNode();
        //전체 엣지 갱신
        EdgeManager.instance.UpdateAll();

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
		StringCase[] all_case = FindObjectsOfType<StringCase>();
		for (int i = 0; i < all_nodes.Length; ++i)
		{
			Node node = null;
			StringCase stringCase = null;
			if (i < all_nodes.Length)
				node = all_nodes[i];
			if (i < all_case.Length)
				stringCase = all_case[i];

			if (node != null)
			{
				IExpandableDisplay display = node.GetComponent<IExpandableDisplay>();
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
        Debug.Log(all_nodes.Length + "개의 노드 비활성화");
        foreach (Node node in all_nodes)
            node.gameObject.SetActive(false);
    }
}