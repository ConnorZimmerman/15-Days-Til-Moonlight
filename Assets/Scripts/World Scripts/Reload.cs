using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Reload : MonoBehaviour
{

    private CameraController theCamera;

    public Vector2 startDirection;

    public string pointName;

    public PlayerHealthManager playerHealth;

    public PlayerController thePlayer;

    private DialogueManager theDM;

    public float waitToReload;

    private static bool reloadExists;

    public bool reloadIs;

    public PlayerStaminaManager staminaMan;

    public GameObject playerObject;

    void Start()

    {
        thePlayer = FindObjectOfType<PlayerController>();

        theDM = FindObjectOfType<DialogueManager>();

        playerObject = GameObject.Find("Player");
        playerHealth = playerObject.GetComponent<PlayerHealthManager>();

        if (!reloadExists)
        {
            reloadExists = true;
           // DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            //Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        reloadIs = false;

        if (playerHealth.playerCurrentHealth <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            waitToReload -= Time.deltaTime;
            reloadIs = true;

            thePlayer.lastMove = new Vector2(0, -1f);

            if (waitToReload <= 0)
            {
                playerHealth.playerIsDead = false;

                thePlayer.swingBig.SetActive(false);
                thePlayer.swingBig.transform.localRotation = new Quaternion(0, 0, 0, 0);

                staminaMan.playerCurrentStamina = staminaMan.playerMaxStamina;

                playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;

                playerHealth.oldPlayerCurrentHealth = playerHealth.playerCurrentHealth;

                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //SceneManager.LoadScene(SceneManager.SetActiveScene(1).buildIndex);
                //SceneManager.LoadScene(0, LoadSceneMode.Single);


                playerHealth.gameObject.SetActive(true);

                
                waitToReload = 2;

                if (thePlayer.startPoint == pointName)
                {
                    thePlayer.transform.position = transform.position;
                    theDM.dialogActive = false;
                    theDM.dBox.SetActive(false);
                    thePlayer.canMove = true;

                    theCamera = FindObjectOfType<CameraController>();
                    theCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                        theCamera.transform.position.z);
                }
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Global Music Tracker", 0);
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
            }
        }
    }
}
