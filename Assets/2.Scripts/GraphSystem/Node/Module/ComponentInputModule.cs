﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ComponentParamEvent : UnityEvent<Component,int> { }
public class ComponentInputModule : ModuleColorize, IInputParam<Component>
{
    [SerializeField]
    public ComponentParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(Component input)
    {
        if (input != null)
        {
            destination.Invoke(input, input_index);
        }
    }
}
