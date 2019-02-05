using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateTickEvent : Event
{
    public void LateUpdate()
    {
        base.Update();
        Active();
        active_output.Active();
    }
    public override string GetInfoString()
    {
        return "매 프레임마다 실행신호를 내보냅니다. 하지만 TickEvent보다는 늦게 내보냅니다.";
    }
}
