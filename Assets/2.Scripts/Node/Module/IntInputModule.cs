using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BigIntParamEvent : UnityEvent<BigInt,int> { }
public class IntInputModule : MonoBehaviour, InputParam<BigInt>
{
    [SerializeField]
    public BigIntParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(BigInt input)
    {
        if (input != null)
        {
            destination.Invoke(input,input_index);
        }
    }
}
