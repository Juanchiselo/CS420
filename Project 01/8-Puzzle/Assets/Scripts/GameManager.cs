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

    public GameObject emptyTile = null;

    // Input Fields
    [Header("Input Fields")]
    public InputField puzzleStateInputField = null;

    // Text
    [Header("Text")]
    public Text timer = null;
    public Text movesCounter = null;

    // Buttons
    [Header("Buttons")]
    public Button startButton = null;
    public Button stopButton = null;

    // Use this for initialization
    void Start ()
    {
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
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

    public void SolveButtonClick()
    {
        //puzzleStateInputField = GameObject.Find("Puzzle State").GetComponent<InputField>();


        if (EightPuzzle.IsPuzzleValid(puzzleStateInputField.text))
        {
            Debug.Log("Puzzle is valid!");

            if (EightPuzzle.IsSolvable(puzzleStateInputField.text))
                Debug.Log("Puzzle is solvable!");
            else
                Debug.Log("Puzzle is not solvable!");

        }
        else
            Debug.Log("Puzzle is not valid!");

    }

    public void StartGame()
    {
        gameState = GameState.Playing;
        startButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        gameState = GameState.Stop;
        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
    }

    private void MoveTile(GameObject clickedTile)
    {
        Debug.Log("X: " + clickedTile.gameObject.transform.position.x
            + " - Y: " + clickedTile.gameObject.transform.position.y);
        Debug.Log("eX: " + emptyTile.gameObject.transform.position.x
            + " - eY: " + emptyTile.gameObject.transform.position.y);
        Debug.Log("Modified x: " + (clickedTile.gameObject.transform.position.x - 1.6).ToString());

        if (emptyTile.transform.position.x == (clickedTile.gameObject.transform.position.x - 1.6)
            && emptyTile.transform.position.y == clickedTile.gameObject.transform.position.y)
        {
            Debug.Log("X: " + clickedTile.gameObject.transform.position.x
            + " - Y: " + clickedTile.gameObject.transform.position.y);
            Debug.Log("Clicked tile is on the left.");
        }
        //else if (clickedTile.gameObject.transform.position.x - 1.6 == emptyTile.transform.position.x)
        //{
        //    Debug.Log("X: " + clickedTile.gameObject.transform.position.x
        //    + " - Y: " + clickedTile.gameObject.transform.position.y);
        //    Debug.Log("Clicked tile is on the right.");
        //}
        //else if (clickedTile.gameObject.transform.position.y - 1.6 == emptyTile.transform.position.y)
        //{
        //    Debug.Log("X: " + clickedTile.gameObject.transform.position.x
        //    + " - Y: " + clickedTile.gameObject.transform.position.y);
        //    Debug.Log("Clicked tile is above.");
        //}
        //else if (clickedTile.gameObject.transform.position.y - 1.6 == emptyTile.transform.position.y)
        //{
        //    Debug.Log("X: " + clickedTile.gameObject.transform.position.x
        //    + " - Y: " + clickedTile.gameObject.transform.position.y);
        //    Debug.Log("Clicked tile is below.");
        //}
    }
}
