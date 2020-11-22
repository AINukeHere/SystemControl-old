using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAudioClip : GetVariable
{
    [SerializeField]
    private AudioClip value = null;
    [SerializeField]
    private bool HasValue = true;

    private TextMesh myTextMesh;
    [SerializeField]
    private AudioClipOutputModule audioClipOutputModule;
    //소수점 2자리까지만 출력
    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
        if (myTextMesh != null)
        {
            if (HasValue)
                myTextMesh.text = value.name;
            else
                myTextMesh.text = "NULL";
        }
    }
    public override void CheckOutput()
    {
        audioClipOutputModule.Input(value);
    }
    public override string GetInfoString()
    {
        return "음원입니다. 상수이므로 절대 변경되지 않습니다.";
    }
}