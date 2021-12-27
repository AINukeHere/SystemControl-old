using UnityEngine;

public class PlayAudioClip : ActivatableNode
{
    private AudioSource audio_source;
    public AudioClip value = null;
    public ActiveOutputModule active_output;

    public void Input(AudioClip input, int unused = 0)
    {
        if (input != null)
        {
            value = input;
            CheckOutput();
        }
    }
    public override void Awake()
    {
        base.Awake();
        audio_source = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();
        value = null;
    }
    public override void CheckOutput()
    {
        if (isActive >= 2 && value != null)
        {
            audio_source.PlayOneShot(value);
            active_output.Active();
            isActive--;
        }
    }
    public override void ExpandDisplay()
    {
        if (textMesh)
            textMesh.text = $"{nodeName}\n{(value != null ? value.name : "값없음")}";
    }
}
