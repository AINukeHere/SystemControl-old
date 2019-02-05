using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchOnString : NotInputActivatable, Movable
{ 
    private TextMesh myTextMesh;

    [SerializeField]
    private GameObject StringCasePrefab;

    public string input_value;

    public List<StringCase> cases = new List<StringCase>();
    public ActiveOutputModule default_output;

    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
		for(int i =0; i < cases.Count; ++i)
		{
			cases [i].SetSwitchHead (this);
		}
    }

    public bool testBollean = false;
    public bool testBollean2 = false;
    public override void Update()
    {
		base.Update();
		input_value = null;
        //CheckOutput();
        if (isActive >= 1)
        {
            isActive--;
        }

        if (testBollean)
        {
            AddCase();
            testBollean = false;
        }
        if (testBollean2)
        {
            DeleteCase();
            testBollean2 = false;
        }

    }
    public override void Active()
    {
        base.Active();
        CheckOutput();
        for (int i = 0; i < cases.Count; ++i)
            cases[i].Active();
    }
    public void Input(string input, int unused = 0)
    {
        if (input != null)
        {
            input_value = input;
            CheckOutput();
        }
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && input_value != null)
        {
            bool isCaseNull = false;
            for (int i = 0; i < cases.Count; ++i)
            {
                if (cases[i].case_value == null)
                {
                    isCaseNull = true;
                    break;
                }
            }
            if(!isCaseNull)
            {
                //case 에의해 activeinputmoudle에 전달
                int i;
                for (i = 0; i < cases.Count; ++i)
                {
                    if (cases[i].case_value == input_value)
                    {
                        cases[i].active_output.Active();
                        break;
                    }
                }
                if (i == cases.Count)
                    default_output.Active();
            }
        }
    }
    public void AddCase()
    {
        GameObject _case = Instantiate(StringCasePrefab, 
            gameObject.transform.position + new Vector3(0,-3.2f - 2.4f * cases.Count, 0),
            Quaternion.identity,
            gameObject.transform);
		_case.GetComponent<StringCase> ().SetSwitchHead (this);
        cases.Add(_case.GetComponent<StringCase>());
    }
    public void DeleteCase()
    {
        if(cases.Count > 0)
        {
            Destroy(cases[cases.Count - 1].gameObject);
            cases.RemoveAt(cases.Count - 1);
        }
    }
    public void Move(Vector2 pos)
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll((Vector3)pos - transform.lossyScale, (Vector3)pos + transform.lossyScale);
        int i;
        for(i = 0; i < colls.Length; ++i)
            if (colls[i].CompareTag("LockField"))
                break;
        if(i == colls.Length)
            transform.position = pos;
    }
    public bool isMoving
    {
        get; set;
    }
    public override string GetInfoString()
    {
        return "입력된 문장을 보고 같은 문장이 있으면 해당 문장의 바로 우측으로 실행신호를 보내고 하나도 매칭이 안된다면 맨 상단으로 내보냅니다.";
    }
}
