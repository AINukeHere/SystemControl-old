using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntOutputModule : OutputModule<int?>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
