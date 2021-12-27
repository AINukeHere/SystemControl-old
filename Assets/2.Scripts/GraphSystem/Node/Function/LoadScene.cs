using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : ActivatableNode
{
    public ActiveOutputModule active_output;
    //values
    string value = null;

    public void Input(string input, int index = 0)
    {
        if (input != null)
        {
            value = input;
            CheckOutput();
        }
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
            SceneManager.LoadScene(value);
            active_output.Active();
            isActive--;
        }
    }
    public override void ExpandDisplay()
    {
        if (textMesh != null)
        {
            textMesh.text = $"{nodeName}\n{value ?? "값없음"}";
        }
    }
}
