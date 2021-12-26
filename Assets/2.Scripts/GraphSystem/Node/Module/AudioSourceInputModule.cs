using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AudioSourceParamEvent : UnityEvent<AudioSource, int> { }
public class AudioSourceInputModule : ModuleColorize, IInputParam<AudioSource>
{
    public AudioSourceParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(AudioSource input)
    {
        if (input != null)
        {
            destination.Invoke(input,input_index);
        }
    }
}
