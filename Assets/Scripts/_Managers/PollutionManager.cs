using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float pollutionLevel;
    void Start()
    {
        pollutionLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("level de pollution" + pollutionLevel);
    }
}
