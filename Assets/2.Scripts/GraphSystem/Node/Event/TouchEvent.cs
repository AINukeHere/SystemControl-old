using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : Event
{
    public override void Update()
    {
        base.Update();
        if (isActive >= 1)
        {
            active_output.Active();
            isActive = 0;
        }
    }
    public override string GetInfoString()
    {
        return "사용자가 클릭을 했을 때 실행신호를 내보냅니다.";
    }
}
