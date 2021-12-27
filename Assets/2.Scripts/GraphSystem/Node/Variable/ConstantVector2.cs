using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVector2 : GetVariable
{
	[SerializeField]
	private Vector2 value = Vector2.zero;
	[SerializeField]
	private bool HasValue = true;
    [SerializeField]
    private Vector2OutputModule vector2OutputModule;
    //소수점 2자리까지만 출력
	public override void CheckOutput()
	{
        vector2OutputModule.Input(value);
	}
    public override void ExpandDisplay()
    {
        textMesh.text = value.ToString();
    }
}