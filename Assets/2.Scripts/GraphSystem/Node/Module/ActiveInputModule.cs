using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActiveParamEvent : UnityEvent<ActivateType?, int> { }
public class ActiveInputModule : MonoBehaviour, IInputParam<ActivateType?>
{
    [SerializeField]
    public ActiveParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(ActivateType? input)
    {
        if (input != null)
        {
            destination.Invoke(input, input_index);
        }
    }
}

///구버전 코드
//public class ActiveInputModule : Activatable {
//    public UnityEvent destination;
//    public override void Active()
//    {
//        base.Active();
//        destination.Invoke();
//    }
//}
