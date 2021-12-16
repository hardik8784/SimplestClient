using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    // Start is called before the first frame update
    public string Messages_;

    private Button ButtonConnected;
    private NetworkedClient networkedClient;
    void Start()
    {
        ButtonConnected = GetComponent<Button>();
        networkedClient = FindObjectOfType<NetworkedClient>();

        ButtonConnected.onClick.AddListener(SendPresetMessageToOpponent);
    }

    public void SendPresetMessageToOpponent()
    {
        networkedClient.SendMessageToHost(ClientToServerSignifiers.SendPresetMessage + "," + Messages_);
    }
}
