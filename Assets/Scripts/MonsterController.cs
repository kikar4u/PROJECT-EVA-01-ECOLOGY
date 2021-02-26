using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    // might be useless
    int health;
    Transform characterTransform;
    GameObject GM;
    void Start()
    {
        characterTransform = gameObject.GetComponent<Transform>();
        GM = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.GetComponent<GameManager>().isStarted)
        {
            moving(speed);
        }

    }
    void moving(float speed)
    {
        characterTransform.Translate((Vector3.left * Time.deltaTime) * speed);
    }
}
