using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntParamEvent : UnityEvent<int?,int> { }
public class IntInputModule : MonoBehaviour, IInputParam<int?>
{
    [SerializeField]
    public IntParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(int? input)
    {
        if (input != null)
        {
            destination.Invoke(input,input_index);
        }
    }
}
