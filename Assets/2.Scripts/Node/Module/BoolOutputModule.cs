﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolOutputModule : OutputModule<bool?>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}