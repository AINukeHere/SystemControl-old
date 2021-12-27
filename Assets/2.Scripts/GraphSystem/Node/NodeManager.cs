using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ModuleValueType
{
    ACTIVE,
    INT,
    FLOAT,
    STRING,
    VECTOR2,
    VECTOR3,
    BOOL,
    AUDIO_CLIP,
    AUDIO_SOURCE,
    COMPONENT,

    MAX
}
public class NodeManager : MonoBehaviour
{
    public static NodeManager instance { get; private set; }
    private Color[] typeColorInfo;
    private Dictionary<Type, string> dict_nodeType_explainText;
    private Dictionary<Type, string> dict_nodeType_normalDisplayText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
			Destroy(gameObject);
        NodeManager.instance.CheckNodeToReoutput();
        isExpandDisplay = false;
		if(toogleEyeImg != null)
            toogleEyeImg.sprite = eye_closed_img;
		StartCoroutine (UpdateNodeWithEyeState ());

        //모듈 타입별 색상 관리
        typeColorInfo = new Color[(int)ModuleValueType.MAX];
        typeColorInfo[(int)ModuleValueType.ACTIVE] = Color.white;
        typeColorInfo[(int)ModuleValueType.INT] = new Color32(30, 226, 174, 255);
        typeColorInfo[(int)ModuleValueType.FLOAT] = Color.green;
        typeColorInfo[(int)ModuleValueType.STRING] = Color.magenta;
        typeColorInfo[(int)ModuleValueType.VECTOR2] = new Color32(231, 182, 35, 255);
        typeColorInfo[(int)ModuleValueType.VECTOR3] = new Color32(231, 182, 35, 255);
        typeColorInfo[(int)ModuleValueType.BOOL] = Color.red;
        typeColorInfo[(int)ModuleValueType.AUDIO_CLIP] = new Color32(128, 64, 0, 255);
        typeColorInfo[(int)ModuleValueType.AUDIO_SOURCE] = new Color32(0, 168, 242, 255);
        typeColorInfo[(int)ModuleValueType.COMPONENT] = new Color32(0, 168, 242, 255);

