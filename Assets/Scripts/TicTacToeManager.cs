using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{

    private int[,] ticTacToeboard;
    public int[,] GetTicTacToeBoard => ticTacToeboard;

    [SerializeField] private int playerID;

    public int PlayerID
    {
        get => playerID;
        set
        {
            playerID = value;
        }
    }

    public string PlayerName;
    public Text textDisplay, IDDisplay;
    InputField chatInputField;
    Button sendButton, resetButtonTrigger, saveReplayButton;

    public delegate void SearchButton(int row, int column, int opponentID);
    public event SearchButton Search;

    public delegate void ResetButton(int row, int column);
    public event ResetButton Reset;

    public delegate void DeactivateBoard(int row, int column);
    public event DeactivateBoard Deactivate;

    public delegate void DelegateTurn(int row, int column, int checkTurn);
    public event DelegateTurn NextTurn;

    [SerializeField] NetworkedClient networkedClient;

    [SerializeField] private int playerTurn;
    public int PlayerTurn
    {
        get => playerTurn;
        set
        {
            playerTurn = value;
        }
    }

    [SerializeField] private bool isReplaying;
    public bool IsReplaying
    {
        get => isReplaying;
        set
        {
            isReplaying = value;
        }
    }

    public Queue<TicTacToePlayAnimation> replayAnimationQueue;
    float delayReplayAnimation;

    /// <summary>
    /// The moment this manager turns on, go through any and all possible items
    /// </summary>
    private void OnEnable()
    {
        // finds the client object for communication to the server
        networkedClient = GameObject.FindObjectOfType<NetworkedClient>();

        InputField[] allInputFields = FindObjectsOfType<InputField>();
        foreach (InputField tempField in allInputFields)
        {
            if (tempField.gameObject.name == "Chat Input Field")
                chatInputField = tempField;
        }

        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button tempButton in allButtons)
        {
            if (tempButton.gameObject.name == "Send Message Button")
            {
                sendButton = tempButton;
            }
            else if (tempButton.gameObject.name == "Reset Game Button")
            {
                resetButtonTrigger = tempButton;
            }
            else if (tempButton.gameObject.name == "Save Replay Button")
            {
                saveReplayButton = tempButton;
            }
        }

        replayAnimationQueue = new Queue<TicTacToePlayAnimation>();

        //});
    }

    void Start()
    {
        // set up the board size
        ticTacToeboard = new int[3, 3];
    }

    // Update is called once per frame
    void Update()
    {
        if (replayAnimationQueue.Count > 0)
        {
            if (delayReplayAnimation <= 0.0f)
            {
               
                TicTacToePlayAnimation currentPlay = replayAnimationQueue.Dequeue();
                ServerPlacePosition(currentPlay.row, currentPlay.column, currentPlay.playerID);

                delayReplayAnimation = 1.0f;
            }
            else
            {
                delayReplayAnimation -= Time.deltaTime;
            }
        }
    }

    public void CheckTurn(int currentTurn)
    {
        for (int i = 0; i < ticTacToeboard.GetLength(0); i++)
        {
            for (int j = 0; j < ticTacToeboard.GetLength(1); j++)
            {
                if (ticTacToeboard[i, j] == 0)
                {
                    NextTurn(i, j, currentTurn);
                }
            }
        }
    }

 
    public void PlacePosition(int row, int column, int currentPlayer)
    {

        ticTacToeboard[row, column] = currentPlayer;

        networkedClient.SendMessageToHost(ClientToServerSignifiers.PlayerAction + "," + row + "," + column + "," + playerID);

        if (CheckWinCondition())
        {
            networkedClient.SendMessageToHost(ClientToServerSignifiers.PlayerWins + "," + playerID);
        }
      
        else
        {
            CheckDraw();
        }
    }

    public void ServerPlacePosition(int row, int column, int opponentPlayer)
    {
      
        ticTacToeboard[row, column] = opponentPlayer;
      
        Search(row, column, opponentPlayer);

        CheckDraw(); 
    }

    /// <returns></returns>
    private bool CheckWinCondition()
    {
        // search all possible win conditions
        if ((ticTacToeboard[0, 0] == playerID && ticTacToeboard[1, 0] == playerID && ticTacToeboard[2, 0] == playerID)
        || (ticTacToeboard[0, 1] == playerID && ticTacToeboard[1, 1] == playerID && ticTacToeboard[2, 1] == playerID)
        || (ticTacToeboard[0, 2] == playerID && ticTacToeboard[1, 2] == playerID && ticTacToeboard[2, 2] == playerID)
        || (ticTacToeboard[0, 0] == playerID && ticTacToeboard[0, 1] == playerID && ticTacToeboard[0, 2] == playerID)
        || (ticTacToeboard[1, 0] == playerID && ticTacToeboard[1, 1] == playerID && ticTacToeboard[1, 2] == playerID)
        || (ticTacToeboard[2, 0] == playerID && ticTacToeboard[2, 1] == playerID && ticTacToeboard[2, 2] == playerID)
        || (ticTacToeboard[0, 0] == playerID && ticTacToeboard[1, 1] == playerID && ticTacToeboard[2, 2] == playerID)
        || (ticTacToeboard[2, 0] == playerID && ticTacToeboard[1, 1] == playerID && ticTacToeboard[0, 2] == playerID))
        {
            //Debug.Log("Player " + playerID + " wins");
            textDisplay.text = PlayerName + " wins";
            return true;
        }
        return false;
    }

 

    /// <summary>
    /// Checks for the draw
    /// </summary>
    public void CheckDraw()
    {
       
        playerTurn++;
     
    }


    
    public void ResetGame()
    {
        ResetButtons();

        playerTurn = 1; 
        resetButtonTrigger.gameObject.SetActive(false);
    }

    public void ResetButtons()
    {
        for (int i = 0; i < ticTacToeboard.GetLength(0); i++)
        {
            for (int j = 0; j < ticTacToeboard.GetLength(1); j++)
            {
                ticTacToeboard[i, j] = 0;
                Reset(i, j);
            }
        }
    }

    public void GameOverOnWin()
    {
        for (int i = 0; i < ticTacToeboard.GetLength(0); i++)
        {
            for (int j = 0; j < ticTacToeboard.GetLength(1); j++)
            {
                if (ticTacToeboard[i, j] == 0)
                {
                    Deactivate(i, j);
                }
            }
        }
    }





   

   
    private void SendChatMessage()
    {
        networkedClient.SendMessageToHost(ClientToServerSignifiers.SendPresetMessage + "," + chatInputField.text);
    }

    public void ReceiveMessage(string message)
    {
        textDisplay.text = message;
    }

}

public class TicTacToePlayAnimation
{
    public int row;
    public int column;
    public int playerID;

    public TicTacToePlayAnimation(int in_Row, int in_Column, int in_PlayerID)
    {
        row = in_Row;
        column = in_Column;
        playerID = in_PlayerID;
    }
}
