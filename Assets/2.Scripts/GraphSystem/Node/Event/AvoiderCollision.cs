using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoiderCollision : Event,IExpandableDisplay
{
    private TextMesh textMesh;

    private Transform AvoiderTr;
    public StringOutputModule string_output;

    public override void Awake()
    {
        base.Awake();
        textMesh = GetComponentInChildren<TextMesh>();
        GameObject avoider = GameObject.FindGameObjectWithTag("Avoider");
        if (avoider != null)
            AvoiderTr = avoider.GetComponent<Transform>();
        else
            Debug.LogWarning("avoider not found");
    }
    string collTags;
    public override void Update()
    {
        base.Update();
        if (isActive >= 1)
            isActive--;
        if (isExpanded)
            collTags = "AvoiderCollision\n";
		else
			collTags = "AvoiderCollision\n\nTag";
        Transform real_avoider_model = AvoiderTr.GetChild(0);

        Collider2D[] colls;// = new Collider2D[10];
        //int colliderCount = AvoiderTr.GetComponentInChildren<Collider2D>().OverlapCollider(new ContactFilter2D(), colls);
        colls = Physics2D.OverlapAreaAll(real_avoider_model.position - real_avoider_model.lossyScale, real_avoider_model.position + real_avoider_model.lossyScale);
        bool bExistCollider = false;
        for (int i = 0,count = 0; i< colls.Length; ++i)
        {
            Collider2D coll = colls[i];
            if (!(coll.transform.IsChildOf(AvoiderTr) || coll.transform == AvoiderTr))
            {
                bExistCollider = true;
                //상수,변수의 신호를 재전송
                if (count != 0)
                    NodeManager.instance.ProcessNodeToReoutput();

                count++;
                string_output.Input(coll.tag);
                active_output.Active();
                Active();
                collTags += (coll.tag + "\n");
            }
        }
        if (isExpanded && !bExistCollider)
            collTags += "값없음";
        if (textMesh != null)
        {
            textMesh.text = collTags;
        }
    }
    public bool isExpanded
    {
        get; set;
    }
    public void NormalDisplay()
    {
        if (textMesh != null)
            textMesh.text = "AvoiderCollision\n\nTag";
    }
    public void ExpandDisplay()
    {
        if (textMesh != null)
        {
            textMesh.text = "AvoiderCollision\n" + collTags;
            //Transform real_avoider_model = AvoiderTr.GetChild(0);
            //Collider2D[] colls;// = Physics2D.OverlapAreaAll(AvoiderTr.position - AvoiderTr.lossyScale, AvoiderTr.position + AvoiderTr.lossyScale);
            //colls = Physics2D.OverlapAreaAll(real_avoider_model.position - real_avoider_model.lossyScale, real_avoider_model.position + real_avoider_model.lossyScale);
            //for (int i = 0; i < colls.Length; ++i)
            //{
            //    Collider2D coll = colls[i];
            //    if (!(coll.transform.IsChildOf(AvoiderTr) || coll.transform == AvoiderTr))
            //    {
            //        textMesh.text += (coll.tag + "\n");
            //    }
            //}
        }
    }
    public override string GetInfoString()
    {
        return "Avoider와 충돌하고있는 것들의 이름을 내보냅니다.";
    }
}
