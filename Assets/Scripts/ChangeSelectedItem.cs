using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSelectedItem : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void objectCard(GameObject entityToSpawn){
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().objectToSpawn = entityToSpawn;
    }
}
