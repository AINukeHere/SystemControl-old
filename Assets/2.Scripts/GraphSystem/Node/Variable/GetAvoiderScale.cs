﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAvoiderScale : GetVariable
{
    [SerializeField]
    private Vector2OutputModule vector2_output;
    private Transform avoiderTr;

    public override void Awake()
    {
        base.Awake();
        GameObject avoider = GameObject.FindGameObjectWithTag("Avoider");
        if (avoider != null)
            avoiderTr = avoider.GetComponent<Transform>();
    }

    public override void CheckOutput()
    {
        vector2_output.Input(avoiderTr.localScale);
    }
    public override void ExpandDisplay()
    {
        textMesh.text = ((Vector2)avoiderTr.localScale).ToString();
    }
}
