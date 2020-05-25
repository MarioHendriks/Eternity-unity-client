using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIhandler : MonoBehaviour
{
    public TextMeshProUGUI NameUI;
    public PlayerData PlayerData;

    public void setName(string name)
    {
        NameUI.text = name;
        PlayerData.name = name;
        if (PlayerData.connectWebsocket())
        {
            WsMessage ws = new WsMessage();
            ws.Action = "actie";
            PlayerData.SendToServer(ws);
        }
    }

    public void RegisterSocketToClient()
    {
        WsMessage ws = new WsMessage();
        ws.Action = "Register";

        User user = new User();
        user.username = "Mario";

        ws.Content = user;

        PlayerData.SendToServer(ws);
    }

}
