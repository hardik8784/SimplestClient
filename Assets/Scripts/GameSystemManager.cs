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

    GameObject GridSpace_00, GridSpace_01, GridSpace_02, GridSpace_10, GridSpace_11, GridSpace_12, GridSpace_21, GridSpace_22, GridSpace_23;


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
            else if (go.name == "GridSpace_00")
                GridSpace_00 = go;
            else if (go.name == "GridSpace_01")
                GridSpace_01 = go;
            else if (go.name == "GridSpace_02")
                GridSpace_02 = go;
            else if (go.name == "GridSpace_10")
                GridSpace_10 = go;
            else if (go.name == "GridSpace_11")
                GridSpace_11 = go;
            else if (go.name == "GridSpace_12")
                GridSpace_12 = go;
            else if (go.name == "GridSpace_21")
                GridSpace_21 = go;
            else if (go.name == "GridSpace_22")
                GridSpace_22 = go;
            else if (go.name == "GridSpace_23")
                GridSpace_23 = go;
        }


        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        ToggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        ToggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);

        FindGameSessionButton.GetComponent<Button>().onClick.AddListener(FindGameSessionButtonPressed);
        PlaceHolderGameButton.GetComponent<Button>().onClick.AddListener(PlaceHolderGameButtonPressed);

        //GridSpace_00.GetComponent<Button>().onClick.AddListener(GridSpace_00_Cliked);
        //GridSpace_01.GetComponent<Button>().onClick.AddListener(GridSpace_01_Cliked);
        //GridSpace_02.GetComponent<Button>().onClick.AddListener(GridSpace_02_Cliked);
        //GridSpace_10.GetComponent<Button>().onClick.AddListener(GridSpace_10_Cliked);
        //GridSpace_11.GetComponent<Button>().onClick.AddListener(GridSpace_11_Cliked);
        //GridSpace_12.GetComponent<Button>().onClick.AddListener(GridSpace_12_Cliked);
        //GridSpace_21.GetComponent<Button>().onClick.AddListener(GridSpace_21_Cliked);
        //GridSpace_22.GetComponent<Button>().onClick.AddListener(GridSpace_22_Cliked);
        //GridSpace_23.GetComponent<Button>().onClick.AddListener(GridSpace_23_Cliked);


        ChangeGameState(GameStates.Login);

    }

    //public void GamePlay(int Button, int Player)
    //{

    //}

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
        //    ChangeGameState(GameStates.PlayingTicTacToe);
        //}
    }

    private void SubmitButtonPressed()
    {
        //Debug.Log("We Good..Submit Button Pressed..!!");

        string n = InputFieldUserName.GetComponent<InputField>().text;
        string p = InputFieldPassword.GetComponent<InputField>().text;

        if (ToggleLogin.GetComponent<Toggle>().isOn)
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.Login + "," + n + "," + p);

            ChangeGameState(GameStates.MainMenu);
            //Debug.Log(ClientToServerSignifiers.Login + "," + n + "," + p);
        }
        else
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
            //Debug.Log(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
        }

    }

    private void FindGameSessionButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.AddToGameSessionQueue + "");

        ChangeGameState(GameStates.WaitingForMatch);

        Debug.Log("Changing the state Waiting For Match");
    }

    private void PlaceHolderGameButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "");
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
        //InputFieldUserName, InputFieldPassword, SubmitButton, ToggleLogin, ToggleCreate;

        //NetworkedClient;

        //FindGameSessionButton, PlaceHolderGameButton;

        InputFieldUserName.SetActive(false);
        InputFieldPassword.SetActive(false);
        SubmitButton.SetActive(false);
        ToggleLogin.SetActive(false);
        ToggleCreate.SetActive(false);
        FindGameSessionButton.SetActive(false);
        PlaceHolderGameButton.SetActive(false);
        InfoText.SetActive(false);
        InfoText2.SetActive(false);
      //  NetworkedClient.SetActive(false);

        if (newState == GameStates.Login)
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
        else if (newState == GameStates.WaitingForMatch)
        {
            //PlaceHolderGameButton.SetActive(true);
        }
        else if (newState == GameStates.PlayingTicTacToe)
        {
            Debug.Log("PlaceHolderButton SetActive to True");
            PlaceHolderGameButton.SetActive(true);
        }
    }
}


public static class GameStates
{
    public const int Login = 1;

    public const int MainMenu = 2;

    public const int WaitingForMatch = 3;

    public const int PlayingTicTacToe = 4;
}