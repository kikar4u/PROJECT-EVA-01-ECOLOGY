using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed;
    // might be useless
    int health;
    Transform characterTransform;
    void Start()
    {
        characterTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        moving(speed);
    }
    void moving(float speed)
    {
        characterTransform.Translate((Vector3.up * Time.deltaTime) * speed);
    }
}
