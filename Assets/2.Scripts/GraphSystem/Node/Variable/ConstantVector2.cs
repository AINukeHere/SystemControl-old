using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVector2 : GetVariable
{
	[SerializeField]
	private Vector2 value = Vector2.zero;
	[SerializeField]
	private bool HasValue = true;

	private TextMesh myTextMesh;
    [SerializeField]
    private Vector2OutputModule vector2OutputModule;
    //소수점 2자리까지만 출력
    public override void Awake()
	{
		base.Awake();
		myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value.ToString();
            else
                myTextMesh.text = "NULL";
        }
	}
	public override void CheckOutput()
	{
        vector2OutputModule.Input(value);
	}
	public override string GetInfoString()
	{
		return "Vector2입니다. 상수이므로 절대 변경되지 않습니다.";
	}
}