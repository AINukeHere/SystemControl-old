﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOutputModule : OutputModule<float?> {
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
