
using UnityEditor;

using UnityEngine;
public class FixArrowPosition : Editor
{
    [MenuItem("Tool/FixArrowPosition")]
    static void Init()
    {
        ArrowConnecting[] arrows = FindObjectsOfType<ArrowConnecting>();
        foreach (ArrowConnecting arrow in arrows)
            arrow.FixParentPosition();
    }
}
public class DotNamingWithObjectName : Editor
{
    [MenuItem("Tool/DotRename")]
    static void Init()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Dot");
        foreach (GameObject obj in objects)
        {
            char[] num = new char[obj.name.Length - 3];
            obj.name.CopyTo(3, num, 0, obj.name.Length - 3);
            obj.GetComponentInChildren<TextMesh>().text = new string(num);
        }
    }
}
public class UI_Rendering_Order_Sort : Editor
{
    [MenuItem("Tool/UI_Sort")]
    static void Init()
    {
        UI_Render_Order_Setting[] uI_Rendering_Order_Sorts = FindObjectsOfType<UI_Render_Order_Setting>();
        foreach (UI_Render_Order_Setting ui in uI_Rendering_Order_Sorts)
        {
            ui.Sorting();
        }
    }
}