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

    GameObject MainTicTacToeBoard;
    public GameObject GetMain_TicTacToeBoard => MainTicTacToeBoard;

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
            else if (go.name == "Main_TicTacToeBoard")
                MainTicTacToeBoard = go;

        }


        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        ToggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        ToggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);

        FindGameSessionButton.GetComponent<Button>().onClick.AddListener(FindGameSessionButtonPressed);
      


        ChangeGameState(GameStates.LoginMenu);

    }


    // Update is called once per frame
    void Update()
    {
     
    }

   

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
      
        InputFieldUserName.SetActive(false);
        InputFieldPassword.SetActive(false);
        SubmitButton.SetActive(false);
        ToggleLogin.SetActive(false);
        ToggleCreate.SetActive(false);
        FindGameSessionButton.SetActive(false);
      
        InfoText.SetActive(false);
        InfoText2.SetActive(false);
       
        MainTicTacToeBoard.SetActive(false);
       

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
