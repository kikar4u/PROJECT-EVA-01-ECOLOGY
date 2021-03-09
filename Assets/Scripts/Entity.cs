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

    /*    public enum directionToGoFor
        {
            none = 0,
            up = 1,
            down = 2

        }*/
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
    public void decalMob(GameObject other, string directionToGo)
    {
        if(directionToGo =="up")
        {

          //StartCoroutine(directionChange(other, Vector3.up));
          other.gameObject.GetComponent<MonsterController>().direction = Vector3.up;

        }
        else if(directionToGo == "down")
        {
          other.gameObject.GetComponent<MonsterController>().direction = Vector3.down;
        }
        else
        {
          other.gameObject.GetComponent<MonsterController>().direction = Vector3.left;
        }
    }
    IEnumerator directionChange(GameObject other, Vector3 direction)
    {
        other.gameObject.GetComponent<MonsterController>().direction = direction;
        yield return new WaitForSeconds(2.0f);
        //other.gameObject.GetComponent<MonsterController>().direction = Vector3.left;
    }
}