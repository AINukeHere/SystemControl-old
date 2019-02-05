using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AudioClipParamEvent : UnityEvent<AudioClip,int> { }
public class AudioClipInputModule : MonoBehaviour, InputParam<AudioClip>
{
    public AudioClipParamEvent destination;
    [SerializeField]
    private int input_index;
    public void Input(AudioClip input)
    {
        if (input != null)
        {
            destination.Invoke(input,input_index);
        }
    }
}
