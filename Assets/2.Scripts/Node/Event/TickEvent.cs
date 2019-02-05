using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickEvent : Event
{
    public override void Update()
    {
        base.Update();
        Active();
        active_output.Active();
    }
    public override string GetInfoString()
    {
        return "매 프레임마다 실행신호를 내보냅니다.";
    }
}
