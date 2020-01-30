using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TestSocketClient : MonoBehaviour
{
    private SocketClient socketClient = new SocketClient();

    public string hostname;
    public int port;


    // Use this for initialization 	
    void Start()
    {
        socketClient = new SocketClient();

        ConnectToSocketServer();

    }

    void OnDisable()
    {
        DisconnectFromSocketServer();
    }

    void OnEnable()
    {

    }

    [ContextMenu("Disconnect from Socket Server")]
    public void DisconnectFromSocketServer()
    {
        socketClient.DisconnectFromTcpServer();
    }


    [ContextMenu("Connect to Socket Server")]
    public void ConnectToSocketServer()
    {
        try
        {
            socketClient.ConnectToTcpServer(hostname, port);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Connecting to server {hostname}:{port} failed!");
            throw;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socketClient.SendMessage("This is a message sent from Unity client");
        }
    }
}