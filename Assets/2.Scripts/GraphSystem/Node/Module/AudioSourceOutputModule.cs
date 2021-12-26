using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceOutputModule : OutputModule<AudioClip>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
