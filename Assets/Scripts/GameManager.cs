using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    GameBoard board = default;
    [SerializeField] LayerMask lMask;
   
    public GameObject objectToSpawn;

    [SerializeField]
    Camera currentCamera;
    RaycastHit hit;
    void Awake()
    {
        board.Initialize(boardSize);
        lMask = ~lMask;
    }
    void OnValidate()
    {
        if (boardSize.x < 2)
        {
            boardSize.x = 2;
        }
        if (boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            spawnEntity(objectToSpawn);
        }
        //Debug.Log(Input.mousePosition);

    }
    public void spawnEntity(GameObject a)
    {

        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);


        mousePos.z = currentCamera.transform.position.y - 0.5f;       // we want 2m away from the camera position
        Vector3 objectPos = currentCamera.ScreenToWorldPoint(mousePos);
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);


        //Debug.DrawRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y,currentCamera.gameObject.transform.position.y), transform.TransformDirection(Vector3.down) *150, Color.yellow);
        //Debug.Log(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentCamera.gameObject.transform.position.y));
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("pan" + objectHit);
            if(hit.transform.gameObject.name == "UIBounds")
            {
                
                Debug.Log("touch UI");
            }
            // Do something with the object that was hit by the raycast.
        }
        else
        {

                Instantiate(a, objectPos, Quaternion.identity);
            
        }



    }
}
