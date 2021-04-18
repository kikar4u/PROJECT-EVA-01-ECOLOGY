using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void reloadActiveScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene.ToString());
    }
}
