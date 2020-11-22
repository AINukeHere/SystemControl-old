using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EdgePointLocalPositionSetZero : Editor
{
    [MenuItem("Tool/EdgePointLocalPositionSetZero")]
    static void Init()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("EdgePoint");
        foreach (GameObject obj in objects)
        {
            obj.name = "EdgePoint";
            obj.transform.localPosition = new Vector3(0, 0, 0);
            SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
            Transform parentTr = obj.transform.parent;
            if (parentTr == null)
                continue;
            SpriteRenderer parentRenderer = parentTr.GetComponent<SpriteRenderer>();
            if (parentRenderer == null)
                continue;
            renderers[renderers.Length - 1].color = parentTr.GetComponent<SpriteRenderer>().color;
        }
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