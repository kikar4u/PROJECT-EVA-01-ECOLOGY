using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float timeLeft = 3.0f;
    //public Text startText; //used for showing countdown from 3,2,1 
    GameObject ville;
    public bool isStarted = false;
    [Header("Timer Attributes")]
    [SerializeField]
    public float timeInSeconds;
    GameObject timer;
    private Text population;
    GameObject overlayObject;
    Vector3 rotationForObjecttoSpawn = new Vector3(0.0f,0.0f,0.0f);
    GameObject gameOver;
    GameObject winUI;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //board.Initialize(boardSize);
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

 public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("prpsepperppepepepeppepepepepepep");
        currentCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        timer = GameObject.Find("Timer");
        ville = GameObject.Find("Ville");
        population = GameObject.Find("Population").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver");
        winUI = GameObject.Find("WIN");
        winUI.SetActive(false);
        gameOver.SetActive(false);
        isStarted = false;
        population.text = "Population : " + ville.GetComponent<JaugePopulation>().nbrPopulation;
        timeInSeconds = GameObject.Find("Timer").GetComponent<TimeInSeconds>().timeInSeconds;
        timer.GetComponent<Slider>().maxValue = timeInSeconds;
        timer.GetComponent<Slider>().value = timeInSeconds;

        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            overlayObject = Instantiate(objectToSpawn, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

/*        population.text = "Population : " + ville.GetComponent<JaugePopulation>().nbrPopulation;
        timer.text = timeInSeconds + "";*/


        //overlayObject.GetComponentInChildren<PolygonCollider2D>().enabled = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "MainMenu" && currentCamera != null)
        {
            //Debug.Log("overlayobjet" + overlayObject);
            overlayObject.transform.position = new Vector3(currentCamera.ScreenToWorldPoint(temp).x, currentCamera.ScreenToWorldPoint(temp).y, 0.0f);
            overlayObject.GetComponentInChildren<SpriteRenderer>().sprite = objectToSpawn.GetComponentInChildren<SpriteRenderer>().sprite;
        }
        //timeLeft -= Time.deltaTime % 60;
        //Debug.Log(timeLeft);
        //startText.text = (timeLeft).ToString("0");
        if (isStarted)
        {
            launchTimer();
            updatePopulation();
            if (ville.GetComponent<JaugePopulation>().nbrPopulation <= 0)
            {
                Time.timeScale = 0;
                Debug.Log("GAME OVER");
                gameOver.SetActive(true);

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !isStarted && SceneManager.GetActiveScene().name != "MainMenu")
            {
                spawnEntity(objectToSpawn);
            }
            if(Input.GetButtonDown("Fire2") && !isStarted)
            {
                if(rotationForObjecttoSpawn.z == 360)
                {
                    rotationForObjecttoSpawn.z = 0;
                }
                rotationForObjecttoSpawn.z += 90;
                //Debug.Log("rotation object to spawn" + rotationForObjecttoSpawn);
                overlayObject.transform.Rotate(rotationForObjecttoSpawn, Space.World);
            }
        }


        //Ray ray = currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z * 10));
        

        //Debug.Log(Input.mousePosition);

    }
    public void updatePopulation()
    {
        population.text = "Population : " + ville.GetComponent<JaugePopulation>().nbrPopulation;
    }
    public void launchGame()
    {
        isStarted = true;
    }
    public void restartGame()
    {
        GameObject.FindGameObjectWithTag("LoadManager").GetComponent<SceneLoader>().reloadActiveScene();
    }
    public void spawnEntity(GameObject a)
    {

        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);

        if(currentCamera != null)
        {

        mousePos.z = -currentCamera.transform.position.z;       // we want 2m away from the camera position
        Vector3 objectPos = currentCamera.ScreenToWorldPoint(mousePos);
        Ray ray = currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z * 10));
        RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        bool isThereEntity = false;
        bool isThereUI = false;
        if(hit2.collider != null)
        {   
            if(hit2.transform.gameObject.tag == "Entity")
            {
                //Debug.Log(hit2.transform.gameObject.name);
                isThereEntity = true;
            }
            else
            {
                isThereEntity = false;
            }

        }
        if(Physics.Raycast(ray, out hit))
        {
                Transform objectHit = hit.transform;


                if (hit.transform.gameObject.name == "UIBounds")
                {
                    // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
                    Debug.Log("touch UI / entity");
                    isThereUI = true;
                }
                else
                {
                    isThereUI = false;
                }
                // Do something with the object that was hit by the raycast.
        }
        if (isThereEntity == false && isThereUI == false)
        {

            //Debug.Log("devrait spawn");
            GameObject spawnedObject = Instantiate(a, objectPos, Quaternion.identity);
            spawnedObject.transform.Rotate(rotationForObjecttoSpawn, Space.World);

        }

        }



    }
    public void launchTimer()
    {
        timeInSeconds -= Time.deltaTime % 60;
        float timeLeft = Mathf.RoundToInt(timeInSeconds);

        if (timeInSeconds <= 0)
        {
            Debug.Log("Fin du niveau");
            Time.timeScale = 0;
            winUI.SetActive(true);
            //Do finish timer
        }
        else
        {
            timer.GetComponent<Slider>().value = timeLeft;
            //Debug.Log(timeInSeconds);
        }
    }
}
