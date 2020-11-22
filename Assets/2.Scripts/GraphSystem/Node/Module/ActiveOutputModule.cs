using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActivateType
{

}
public class ActiveOutputModule : OutputModule<ActivateType?>
{
    public void Active()
    {
        base.Input(new ActivateType());
    }
    public override void AfterInputCallBack()
    {
    }
}

///구버전 코드
//public class ActiveOutputModule : MonoBehaviour, Movable
//{

//    private GameObject edgePrefab;
//    [SerializeField] private List<NewEdge> edges;
//    [SerializeField] private float autoAssignRadius = 0.5f;
//    [SerializeField] protected List<ActiveInputModule> connectedInputModules = new List<ActiveInputModule>();
//    private NewEdge currentEdge;

//    protected virtual void Awake()
//    {
//        edgePrefab = Resources.Load<GameObject>("NewEdge");
//    }
//    public void Active()
//    {
//        foreach (ActiveInputModule connectedInputModule in connectedInputModules)
//            connectedInputModule.Active();
//    }

//    public bool isMoving { get; set; }
//    public virtual void MoveBegin()
//    {
//        currentEdge = Instantiate(edgePrefab).GetComponent<NewEdge>();
//        edges.Add(currentEdge);
//        currentEdge.startTarget = transform;
//        currentEdge.endTarget = GameObject.Find("Controller").transform;
//    }
//    public virtual void Move(Vector2 pos, bool bGroup = false)
//    {
//        if (bGroup)
//            return;
//        if (!isMoving)
//        {
//            isMoving = true;
//            MoveBegin();
//        }
//        currentEdge.EdgeUpdate();
//    }
//    public virtual void MoveEnd(Vector2 pos, bool bGroup = false)
//    {
//        if (bGroup)
//            return;
//        bool bFoundInputModule = false;  //colls에서 InputNode를 찾았는지 체크
//        Collider2D[] colls;
//        colls = Physics2D.OverlapCircleAll(pos, autoAssignRadius);
//        if (colls.Length > 0)
//        {
//            int min_dist_idx = -1;
//            for (int i = 0; i < colls.Length; ++i)
//            {
//                Collider2D coll = colls[i];
//                if (coll.CompareTag("InputNode"))
//                {
//                    //아직 최저가 결정되지 않았거나 최저보다 더 적을 때 갱신
//                    if (min_dist_idx == -1 ||
//                        Vector3.Distance(coll.transform.position, pos) < Vector3.Distance(colls[min_dist_idx].transform.position, pos))
//                        min_dist_idx = i;
//                }
//            }
//            if (min_dist_idx != -1)
//            {
//                ActiveInputModule nearestInputModule = colls[min_dist_idx].GetComponent<ActiveInputModule>();
//                if (nearestInputModule != null)
//                {
//                    bool bExist = false;
//                    foreach (ActiveInputModule target in connectedInputModules)
//                    {
//                        if (target.Equals(nearestInputModule))
//                        {
//                            bExist = true;
//                            break;
//                        }
//                    }
//                    if (!bExist && EdgeManager.instance.registerEdge(colls[min_dist_idx].transform, gameObject))
//                    {
//                        connectedInputModules.Add(nearestInputModule);
//                        currentEdge.endTarget = colls[min_dist_idx].transform;
//                        Debug.Log(colls[min_dist_idx].gameObject.name + " connected");
//                        bFoundInputModule = true;
//                    }
//                }
//            }
//        }
//        if (!bFoundInputModule)
//        {
//            edges.Remove(currentEdge);
//            Destroy(currentEdge.gameObject);
//        }

//        isMoving = false;
//    }
//    public virtual void RemoveEdge(Transform edgeTr)
//    {
//        ActiveInputModule inputParamT = edgeTr.GetComponent<ActiveInputModule>();
//        for (int i = 0; i < connectedInputModules.Count; ++i)
//        {
//            if (connectedInputModules[i].Equals(inputParamT))
//            {
//                connectedInputModules.RemoveAt(i);
//                break;
//            }
//        }
//    }
//}
