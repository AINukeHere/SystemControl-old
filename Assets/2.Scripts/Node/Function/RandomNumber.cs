
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : NotInputActivatable, Movable, ExpandableDisplay
{
    private TextMesh myTextMesh;

    public BigInt value;
    public BigInt rangeValue;

    public IntOutputModule int_output;
    public ActiveOutputModule active_output;
    public bool isExpanded
    {
        get; set;
    }
    public override void Active()
    {
        base.Active();
        CheckOutput();
    }
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        isExpanded = GameManager.instance.isExpandDisplay;
    }
    public override void Update()
    {
        base.Update();

        if (isActive >= 1)
        {
            //CheckOutput();
            isActive--;
        }

        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();
    }
    //Output을 출력할 준비가 되었으면 처리
    public override void CheckOutput()
    {
        if (isActive >= 2 && rangeValue != null)
        {
            value = BigInt.RandomRange(rangeValue);
            rangeValue = null;

            int_output.Input(value);
            active_output.Active();
            isActive--;
        }
    }

    public void Input(BigInt input, int unused = 0)
    {
        if (input != null)
        {
            rangeValue = input;
            CheckOutput();
        }
    }
    public void NormalDisplay()
    {
        myTextMesh.text = "Random Number";
    }
    public void ExpandDisplay()
    {
        if (value != null)
            myTextMesh.text = "Random Number" + (isExpanded ? "\n" + value.ToString() : "");
        else
            myTextMesh.text = "Random Number";
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
    public override string GetInfoString()
    {
        return "0부터 입력된 값 중 하나를 내보냅니다.";
    }
}
