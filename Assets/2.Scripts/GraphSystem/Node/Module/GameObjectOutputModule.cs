using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectOutputModule : OutputModule<GameObject> {
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
