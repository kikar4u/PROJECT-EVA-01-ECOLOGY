using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame  
    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().isStarted = true;
    }
    public void restartGame()
    {
        gameManager.GetComponent<GameManager>().restartGame();
    }
}
