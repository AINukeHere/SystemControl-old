using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vector3ParamEvent : UnityEvent<Vector3?,int> { }
public class Vector3InputModule : ModuleColorize, IInputParam<Vector3?>
{
    [SerializeField]
    public Vector3ParamEvent destination;

    [SerializeField]
    private int input_index;
    public void Input(Vector3? input)
    {
        if (input.HasValue)
        {
            destination.Invoke(input, input_index);
        }
    }
}
