using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    float speedMalus;
    float speedAtEnter;
    [SerializeField]
    float pollution;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().tmpPollutionLevel += pollution;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void changeSpeed(GameObject other)
    {
        // gestion du changement de vitesse suivant l'objet touché
        if(other.gameObject.GetComponent<MonsterController>() != null)
        {
            // on stock la vitesse originale pour éviter de rajouter de la vitesse a une vitesse déjà modifiée
            speedAtEnter = other.gameObject.GetComponent<MonsterController>().originalSpeed;
            if(other.gameObject.GetComponent<MonsterController>().speed < 0.6f)
            {

                other.gameObject.GetComponent<MonsterController>().speed += 0.6f;

            }
            else
            {
                other.gameObject.GetComponent<MonsterController>().speed -= speedMalus;
                

            }
            
        }
    }

    public void initSpeed(GameObject other)
    {
        if (other.gameObject.GetComponent<MonsterController>() != null)
        {
            // réinitialisation de la vitesse
            other.gameObject.GetComponent<MonsterController>().speed = speedAtEnter;

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
    }
}