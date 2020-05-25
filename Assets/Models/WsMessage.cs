using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WsMessage
{
    public string Action { get; set;}
    public object Content { get; set; }

    public WsMessage()
    {
    }
}
