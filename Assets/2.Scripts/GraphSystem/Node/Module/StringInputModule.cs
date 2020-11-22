﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringParamEvent : UnityEvent<string,int> { }
public class StringInputModule : MonoBehaviour, IInputParam<string>
{
    [SerializeField]
    public StringParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(string input)
    {
        if (input != null)
        {
            destination.Invoke(input, input_index);
        }
    }
}
