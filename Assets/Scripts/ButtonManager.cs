using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private int XPos;
    [SerializeField] private int YPos;

    private Button ButtonConnected;

    // Start is called before the first frame update
    void Start()
    {
        ButtonConnected = GetComponent<Button>();
        ButtonConnected.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonClicked()
    {
        Debug.Log(XPos + "," + YPos);
        // check the manager ref if the icon is filled first

      
    }
}
