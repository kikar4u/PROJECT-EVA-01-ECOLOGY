using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollider : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("change la vitesse du monstre");
        // change speed of ennemy
        entity.changeSpeed(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // change speed of ennemy
        entity.initSpeed(other.gameObject);
    }
}
