using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPlayEvent : Event
{
    public void Start()
    {
        Active();
        active_output.Active();
    }
    public override string GetInfoString()
    {
        return "맨 처음 한번만 실행신호를 내보냅니다.";
    }
}
