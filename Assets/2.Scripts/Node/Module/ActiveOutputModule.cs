using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOutputModule : MonoBehaviour {

    public void Active()
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll(transform.position - transform.lossyScale, transform.position + transform.lossyScale);
        foreach (Collider2D coll in colls)
        {
            ArrowInput arrow_input = coll.GetComponent<ArrowInput>();
            if (arrow_input != null)
            {
                arrow_input.Active();
                //break;
            }
        }
    }
}
