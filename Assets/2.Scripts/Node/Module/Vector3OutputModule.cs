﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3OutputModule : OutputModule<Vector3?>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
