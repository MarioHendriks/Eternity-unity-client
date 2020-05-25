using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData player;
    // Start is called before the first frame update
    void Start()
    {
        if (player.connectWebsocket())
        {
            RegisterSocketToClient();
        }
    }

    private void RegisterSocketToClient()
    {
        WsMessage ws = new WsMessage();
        ws.Action = "Register";

        User user = new User();
        user.username = player.name;

        ws.Content = user;

        player.SendToServer(ws);
    }
}
