using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    [SerializeField]
    GameBoard board = default;
    [SerializeField]
    GameObject objectToSpawn;

    [SerializeField]
    Camera currentCamera;

    void Awake()
    {
        board.Initialize(boardSize);

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
    }
    public void spawnEntity(GameObject a)
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log("position du sol" + board.transform.position.y);
        mousePos.z = currentCamera.transform.position.y - 0.5f;       // we want 2m away from the camera position
        Vector3 objectPos = currentCamera.ScreenToWorldPoint(mousePos);
        Instantiate(a, objectPos, Quaternion.identity);
    }
}
