using UnityEngine;

public class PlayAudioClip : ActivatableNode,IExpandableDisplay
{
    private TextMesh myTextMesh;
    private AudioSource audio_source;

    public AudioClip value = null;

    public ActiveOutputModule active_output;

    public bool isExpanded { get; set; }
    public void NormalDisplay() {
        if(myTextMesh)
            myTextMesh.text = "PlayAudioClip";
    }
    public void ExpandDisplay()
    {
        if (myTextMesh)
            myTextMesh.text = value != null ? "PlayAudioClip\n" + value.name : "PlayAudioClip\n";
    }

    public void Input(AudioClip input, int unused = 0)
    {
        if (input != null)
        {
            value = input;
            CheckOutput();
        }
    }

    public override void Active()
    {
        base.Active();
        CheckOutput();
    }
    public override void Awake()
    {
        base.Awake();
        audio_source = GetComponent<AudioSource>();
        myTextMesh = GetComponentInChildren<TextMesh>();
    }

    public override void Update()
    {
        base.Update();

        if (isExpanded)
            ExpandDisplay();
        else
            NormalDisplay();

        if (isActive >= 1)
        {
            //CheckOutput();
            isActive--;
        }
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
    public override string GetInfoString()
    {
        return "입력된 음원을 실행합니다.";
    }
}
