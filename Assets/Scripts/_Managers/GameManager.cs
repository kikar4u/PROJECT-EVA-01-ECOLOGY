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
    public float tmpPollutionLevel;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //board.Initialize(boardSize);
        lMask = ~lMask;

    }
    // no longer used
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
      GameObject.FindGameObjectWithTag("GameController").GetComponent<PollutionManager>().pollutionLevel += tmpPollutionLevel;
      Time.timeScale = 1;
      if (SceneManager.GetActiveScene().name != "MainMenu")
      {
        overlayObject = Instantiate(objectToSpawn, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
      }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // récupérer la scène chargée
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        // si on est sur une scène différente du mainmenu, et qu'on a récupérer correctement la caméra, alors on fait apparaître le "bâtiment curseur" sur la souris
        if (SceneManager.GetActiveScene().name != "MainMenu" && currentCamera != null)
        {
            overlayObject.transform.position = new Vector3(currentCamera.ScreenToWorldPoint(temp).x, currentCamera.ScreenToWorldPoint(temp).y, 0.0f);
            // permet de changer de sprite
            overlayObject.GetComponentInChildren<SpriteRenderer>().sprite = objectToSpawn.GetComponentInChildren<SpriteRenderer>().sprite;
            // on désactive l'audiosource pour éviter le FX d'apparition onstart()
            overlayObject.gameObject.GetComponent<AudioSource>().enabled = false;
        }
        
        if (isStarted)
        {
            launchTimer();
            updatePopulation();
            overlayObject.SetActive(false);
            
            if (ville.GetComponent<JaugePopulation>().nbrPopulation <= 0)
            {
                Time.timeScale = 0;
                Debug.Log("GAME OVER");
                // apparition de l'UI game over
                gameOver.SetActive(true);

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !isStarted && SceneManager.GetActiveScene().name != "MainMenu")
            {
                spawnEntity(objectToSpawn);
            }
            // ANNULE, Fonctionne mais rotation parfois inexacte entre l'UI et l'objet apparu => mauvais feedback
/*            if(Input.GetButtonDown("Fire2") && !isStarted)
            {
                if (rotationForObjecttoSpawn.z == 180)
                {
                    rotationForObjecttoSpawn.z = 0;
                }
                else
                {
                    rotationForObjecttoSpawn.z += 90;
                }
                
                //Debug.Log("rotation object to spawn" + rotationForObjecttoSpawn);
                overlayObject.transform.Rotate(rotationForObjecttoSpawn, Space.World);
            }*/
        }
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            restartGame();
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
        // on réinitialise le cache du niveau de pollution pour éviter d'augmenter la difficulté alors que c'est le même niveau
        tmpPollutionLevel = 0;
        this.GetComponent<PollutionManager>().pollutionLevel = this.GetComponent<PollutionManager>().pollutionLevel; 
    }
    public void spawnEntity(GameObject a)
    {

        Vector3 mousePos = Input.mousePosition;

        if(currentCamera != null)
        {

            // récupération de la position du curseur et manipulation de l'axe z pour que ce soit devant la caméra
            mousePos.z = -currentCamera.transform.position.z;       
            Vector3 objectPos = currentCamera.ScreenToWorldPoint(mousePos);
            Ray ray = currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z * 10));
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            bool isThereEntity = false;
            bool isThereUI = false;
            if(hit2.collider != null)
            {   
                if(hit2.transform.gameObject.tag == "Entity")
                {
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
                    // on vérifie que l'objet ne spawnerai pas sur l'UI

                    if (hit.transform.gameObject.name == "UIBounds")
                    {
                        
                        Debug.Log("touch UI / entity");
                        isThereUI = true;
                    }
                    else
                    {
                        isThereUI = false;
                    }
            }
            if (isThereEntity == false && isThereUI == false)
            {   
                // si tout est bon, on spawn l'objet, met à jour son collider et on lui applique la rotation voulue
                GameObject spawnedObject = Instantiate(a, objectPos, Quaternion.identity);
                spawnedObject.GetComponentInChildren<PolygonCollider2D>().enabled = true;
                Debug.Log(spawnedObject.GetComponentInChildren<PolygonCollider2D>().enabled);
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
            Time.timeScale = 0;
            winUI.SetActive(true);
            // fin du niveau, win
        }
        else
        {
            timer.GetComponent<Slider>().value = timeLeft;
            // sinon on modifie le slider qui fait office de timer
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
