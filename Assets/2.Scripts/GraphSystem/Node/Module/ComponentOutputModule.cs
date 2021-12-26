using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentOutputModule : OutputModule<Component>
{
    public override void AfterInputCallBack()
    {
        input = null;
    }
}