        dict_nodeType_explainText = new Dictionary<Type, string>();
        dict_nodeType_normalDisplayText = new Dictionary<Type, string>();
        #region Control
        dict_nodeType_explainText.Add(typeof(Brench), "입력된 참또는 거짓에 따라 참일경우 실행신호를 오른쪽상단모듈로 보내고 거짓일 경우 오른쪽 하단모듈로 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(SwitchOnString), "입력된 문장을 보고 같은 문장이 있으면 해당 문장의 바로 우측으로 실행신호를 보내고 하나도 매칭이 안된다면 맨 상단으로 내보냅니다.");
        #endregion
        #region Event
        dict_nodeType_explainText.Add(typeof(AvoiderCollision), "Avoider와 충돌하고있는 것들의 이름을 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(BeginPlayEvent), "맨 처음 한번만 실행신호를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(KeyEvent), "특정 키가 입력되면 실행신호를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(LateTickEvent), "매 프레임마다 실행신호를 내보냅니다. 하지만 TickEvent보다는 늦게 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(TickEvent), "매 프레임마다 실행신호를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(TouchEvent), "사용자가 클릭을 했을 때 실행신호를 내보냅니다.");
        #endregion
        #region Function
        dict_nodeType_explainText.Add(typeof(AddScore), "현재 Score에 이 노드로 입력된 값만큼 더합니다.");
        dict_nodeType_explainText.Add(typeof(AvoiderReset), "Avoider의 위치, 크기, 속도를 초기화시킵니다.");
        dict_nodeType_explainText.Add(typeof(GetComponent), "입력된 게임오브젝트의 선택된 컴포넌트를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(IsPlayingAudioSource), "입력된 음원이 현재 실행중인지를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(LoadScene), "입력된 씬이름의 씬을 로드합니다.");
        dict_nodeType_explainText.Add(typeof(PlayAudioClip), "입력된 음원을 실행합니다.");
        dict_nodeType_explainText.Add(typeof(SetActiveWall), "입력된 두 점을 잇는 벽을 입력된 참또는 거짓에 따라 생성하거나 제거합니다.");
        dict_nodeType_explainText.Add(typeof(StartStage), "입력된 값의 스테이지를 시작합니다.");
        dict_nodeType_explainText.Add(typeof(SubtractScore), "입력된 값만큼 점수를 낮춥니다.");
        #endregion
        #region Operator
        dict_nodeType_explainText.Add(typeof(Absolute_Float), "입력된 실수값의 절대값을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Absolute_Float), "│Float│");
        dict_nodeType_explainText.Add(typeof(Add_Float), "입력된 두 실수값을 더한 결과를 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Add_Float), "＋");
        dict_nodeType_explainText.Add(typeof(Add_Int), "입력된 두 정수값을 더한 결과를 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Add_Int), "＋");
        dict_nodeType_explainText.Add(typeof(BreakVector2), "입력된 벡터2를 스칼라값(실수) 둘로 쪼개서 각각 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(BreakVector3), "입력된 벡터3를 스칼라값(실수) 셋으로 쪼개서 각각 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(Equal_Float), "입력된 두 실수가 같은지 판별하여 참 또는 거짓을 내보냅니다. ※주의! 컴퓨터가 실수를 저장하는 방식의 특성상 이 노드는 정상작동하기가 쉽지 않습니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Equal_Float), "＝＝");
        dict_nodeType_explainText.Add(typeof(Equal_String), "입력된 두 문장이 같은지 판별하여 참 또는 거짓을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Equal_String), "＝＝");
        dict_nodeType_explainText.Add(typeof(Greater_Float), "입력된 첫번째 실수값이 두번째 실수값보다 큰지 비교하여 참 또는 거짓을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Greater_Float), "＞");
        dict_nodeType_explainText.Add(typeof(Greater_Int), "입력된 첫번째 정수값이 두번째 실수값보다 큰지 비교하여 참 또는 거짓을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Greater_Int), "＞");
        dict_nodeType_explainText.Add(typeof(Int2Float), "입력된 정수값을 실수값으로 변환하여 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(Less_Float), "입력된 첫번째 실수값이 두번째 실수값보다 작은지 비교하여 참 또는 거짓을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Less_Float), "＜");
        dict_nodeType_explainText.Add(typeof(Less_Int), "입력된 첫번째 정수값이 두번째 실수값보다 작은지 비교하여 참 또는 거짓을 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Less_Int), "＜");
        dict_nodeType_explainText.Add(typeof(MakeVector2), "입력된 두 실수값으로 벡터2를 만들어 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(MakeVector3), "입력된 세 실수값으로 벡터3를 만들어 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(Multiply_Float), "입력된 두 실수를 곱한 결과를 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Multiply_Float), "×");
        dict_nodeType_explainText.Add(typeof(Subtract_Float), "입력된 첫번째 실수에서 두번째 실수를 뺀 결과를 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Subtract_Float), "－");
        dict_nodeType_explainText.Add(typeof(Subtract_Int), "입력된 첫번째 정수에서 두번째 정수를 뺀 결과를 내보냅니다.");
        dict_nodeType_normalDisplayText.Add(typeof(Subtract_Int), "－");
        #endregion
        #region Variable
        dict_nodeType_explainText.Add(typeof(ConstantAudioClip), "음원입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(ConstantBool), "참 또는 거짓을 나타내는 부울값입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(ConstantFloat), "실수값입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(ConstantInt), "정수값입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(ConstantString), "문장입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(ConstantVector2), "Vector2입니다. 상수이므로 절대 변경되지 않습니다.");
        dict_nodeType_explainText.Add(typeof(GetAvoiderScale), "Avoider의 크기를 벡터2로 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetAvoiderVelocity), "Avoider의 현재 속도(방향이 있는 값)를 벡터2로 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetAvoiderX), "Avoider의 x좌표를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetAvoiderY), "Avoider의 y좌표를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetCurrentStageNum), "현재 스테이지 값을 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetDeltaTime), "이전프레임에서 현재프레임사이에 흐른 시간을 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetGameCameraPos), "게임을 비추고 있는 카메라의 좌표를 벡터3로 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetHorizontalInput), "플레이어의 좌/우 키에 대한 입력값을 내보냅니다. (-1또는 0 또는 1)");
        dict_nodeType_explainText.Add(typeof(GetNodeCameraX), "노드를 비추고있는 카메라의 X좌표를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetNodeCameraY), "노드를 비추고있는 카메라의 Y좌표를 내보냅니다.");
        dict_nodeType_explainText.Add(typeof(GetVerticalInput), "플레이어의 상/하 키에 대한 입력값을 내보냅니다. (-1또는 0 또는 1)");
        dict_nodeType_explainText.Add(typeof(SetAvoiderGravityScale), "입력된 실수로 Avoider의 중력크기를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetAvoiderScale), "입력된 벡터2로 Avoider의 크기값을 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetAvoiderVelocity), "입력된 벡터2로 Avoider의 속도(방향이 있는 값)를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetAvoiderX), "입력된 실수값으로 Avoider의 x값 좌표를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetAvoiderY), "입력된 실수값으로 Avoider의 y값 좌표를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetGameCameraPos), "입력된 벡터3로 게임을 비추고 있는 카메라의 위치를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetNodeCameraX), "입력된 실수값으로 노드를 비추고 있는 카메라의 x좌표를 새로 설정합니다.");
        dict_nodeType_explainText.Add(typeof(SetNodeCameraY), "입력된 실수값으로 노드를 비추고 있는 카메라의 y좌표를 새로 설정합니다.");
        #endregion
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
        //전체 엣지 삭제
        EdgeManager.instance.RemoveAllEdge();

        //노드초기화
        Node[] all_nodes = FindObjectsOfType<Node>();
        foreach (Node node in all_nodes)
            node.ResetNode();
        //모든 OutputModule에서 CreateDefaultEdges 호출
        List<Component> modules = new List<Component>();
        modules.AddRange(FindObjectsOfType<ActiveOutputModule>());
        modules.AddRange(FindObjectsOfType<AudioClipOutputModule>());
        modules.AddRange(FindObjectsOfType<BoolOutputModule>());
        modules.AddRange(FindObjectsOfType<FloatOutputModule>());
        modules.AddRange(FindObjectsOfType<IntOutputModule>());
        modules.AddRange(FindObjectsOfType<StringOutputModule>());
        modules.AddRange(FindObjectsOfType<Vector2OutputModule>());
        modules.AddRange(FindObjectsOfType<Vector3OutputModule>());
        foreach (var module in modules)
            module.SendMessage("CreateDefaultEdges");

        //EdgeManager.instance.UpdateAll();

        if (TutorialManager.instance != null)
			TutorialManager.instance.ConditionSatisfied (TutorialManager.ConditionType.RESET_NODE);
    }
	public Image toogleEyeImg;
    public bool isExpandDisplay { get; private set; }
    public Sprite eye_closed_img;
	public Sprite eye_opened_img;
	public void Btn_ToogleEye()
	{
		isExpandDisplay = !isExpandDisplay;

		//이미지 교체
        if (toogleEyeImg != null)
		    toogleEyeImg.sprite = isExpandDisplay ? eye_opened_img : eye_closed_img;

		//확장형 디스플레이에서 확장디스플레이 호출
		CallDisplay();
	}
	void CallDisplay()
	{
		Node[] all_nodes = FindObjectsOfType<Node>();
		StringCase[] all_case = FindObjectsOfType<StringCase>();
        int max_count = all_nodes.Length < all_case.Length ? all_case.Length : all_nodes.Length;

        for (int i = 0; i < max_count; ++i)
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

    public Color getTypeColor(ModuleValueType valueType)
    {
        return typeColorInfo[(int)valueType];
    }
    public string getExplainText(Type nodeClassType)
    {
        return dict_nodeType_explainText[nodeClassType];
    }
    public string getNormalDisplayText(Type nodeClassType)
    {
        if (dict_nodeType_normalDisplayText.ContainsKey(nodeClassType))
            return dict_nodeType_normalDisplayText[nodeClassType];
        Debug.LogWarning($"getNormalDisplayText returned just class name : {nodeClassType.ToString()}");
        return nodeClassType.ToString();
    }
}