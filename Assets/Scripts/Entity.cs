using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    float speedMalus;
    float speedAtEnter;
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
            speedAtEnter = other.gameObject.GetComponent<MonsterController>().originalSpeed;
            if(other.gameObject.GetComponent<MonsterController>().speed > 0.2f){
                other.gameObject.GetComponent<MonsterController>().speed -= speedMalus;
            }
            else if(other.gameObject.GetComponent<MonsterController>().speed < 0.2f){
                other.gameObject.GetComponent<MonsterController>().speed = 0.2f;
            }
            
        }
        
        // change speed of ennemy
        //Debug.Log("l'ennemi n'a plus qu'une vitesse de " + speedMalus);
    }

    public void initSpeed(GameObject other)
    {
        if (other.gameObject.GetComponent<MonsterController>() != null)
        {
            other.gameObject.GetComponent<MonsterController>().speed = speedAtEnter;
            //Debug.Log("HELLO WORLD JE RETOURNE A MA VITESSE DE BASE");
        }
    }
}