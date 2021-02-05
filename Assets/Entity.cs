using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    int speedMalus;
    // Start is called before the first frame update
    public void changeSpeed()
    {

        // change speed of ennemy
        Debug.Log("l'ennemi n'a plus qu'une vitesse de " + speedMalus);
    }
}
