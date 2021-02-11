using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float timeInSeconds;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //launchTimer();
    }
    public void launchTimer()
    {
        timeInSeconds -= Time.deltaTime % 60;
        //Debug.Log(timeInSeconds);
    }
}
