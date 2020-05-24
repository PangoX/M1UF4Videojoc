using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, Ended } // Game States

public class GameController : MonoBehaviour
{
    [Range(0f,0.25f)] // Rang Speed
    public float parallaxSpeed = 0.02f; 
    public RawImage background;
    public GameObject uiIdle;

    public GameState gameState = GameState.Idle;
    public GameObject player; // Player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown(KeyCode.Space); // Restart Game

        // Start Game

        if (gameState == GameState.Idle && userAction){ //State Static + Swap States
            gameState = GameState.Playing;

            uiIdle.SetActive(false); // Hide Instructions
            player.SendMessage("UpdateState", "Player-Walk"); // State GameController
        }
        else if (gameState == GameState.Playing) // Effect Background
        {
            Parallax();
        }
        else if (gameState == GameState.Ended) // Restart Game
        {
            if (userAction)
            {
                RestartGame();
            }
        }
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;  // Variable Movement
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f); // Suma movement
    }

    public void RestartGame() // Restart
    {
        SceneManager.LoadScene("base");
    }
}
