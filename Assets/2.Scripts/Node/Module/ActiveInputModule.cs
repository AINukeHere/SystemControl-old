using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveInputModule : Activatable {
    public UnityEvent destination;
    public override void Active()
    {
        base.Active();
        destination.Invoke();
    }
}
