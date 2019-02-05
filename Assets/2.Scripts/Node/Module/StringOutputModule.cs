using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringOutputModule : OutputModule<string>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
