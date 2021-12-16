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

    string nameDisplay = "";

    GameObject GameSystemManager;
    [SerializeField]
    TicTacToeManager TicTacToeManager_;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        // search for the game system manager in the client side
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<GameSystemManager>() != null)
            {
                GameSystemManager = go;
            }
        }
        //TicTacToeManager_ = GameSystemManager.GetComponent<GameSystemManager>().GetMain_TicTacToeBoard.GetComponent<TicTacToeManager>();
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

        string[] csv = msg.Split(',');

        int Signifier = int.Parse(csv[0]);

        if (Signifier == ServerToClientSignifiers.AccountCreationComplete)
        {
            GameSystemManager.GetComponent<GameSystemManager>().ChangeGameState(GameStates.MainMenu);
        }
        else if (Signifier == ServerToClientSignifiers.LoginComplete)
        {
            TicTacToeManager_.PlayerName = csv[1];
            GameSystemManager.GetComponent<GameSystemManager>().ChangeGameState(GameStates.MainMenu);
        }
        else if (Signifier == ServerToClientSignifiers.AccountCreationFailed)
        {
            Debug.Log("Account Not Created, please try again");
        }
        else if (Signifier == ServerToClientSignifiers.LoginFailed)
        {
            Debug.Log("Login Failed, please try again");
        }

        else if (Signifier == ServerToClientSignifiers.OpponentPlay)
        {
            // receive actions from the opponent
            TicTacToeManager_.ServerPlacePosition(int.Parse(csv[1]), int.Parse(csv[2]), int.Parse(csv[3]));
        }

        // should the game room be established, start the tic tac toe game
        else if (Signifier == ServerToClientSignifiers.GameStart)
        {
            GameSystemManager.GetComponent<GameSystemManager>().ChangeGameState(GameStates.TicTacToe);
            TicTacToeManager_.PlayerTurn = 1; 
            TicTacToeManager_.PlayerID = int.Parse(csv[1]); 

            if (TicTacToeManager_.PlayerID == 1 ||
            TicTacToeManager_.PlayerID == 2)

                TicTacToeManager_.IDDisplay.text = "PlayerID: " + TicTacToeManager_.PlayerName;

        }
        else if (Signifier == ServerToClientSignifiers.SendMessage)
        {
            Debug.Log("Message: " + csv[1]);

            TicTacToeManager_.ReceiveMessage(csv[1]);
        }
        else if (Signifier == ServerToClientSignifiers.NotifyOpponentWin)
        {
            Debug.Log("Your Compititor has won: " + csv[1]);
            TicTacToeManager_.GameOverOnWin();
        }
        else if (Signifier == ServerToClientSignifiers.GameReset)
        {
            Debug.Log("Opponent resets the game");
            TicTacToeManager_.ResetGame();
    
        }
        else if (Signifier == ServerToClientSignifiers.ChangeTurn)
        {
           
            TicTacToeManager_.CheckTurn(int.Parse(csv[1]));
        }
    }


    public bool IsConnected()
    {
        return isConnected;
    }


}


public static class ClientToServerSignifiers
{
    public const int CreateAccount = 1;
    public const int Login = 2;
    public const int WaitingToJoinGameRoom = 3;
    public const int TicTacToe = 4; 
    public const int PlayerAction = 5;
    public const int SendPresetMessage = 6;
    public const int PlayerWins = 7;
}
public static class ServerToClientSignifiers
{
    public const int LoginComplete = 1;
    public const int LoginFailed = 2;
    public const int AccountCreationComplete = 3;
    public const int AccountCreationFailed = 4;
    public const int OpponentPlay = 5;
    public const int GameStart = 6;
    public const int SendMessage = 7;
    public const int NotifyOpponentWin = 8; 
    public const int ChangeTurn = 9;
    public const int GameReset = 10;
}
