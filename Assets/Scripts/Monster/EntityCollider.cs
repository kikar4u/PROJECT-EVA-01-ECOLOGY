using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollider : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    [SerializeField]
    bool decalMob = false;
    [SerializeField]
    string directionToGo = "up";
    private void OnTriggerEnter2D(Collider2D other)
    {
        //changement de direction du mob après avoir touché le bon type de bâtiment
        if (decalMob)
        {

            entity.decalMob(other.gameObject, directionToGo);
            
        }
        else
        {
            entity.changeSpeed(other.gameObject);

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            // change speed of ennemy
            if (!decalMob)
            {
                entity.initSpeed(other.gameObject);
            }

            entity.decalMob(other.gameObject, "left");
        }

    }
}
