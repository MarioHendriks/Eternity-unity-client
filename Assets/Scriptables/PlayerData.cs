using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Threading;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public Socket socket;
    byte[] bytes;


    public string name;
    private WsMessage _wsMessage;

    public bool connectWebsocket()
    {

       bytes = new byte[1024];

        try
        {
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 9595);

            // Create a TCP/IP  socket.    
            socket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.    
            try
            {
                // Connect to Remote EndPoint  
                socket.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                socket.RemoteEndPoint.ToString());


                Thread t = new Thread(new ThreadStart(listenToServer));
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return true;
    }

    void listenToServer()
    {
        while(socket != null)
        {
            int bytesRec = socket.Receive(bytes);
            string serverMessage = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }
       
    }

    public void SendToServer(WsMessage wsMessage)
    {

        byte[] msg = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(wsMessage));
        // Send the data through the socket.    
        int bytesSent = socket.Send(msg);
    }

    void CloseSocket()
    {
        // Release the socket.    
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
