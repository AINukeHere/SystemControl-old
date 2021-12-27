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
    [SerializeField]
    private AudioClipOutputModule audioClipOutputModule;
    //소수점 2자리까지만 출력
    public override void CheckOutput()
    {
        audioClipOutputModule.Input(value);
    }

    public override void ExpandDisplay()
    {
        textMesh.text = value.name;
    }
}