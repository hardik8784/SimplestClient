using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{

    GameObject InputFieldUserName, InputFieldPassword, SubmitButton, ToggleLogin, ToggleCreate;





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
        Debug.Log("We Good..Submit Button Pressed..!!");
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
