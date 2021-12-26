using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBodyOnChildren : MonoBehaviour
{
    List<Transform> oneStageChildren;
    private void Start()
    {
        NewEdge.AlwaysUpdateLine = true;
        oneStageChildren = new List<Transform>();
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            if(child.parent == transform)
            {
                oneStageChildren.Add(child);
                if (child.GetComponent<Rigidbody2D>() == null)
                {
                    var rigidbody2d = child.gameObject.AddComponent<Rigidbody2D>();
                    rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                }
            }
        }
    }
    public float resetNodeYThreshold = 10;
    private void Update()
    {
        foreach(var child in oneStageChildren){
            if(child.position.y < resetNodeYThreshold)
            {
                child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                child.position = transform.position;
            }
        }
    }
}
