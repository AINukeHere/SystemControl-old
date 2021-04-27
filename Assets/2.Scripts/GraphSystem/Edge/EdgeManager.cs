using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EdgeManager : MonoBehaviour
{
    public static EdgeManager instance { get; private set; }

    //엣지 연결정보 - Edge, <outputModule의 Transform, inputModule의 Transform>
    private Dictionary<NewEdge, KeyValuePair<Transform, Transform>> edge_outputModule;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            edge_outputModule = new Dictionary<NewEdge, KeyValuePair<Transform, Transform>>();
        }
        else Destroy(gameObject);
    }
    public void removeEdge(NewEdge edge, Transform endTr)
    {
        edge_outputModule[edge].Value.SendMessage("RemoveEdge", endTr);
        edge_outputModule.Remove(edge);
        Destroy(edge.gameObject);
    }
    public bool registerEdge(NewEdge edge, Transform endTr, Transform connectionModule)
    {
        if (isAlreadyExistEdge(endTr))
        {
            return false;
        }
        else
        {
            edge_outputModule.Add(edge,new KeyValuePair<Transform, Transform>(endTr,connectionModule));
            return true;
        }
    }
    public bool isAlreadyExistEdge(Transform target)
    {
        foreach(var edgeInfo in edge_outputModule)
            if (edgeInfo.Value.Key.Equals(target))
                return true;
        return false;
    }

    public void onNodeMoving(List<Transform> movingInputmodules, Transform node)
    {
        foreach (var output_inputNode in edge_outputModule)
        {
            //output 노드 순환하며 해당 node의 child인지 체크
            if (output_inputNode.Value.Value.IsChildOf(node))
            {
                output_inputNode.Key.LineRendererUpdate();
                continue;
            }

            //해당 노드의 input module과 같은 inputmodule이 있는지 체크
            foreach (var module in movingInputmodules)
            {
                if (output_inputNode.Value.Key.Equals(module))
                {
                    output_inputNode.Key.LineRendererUpdate();
                    break;
                }
            }
        }
    }
    public void UpdateAll()
    {
        foreach(var edgeInfo in edge_outputModule)
        {
            edgeInfo.Key.LineRendererUpdate();
        }
    }
}