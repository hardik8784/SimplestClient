using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{

    GameObject InputFieldUserName, InputFieldPassword, SubmitButton, ToggleLogin, ToggleCreate;

    GameObject NetworkedClient;



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
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        ToggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        ToggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitButtonPressed()
    {
        //Debug.Log("We Good..Submit Button Pressed..!!");

        string n = InputFieldUserName.GetComponent<InputField>().text;
        string p = InputFieldPassword.GetComponent<InputField>().text;

        if (ToggleLogin.GetComponent<Toggle>().isOn)
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.Login + "," + n + "," + p);
            //Debug.Log(ClientToServerSignifiers.Login + "," + n + "," + p);
        }
        else
        {
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
            //Debug.Log(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
        }

    }

    public void ToggleCreateValueChanged(bool newValue)
    {
        ToggleLogin.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void ToggleLoginValueChanged(bool newValue)
    {
        ToggleCreate.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }
}
    public static class ClientToServerSignifiers
    {
        public const int Login = 1;

        public const int CreateAccount = 2;
    }

    public static class ServerToClientSignifiers
    {
        public const int LoginResponse = 1;

        //public const int LoginFailure = 2;

        //public const int CreateAccountSuccess = 1;

        //public const int CreateAccountFailure = 2;
    }

public static class LoginResponses
{
    public const int Success = 1;

    public const int FailureNameInUse = 2;

    public const int FailureNameNotFound = 3;

    public const int IncorrectPassword = 4;
}
