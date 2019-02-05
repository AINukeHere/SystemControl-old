using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2OutputModule : OutputModule<Vector2?>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
