/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 14,2021
 * File : TicTacToeManager.cs
 * Description : This is the script to Assignthe Ids and Open the Container when player collide with the help of OnTriggerEvent
 * Revision History : v0.1 > Added Comments to know the Code better before start anything & to include a program header
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{

    // Start is called before the first frame update // tic tac toe UI
    //
    //[0,0] [0,1] [0,2]
    //[1,0] [1,1] [1,2]
    //[2,0] [2,1] [2,2]
    //
    private int[,] ticTacToeboard;
    public int[,] GetTicTacToeBoard => ticTacToeboard;

    public string PlayerName;
    public Text ShowChat, PlayerIDText;
    InputField InputFieldChat;
    Button SendButton;

    // player variables
    // if current turn is not equal to the player ID, then it's not the player's turn to move
    [SerializeField] private int playerID;

    public int PlayerID
    {
        get => playerID;
        set
        {
            playerID = value;
        }
    }

  
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
