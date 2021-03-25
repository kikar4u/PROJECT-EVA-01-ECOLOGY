using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSelectedItem : MonoBehaviour
{
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void objectCard(GameObject entityToSpawn){
        
        gameManager.GetComponent<GameManager>().objectToSpawn = entityToSpawn;

        entityToSpawn.GetComponentInChildren<PolygonCollider2D>().enabled = false;
        Debug.Log(entityToSpawn.GetComponentInChildren<PolygonCollider2D>().enabled);
        
    }
}
