using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Stop
    }


    public string puzzleState = string.Empty;
    public float elapsedTime = 0.0f;
    public GameState gameState = GameState.Stop;
    public int moves = 0;
    private int[,] board = new int[3, 3];
    private GameObject[] tiles = new GameObject[9];
    public GameObject emptyTile = null;

    // Input Fields
    [Header("Input Fields")]
    public InputField puzzleStateInputField = null;

    // Text
    [Header("Text")]
    public Text timer = null;
    public Text movesCounter = null;
    public Text isSolvable = null;

    // Buttons
    [Header("Buttons")]
    public Button startButton = null;
    public Button stopButton = null;
    
    // Use this for initialization
    void Start ()
    {
        for(int i = 0; i < tiles.Length; i++)
            tiles[i] = GameObject.Find(i.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(gameState == GameState.Playing)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if(!hit.transform.gameObject.name.Equals("Empty Tile"))
                {
                    moves++;
                    movesCounter.text = moves.ToString();
                    MoveTile(hit.transform.gameObject);
                }
            }
        }
    }

    public void SolveButtonClick()
    {
        if (EightPuzzle.IsPuzzleValid(puzzleStateInputField.text))
        {
            Debug.Log("Puzzle is valid!");

            if (EightPuzzle.IsSolvable(puzzleStateInputField.text))
            {
                isSolvable.text = "Yes";
                EightPuzzle.Solve();
            }
            else
                isSolvable.text = "No";

        }
        else
            Debug.Log("Puzzle is not valid!");

    }

    public void SetBoard()
    {
        string puzzleState = puzzleStateInputField.text;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (puzzleState[i] != '0')
            {
                tiles[i].GetComponent<SpriteRenderer>().sprite
                = Resources.Load<Sprite>("Sprites/" + puzzleState[i].ToString());
            }
            else
            {
                tiles[i].GetComponent<SpriteRenderer>().sprite = null;
                emptyTile = tiles[i];
            }
        }
    }

    //private int[] ParsePuzzleState(string puzzleState)
    //{
    //    int[] puzzleStateArray = new int[puzzleState.Length];

    //    for (int i = 0; i < puzzleState.Length; i++)
    //        puzzleStateArray[i] = int.Parse(puzzleState[i].ToString());

    //    return puzzleStateArray;
    //}

    public void StartGame()
    {
        gameState = GameState.Playing;
        startButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);

        SetBoard();
        AStar aStar = new AStar(puzzleStateInputField.text, "012345678");
        aStar.Search(1);
    }

    public void StopGame()
    {
        gameState = GameState.Stop;
        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
    }

    private void MoveTile(GameObject clickedTile)
    {
        Vector3 temporaryPosition = clickedTile.transform.position;
        clickedTile.transform.position = emptyTile.transform.position;
        emptyTile.transform.position = temporaryPosition;
    }
}
