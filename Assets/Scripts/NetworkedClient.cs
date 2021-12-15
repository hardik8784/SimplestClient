/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 14,2021
 * File : NetworkedClient.cs
 * Description : This is the script to Assignthe Ids and Open the Container when player collide with the help of OnTriggerEvent
 * Revision History : v0.1 > Added Comments to know the Code better before start anything & to include a program header
 */


using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedClient : MonoBehaviour
{

    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5491;
    byte error;
    bool isConnected = false;
    int ourClientID;
   

    GameObject GameSystemManager;

    string PlayerName;

    public int WhoisthePlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "GameSystemManager")
                GameSystemManager = go;
        }

        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.S))
        //    SendMessageToHost("Hello from client");

        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }

    private void Connect()
    {

        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.2.47", socketPort, 0, out error); // server is local on network

            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }

    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }

    public void SendMessageToHost(string msg)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);
    }

    private void ProcessRecievedMsg(string msg, int id)
    {
        Debug.Log("msg recieved = " + msg + ".  connection id = " + id);

        //string[] csv = msg.Split(',');

        //int Signifier = int.Parse(csv[0]);

        //if (Signifier == ServerToClientSignifiers.LoginResponse)
        //{
        //    //On Success
        //    int LoginResultSignifier = int.Parse(csv[1]);

        //    if (LoginResultSignifier == LoginResponses.Success)
        //    {
        //        Debug.Log("Entered into the loop");
        //        GameSystemManager.GetComponent<GameSystemManager>().ChangeGameState(GameStates.MainMenu);
        //    }
        //}
        //else if (Signifier == ServerToClientSignifiers.GameSessionStarted)
        //{
        //    GameSystemManager.GetComponent<GameSystemManager>().ChangeGameState(GameStates.TicTacToeStarted);
        //    Debug.Log("Next Item,Playing tic tac toe!!!");
        //    WhoisthePlayer = id;
        //    PlayerName = csv[1];
        //    Debug.Log("PlayerName : " + PlayerName);
        //    Debug.Log("Player ID : " + WhoisthePlayer);
        //}
        //else if (Signifier == ServerToClientSignifiers.OpponentTicTacToePlay)
        //{
        //    Debug.Log("OpponentTicTacToePlay Requested");
        //    Debug.Log("Player1 id : " + int.Parse(csv[1]));
        //    Debug.Log("Player2 id : " + int.Parse(csv[2]));
        //}
    }

    public bool IsConnected()
    {
        return isConnected;
    }


}


//public static class ClientToServerSignifiers
//{
//    public const int Login = 1;

//    public const int CreateAccount = 2;

//    public const int AddToGameSessionQueue = 3;

//    public const int TicTacToePlay = 4;

//    public const int OpponentTurn = 5;
//}

//public static class ServerToClientSignifiers
//{
//    public const int LoginResponse = 1;

//    public const int GameSessionStarted = 2;

//    public const int OpponentTicTacToePlay = 3;

//    public const int GameStarted = 4;

//    //public const int LoginFailure = 2;

//    //public const int CreateAccountSuccess = 1;

//    //public const int CreateAccountFailure = 2;
//}

//public static class LoginResponses
//{
//    public const int Success = 1;

//    public const int FailureNameInUse = 2;

//    public const int FailureNameNotFound = 3;

//    public const int IncorrectPassword = 4;
//}
