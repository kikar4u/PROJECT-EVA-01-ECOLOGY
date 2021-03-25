using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour
{
    public GameObject PanelEntity;
    Vector3 inputMouse;
    // Start is called before the first frame update
    void Start()
    {
        PanelEntity.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onHoverElement()
    {
        PanelEntity.SetActive(true);
    }
    public void onExitElement()
    {
        PanelEntity.SetActive(false);
    }
}
