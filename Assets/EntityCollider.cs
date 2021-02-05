using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollider : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    private void OnTriggerEnter(Collider other)
    {
        // change speed of ennemy
        entity.changeSpeed();
    }
    private void OnTriggerExit(Collider other)
    {
        // change speed of ennemy
    }
}
