using UnityEngine;

public class IsPlayingAudioSource : MethodNode
{
    int isActive;
    public AudioSource value = null;
    private TextMesh myTextMesh;
    private string nodeName = "IsPlaying\nAudioSource";

    public override void Awake()
    {
        base.Awake();
        myTextMesh = GetComponentInChildren<TextMesh>();
    }
    public override void Update()
    {
        base.Update();
        if (isActive >= 1)
            isActive--;
        value = null;
    }


    public BoolOutputModule bool_output;

    public override void NormalDisplay() {
        if(myTextMesh)
            myTextMesh.text = nodeName;
    }
    public override void ExpandDisplay()
    {
        if (myTextMesh)
            myTextMesh.text = value != null ? $"{nodeName}\n{value.name}": nodeName;
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
    public override string GetInfoString()
    {
        return "입력된 음원이 현재 실행중인지를 반환합니다.";
    }

    public override bool CheckRuningState()
    {
        return isActive > 0;
    }
}
