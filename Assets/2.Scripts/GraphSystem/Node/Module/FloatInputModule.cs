using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatParamEvent : UnityEvent<float?,int> { }
public class FloatInputModule : ModuleColorize, IInputParam<float?>
{
    [SerializeField]
    public FloatParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(float? input)
    {
        if (input.HasValue)
        {
            destination.Invoke(input, input_index);
        }
    }
}
