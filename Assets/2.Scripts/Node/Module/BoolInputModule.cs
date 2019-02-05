using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolParamEvent : UnityEvent<bool?,int> { }
public class BoolInputModule : MonoBehaviour, InputParam<bool?>
{
    [SerializeField]
    public BoolParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(bool? input)
    {
        if (input.HasValue)
        {
            destination.Invoke(input, input_index);
        }
    }
}
