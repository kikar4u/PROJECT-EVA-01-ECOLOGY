using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    GameObject gameManager;
    GameObject sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        sceneLoader = GameObject.Find("SceneLoader");
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
    public void getToNextLevel(string sceneName)
    {
        sceneLoader.GetComponent<SceneLoader>().nextLevel(sceneName);
    }
}
