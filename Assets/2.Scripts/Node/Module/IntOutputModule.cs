using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntOutputModule : OutputModule<BigInt>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}
