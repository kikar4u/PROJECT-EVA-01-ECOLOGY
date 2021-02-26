using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    float speedMalus;
    [SerializeField]
    int pollution;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PollutionManager>().pollutionLevel += pollution;
    }
    public void changeSpeed(GameObject other)
    {

        if(other.gameObject.GetComponent<MonsterController>() != null)
        {
            other.gameObject.GetComponent<MonsterController>().speed -= speedMalus;
        }
        
        // change speed of ennemy
        //Debug.Log("l'ennemi n'a plus qu'une vitesse de " + speedMalus);
    }

    public void initSpeed(GameObject other)
    {
        if (other.gameObject.GetComponent<MonsterController>() != null)
        {
            other.gameObject.GetComponent<MonsterController>().speed += speedMalus;
            //Debug.Log("HELLO WORLD JE RETOURNE A MA VITESSE DE BASE");
        }
    }
}