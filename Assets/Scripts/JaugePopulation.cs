﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugePopulation : MonoBehaviour
{

    [SerializeField]
    public int nbrPopulation = 5;
    public bool isInTown = false;
    float timeInSeconds;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        nbrPopulation = nbrPopulation;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {

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
