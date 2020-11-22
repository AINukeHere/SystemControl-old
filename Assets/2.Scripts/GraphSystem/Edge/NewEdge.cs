using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEdge : MonoBehaviour
{
    private Transform _startTarget,_endTarget;
    public Transform startTarget
    {
        get { return _startTarget; }
        private set
        {
            _startTarget = value;
            LineRendererUpdate();
        }
    }
    public Transform endTarget
    {
        get { return _endTarget; }
        private set
        {
            _endTarget = value;
            LineRendererUpdate();
        }
    }
    public float directionCoeff;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider2D;
    [SerializeField] private int nPoints;
    private int bHighlighting;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = nPoints;
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        edgeCollider2D.points = new Vector2[nPoints];
        transform.position = new Vector3(0, 0, -10);
        transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);

        SetNormalEdgeSize();
    }
    public void Update()
    {
        if (bHighlighting > 0)
            bHighlighting -= 1;
        if (bHighlighting == 1)
            SetNormalEdgeSize();
        //LineRendererUpdate();
    }
    public void LineRendererUpdate()
    {
        if (startTarget != null && endTarget != null)
        {
            Vector3[] tempArrayVec3 = new Vector3[nPoints];
            Vector2[] tempArrayVec2 = new Vector2[nPoints];
            Vector3 pos;
            Vector3 p0 = startTarget.position;
            Vector3 p1 = endTarget.position;
            directionCoeff = Mathf.Min(Vector3.Distance(p0, p1), 20);
            Vector3 d0 = startTarget.right * directionCoeff;
            Vector3 d1 = endTarget.right * directionCoeff;

            Vector3 a0 = p0;
            Vector3 a1 = d0;
            Vector3 a2 = 3 * p1 - 3 * p0 - 2 * d0 - d1;
            Vector3 a3 = 2 * p0 - 2 * p1 + d0 + d1;
            float t;
            for (int i = 0; i < nPoints; ++i)
            {
                t = (float)i / (nPoints - 1);
                pos = a0 + a1 * t + a2 * t * t + a3 * t * t * t;
                tempArrayVec3[i] = pos;
                tempArrayVec2[i] = pos;
                //lineRenderer.SetPosition(i, pos);
                //edgeCollider2D.points[i] = pos;
            }
            lineRenderer.SetPositions(tempArrayVec3);
            edgeCollider2D.points = tempArrayVec2;
        }
    }
    public void Highlighting()
    {
        bHighlighting = 3;
        lineRenderer.startWidth = 0.75f;
        lineRenderer.endWidth = 0.75f;
    }
    public void SetNormalEdgeSize()
    {
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
    }
    public void SetStartTarget<T>(OutputModule<T> outputModule)
    {
        startTarget = outputModule.transform;
        lineRenderer.startColor = outputModule.nodeColor;
    }
    public void SetEndTarget(Transform endTr)
    {
        endTarget = endTr;
    }
    static int sortingOrder = 0;
    public void SetOrdering(string sortingLayerName, int sortingOrder)
    {
        lineRenderer.sortingLayerName = "Default";
        lineRenderer.sortingOrder = NewEdge.sortingOrder++;
        Debug.Log("edge sortingOrder = " + NewEdge.sortingOrder);
        //lineRenderer.sortingLayerName = sortingLayerName;
        //lineRenderer.sortingOrder = sortingOrder;
    }
}
