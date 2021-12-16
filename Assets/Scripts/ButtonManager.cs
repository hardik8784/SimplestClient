using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private int XPos;
    [SerializeField] private int YPos;

    private Button ButtonConnected;

    TicTacToeManager ticTacToeManagerRef;

    void Start()
    {
        ButtonConnected = GetComponent<Button>();
        ButtonConnected.onClick.AddListener(OnButtonClicked);

        ticTacToeManagerRef = FindObjectOfType<TicTacToeManager>();

        ticTacToeManagerRef.Search += SetButtonAtLocation;
        ticTacToeManagerRef.Reset += ReactivateButtonOnReset;
        ticTacToeManagerRef.NextTurn += ButtonOnTurnChange;
        ticTacToeManagerRef.Deactivate += DeactivateButton;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnButtonClicked()
    {
        Debug.Log(XPos + "," + YPos);
       
        if (!CheckIfOccupied())
        {
            SetButtonAtLocation(XPos, YPos, ticTacToeManagerRef.PlayerID);

            ticTacToeManagerRef.PlacePosition(XPos, YPos, ticTacToeManagerRef.PlayerID);
        }
    }

    private void SetButtonAtLocation(int row, int column, int playerID)
    {
        if (row == XPos && column == YPos)
        {
            if (playerID == 1)
            {
                ButtonConnected.transform.GetChild(0).GetComponent<Text>().text = "O";
            }
            else if (playerID == 2)
            {
                ButtonConnected.transform.GetChild(0).GetComponent<Text>().text = "X";
            }
            ButtonConnected.interactable = false;
        }
    }

    private bool CheckIfOccupied()
    {
        if (ticTacToeManagerRef.GetTicTacToeBoard[XPos, YPos] >= 1)
        {
            ButtonConnected.interactable = false;
            return true;
        }
        return false;
    }

    public void ReactivateButtonOnReset(int row, int column)
    {
        if (row == XPos && column == YPos)
        {
            ButtonConnected.transform.GetChild(0).GetComponent<Text>().text = ""; // test
            if (ticTacToeManagerRef.PlayerID <= 2)
            {
                ButtonConnected.interactable = true;
            }
        }
    }

    public void ButtonOnTurnChange(int row, int column, int turnArgument)
    {
        if (row == XPos && column == YPos)
        {
            if (ticTacToeManagerRef.PlayerID == turnArgument)
            {
                ButtonConnected.interactable = true;
            }
            else
            {
                ButtonConnected.interactable = false;
            }
        }
    }

    private void DeactivateButton(int row, int column)
    {
        ButtonConnected.interactable = false;
    }
}
