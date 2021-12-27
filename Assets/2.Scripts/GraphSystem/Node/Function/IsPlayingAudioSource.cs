using UnityEngine;

public class IsPlayingAudioSource : MethodNode
{
    int isActive;
    public AudioSource value = null;
    public BoolOutputModule bool_output;

    public override void Update()
    {
        base.Update();
        value = null;
    }

    public void Input(AudioSource input, int unused = 0)
    {
        if (input != null)
        {
            value = input;
            CheckOutput();
        }
    }
    public override void CheckOutput()
    {
        if (value != null)
        {
            bool_output.Input(value.isPlaying);
            bool_output.CheckOutput();
        }
    }

    public override bool CheckRuningState()
    {
        return isActive > 0;
    }

    public override void ExpandDisplay()
    {
        if (textMesh)
            textMesh.text = value != null ? $"{nodeName}\n{value.name}" : nodeName;
    }
}
