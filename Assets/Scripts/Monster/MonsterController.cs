using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    [HideInInspector]
    public float originalSpeed;
    public Vector3 direction = Vector3.left;
    // might be useless
    int health;
    Transform characterTransform;
    GameObject GM;
    void Start()
    {
        originalSpeed = speed;
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
        
        if (GM.GetComponent<PollutionManager>().pollutionLevel > 1)
        {
            speed = speed * (GM.GetComponent<PollutionManager>().pollutionLevel) / 1.5f;
        }
        characterTransform.Translate((direction * Time.deltaTime) * speed);
    }
}
