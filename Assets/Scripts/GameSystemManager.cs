using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{

    GameObject InputFieldUserName, InputFieldPassword, SubmitButton, ToggleLogin, ToggleCreate;

    GameObject NetworkedClient;

    GameObject FindGameSessionButton, PlaceHolderGameButton;

    GameObject InfoText, InfoText2;

    //GameObject GridSpace_00, GridSpace_01, GridSpace_02, GridSpace_10, GridSpace_11, GridSpace_12, GridSpace_21, GridSpace_22, GridSpace_23;

    GameObject MainTicTacToeBoard;

    public GameObject GetMain_TicTacToeBoard => MainTicTacToeBoard;

    //public int WhoisthePlayer = 0;

   // bool ChangeXor0 = false;

    //[SerializeField] public int playerID;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "InputFieldUserName")
                InputFieldUserName = go;
            else if (go.name == "InputFieldPassword")
                InputFieldPassword = go;
            else if (go.name == "SubmitButton")
                SubmitButton = go;
            else if (go.name == "ToggleLogin")
                ToggleLogin = go;
            else if (go.name == "ToggleCreate")
                ToggleCreate = go;
            else if (go.name == "NetworkedClient")
                NetworkedClient = go;
            else if (go.name == "FindGameSessionButton")
                FindGameSessionButton = go;
            else if (go.name == "PlaceHolderGameButton")
                PlaceHolderGameButton = go;
            else if (go.name == "InfoText")
                InfoText = go;
            else if (go.name == "InfoText2")
                InfoText2 = go;
            //else if (go.name == "GridSpace_00")
            //    GridSpace_00 = go;
            //else if (go.name == "GridSpace_01")
            //    GridSpace_01 = go;
            //else if (go.name == "GridSpace_02")
            //    GridSpace_02 = go;
            //else if (go.name == "GridSpace_10")
            //    GridSpace_10 = go;
            //else if (go.name == "GridSpace_11")
            //    GridSpace_11 = go;
            //else if (go.name == "GridSpace_12")
            //    GridSpace_12 = go;
            //else if (go.name == "GridSpace_21")
            //    GridSpace_21 = go;
            //else if (go.name == "GridSpace_22")
            //    GridSpace_22 = go;
            //else if (go.name == "GridSpace_23")
            //    GridSpace_23 = go;
            else if (go.name == "Main_TicTacToeBoard")
                MainTicTacToeBoard = go;

        }


        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        ToggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        ToggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);

        FindGameSessionButton.GetComponent<Button>().onClick.AddListener(FindGameSessionButtonPressed);
        //PlaceHolderGameButton.GetComponent<Button>().onClick.AddListener(PlaceHolderGameButtonPressed);

        //GridSpace_00.GetComponent<Button>().onClick.AddListener(GridSpace_00_Cliked);
        //GridSpace_01.GetComponent<Button>().onClick.AddListener(GridSpace_01_Cliked);
        //GridSpace_02.GetComponent<Button>().onClick.AddListener(GridSpace_02_Cliked);
        //GridSpace_10.GetComponent<Button>().onClick.AddListener(GridSpace_10_Cliked);
        //GridSpace_11.GetComponent<Button>().onClick.AddListener(GridSpace_11_Cliked);
        //GridSpace_12.GetComponent<Button>().onClick.AddListener(GridSpace_12_Cliked);
        //GridSpace_21.GetComponent<Button>().onClick.AddListener(GridSpace_21_Cliked);
        //GridSpace_22.GetComponent<Button>().onClick.AddListener(GridSpace_22_Cliked);
        //GridSpace_23.GetComponent<Button>().onClick.AddListener(GridSpace_23_Cliked);


        ChangeGameState(GameStates.LoginMenu);

    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    ChangeGameState(GameStates.Login);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ChangeGameState(GameStates.MainMenu);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    ChangeGameState(GameStates.WaitingForMatch);
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    ChangeGameState(GameStates.TicTacToeStarted);
        //}
    }

    //private void GridSpace_00_Cliked()
    //{
    //     NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.OpponentTurn + " ");
    //    //GamePlay(00, playerID);
    //   // GamePlay(00, NetworkedClient.GetComponent<NetworkedClient>().WhoisthePlayer);
    //    // Debug.Log("PlayerID : " + playerID);
    //    //  Debug.Log("WhoisthePlayer:" + WhoisthePlayer);
    //}

    //private void GridSpace_01_Cliked()
    //{
       
    //    // GamePlay(01, WhoisthePlayer);
    //    GamePlay(01, NetworkedClient.GetComponent<NetworkedClient>().WhoisthePlayer);
    //}

    //private void GridSpace_02_Cliked()
    //{
    //   // GamePlay(02, WhoisthePlayer);
    //}
    //private void GridSpace_10_Cliked()
    //{
    //  //  GamePlay(10, WhoisthePlayer);
    //}
    //private void GridSpace_11_Cliked()
    //{
    //  //  GamePlay(11, WhoisthePlayer);
    //}
    //private void GridSpace_12_Cliked()
    //{
    //   // GamePlay(12, WhoisthePlayer);
    //}
    //private void GridSpace_21_Cliked()
    //{
    //  //  GamePlay(21, WhoisthePlayer);
    //}
    //private void GridSpace_22_Cliked()
    //{
    //   // GamePlay(22, WhoisthePlayer);
    //}
    //private void GridSpace_23_Cliked()
    //{
    //    //GamePlay(23, WhoisthePlayer);
    //}
   
    //public void GamePlay(int WhichButtonClicked, int player)
    //{
    //    string Xor0 = "";
    //    int FilledSlots = 0;

    //    switch (WhichButtonClicked)
    //    {
    //        // GridSpace_00, GridSpace_01, GridSpace_02, GridSpace_10, GridSpace_11, GridSpace_12, GridSpace_21, GridSpace_22, GridSpace_23;
    //        case 00:
    //            Xor0 = Player_Choice(player, GridSpace_00.transform.GetChild(0).GetComponent<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 1;
    //            break;
    //        case 01:
    //            Xor0 = Player_Choice(player, GridSpace_01.transform.GetChild(0).GetComponent<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 2;
    //            break;
    //        case 02:
    //            Xor0 = Player_Choice(player, GridSpace_02.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 3;
    //            break;
    //        case 10:
    //            Xor0 = Player_Choice(player, GridSpace_10.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 4;
    //            break;
    //        case 11:
    //            Xor0 = Player_Choice(player, GridSpace_11.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 5;
    //            break;
    //        case 12:
    //            Xor0 = Player_Choice(player, GridSpace_12.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 6;
    //            break;
    //        case 21:
    //            Xor0 = Player_Choice(player, GridSpace_21.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 7;
    //            break;
    //        case 22:
    //            Xor0 = Player_Choice(player, GridSpace_22.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 8;
    //            break;
    //        case 23:
    //            Xor0 = Player_Choice(player, GridSpace_23.GetComponentInChildren<Text>().text);
    //            GridSpace_00.GetComponentInChildren<Text>().text = Xor0;
    //            FilledSlots = 9;
    //            break;
    //        default:
    //            break;
    //    }

    //    if (ChangeXor0)
    //    {
    //        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.OpponentTurn + "");
    //        Debug.Log(ClientToServerSignifiers.OpponentTurn + "OpponentTurn");
    //    }

    //}

    //public void SetXor0(int GridSpace_Number, string Xor0)
    //{
    //    switch (GridSpace_Number)
    //    {
    //        // GridSpace_00, GridSpace_01, GridSpace_02, GridSpace_10, GridSpace_11, GridSpace_12, GridSpace_21, GridSpace_22, GridSpace_23;
    //        case 00:
    //            GridSpace_00.transform.GetChild(0).GetComponent<Text>().text = Xor0;
    //            break;
    //        case 01:
    //            GridSpace_01.transform.GetChild(0).GetComponent<Text>().text = Xor0;
    //            break;
    //        case 02:
    //            GridSpace_02.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 10:
    //            GridSpace_10.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 11:
    //            GridSpace_11.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 12:
    //            GridSpace_12.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 21:
    //            GridSpace_21.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 22:
    //            GridSpace_22.GetComponentInChildren<Text>().text = Xor0;
    //            break;
    //        case 23:
    //            GridSpace_23.GetComponentInChildren<Text>().text = Xor0;
    //            //GridSpace_23.transform.GetChild(0).GetComponent<Text>().text = Xor0;
    //            break;
    //    }
    //}

    //public string Player_Choice(int player, string PreviousValue)
    //{
    //    string NewValue = "";
    //    //if (PreviousValue == "A")
    //    //{
    //    //    ChangeXor0 = true;
    //        if (player == 1)
    //        {
    //            NewValue = "X";
    //        }
    //        // WhoisthePlayer = PlayerID;
    //    //    else
    //    //    {
    //    //        NewValue = "O";
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    NewValue = PreviousValue;
    //    //    ChangeXor0 = false;
    //    //}
    //    return NewValue;
    //}

    //public void UpdateUserName_ID(string UserName,int Id)
    //{

    //}

    private void SubmitButtonPressed()
    {
        //Debug.Log("We Good..Submit Button Pressed..!!");

        string n = InputFieldUserName.GetComponent<InputField>().text;
        string p = InputFieldPassword.GetComponent<InputField>().text;

        string msg;

        if (ToggleLogin.GetComponent<Toggle>().isOn)
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.Login + "," + n + "," + p);

            //ChangeGameState(GameStates.MainMenu);
            Debug.Log(ClientToServerSignifiers.Login + "," + n + "," + p);
        }
        else
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
            Debug.Log(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
        }

    }

    private void FindGameSessionButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.WaitingToJoinGameRoom + "");

        ChangeGameState(GameStates.WaitingForPlayers);

        Debug.Log("Changing the state Waiting For Match");
    }

    //private void PlaceHolderGameButtonPressed()
    //{
    //    NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "");
    //}

    private void ToggleCreateValueChanged(bool newValue)
    {
        ToggleLogin.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    private void ToggleLoginValueChanged(bool newValue)
    {
        ToggleCreate.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void ChangeGameState(int newState)
    {
        //InputFieldUserName, InputFieldPassword, SubmitButton, ToggleLogin, ToggleCreate;

        //NetworkedClient;

        //FindGameSessionButton, PlaceHolderGameButton;

        //GameObject GridSpace_00, GridSpace_01, GridSpace_02, GridSpace_10, GridSpace_11, GridSpace_12, GridSpace_21, GridSpace_22, GridSpace_23;

        InputFieldUserName.SetActive(false);
        InputFieldPassword.SetActive(false);
        SubmitButton.SetActive(false);
        ToggleLogin.SetActive(false);
        ToggleCreate.SetActive(false);
        FindGameSessionButton.SetActive(false);
        //PlaceHolderGameButton.SetActive(false);
        InfoText.SetActive(false);
        InfoText2.SetActive(false);
        //GridSpace_00.SetActive(false);
        //GridSpace_01.SetActive(false);
        //GridSpace_02.SetActive(false);
        //GridSpace_10.SetActive(false);
        //GridSpace_11.SetActive(false);
        //GridSpace_12.SetActive(false);
        //GridSpace_21.SetActive(false);
        //GridSpace_22.SetActive(false);
        //GridSpace_23.SetActive(false);
        MainTicTacToeBoard.SetActive(false);
        //JoinGameRoom.SetActive(false);
        //  NetworkedClient.SetActive(false);

        if (newState == GameStates.LoginMenu)
        {
            InputFieldUserName.SetActive(true);
            InputFieldPassword.SetActive(true);
            SubmitButton.SetActive(true);
            ToggleLogin.SetActive(true);
            ToggleCreate.SetActive(true);
            InfoText.SetActive(true);
            InfoText2.SetActive(true);
        }
        else if(newState == GameStates.MainMenu)
        {
            FindGameSessionButton.SetActive(true);
        }
        else if (newState == GameStates.WaitingForPlayers)
        {
            //PlaceHolderGameButton.SetActive(true);
        }
        else if (newState == GameStates.TicTacToe)
        {
            Debug.Log("TicTacToe Board SetActive to True");
            MainTicTacToeBoard.SetActive(true);
            //JoinGameRoom.SetActive(true);
            //PlaceHolderGameButton.SetActive(true);
            //TicTacToeBoard.SetActive(true);
            //GridSpace_00.SetActive(true);
            //GridSpace_01.SetActive(true);
            //GridSpace_02.SetActive(true);
            //GridSpace_10.SetActive(true);
            //GridSpace_11.SetActive(true);
            //GridSpace_12.SetActive(true);
            //GridSpace_21.SetActive(true);
            //GridSpace_22.SetActive(true);
            //GridSpace_23.SetActive(true);
        }
       
    }
}

static public class GameStates
{
    public const int LoginMenu = 1;
    public const int MainMenu = 2;
    public const int WaitingForPlayers = 3;
    public const int TicTacToe = 4;
}
