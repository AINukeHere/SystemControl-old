using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectParamEvent : UnityEvent<GameObject,int> { }
public class GameObjectInputModule : ModuleColorize, IInputParam<GameObject>
{
    [SerializeField]
    public GameObjectParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(GameObject input)
    {
        if (input != null)
        {
            destination.Invoke(input, input_index);
        }
    }
}
