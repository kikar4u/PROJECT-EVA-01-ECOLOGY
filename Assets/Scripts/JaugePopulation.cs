using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugePopulation : MonoBehaviour
{
    [SerializeField]
    public int nbrPopulation = 100;
    public bool isInTown = false;
    float timeInSeconds;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("OH NON LESM ONSTRES SONT DANS LA VILLE ! ");
            isInTown = true;
            StartCoroutine(decrease());

        }
    }
    public void decreasePop()
    {
        nbrPopulation--;
        Debug.Log(nbrPopulation);
    }
    IEnumerator decrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            decreasePop();
        }
    }
    void OutputTime()
    {
        Debug.Log(Time.time);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
