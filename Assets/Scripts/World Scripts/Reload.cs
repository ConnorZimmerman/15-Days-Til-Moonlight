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
    private LoadNewArea loadNewAreaScript;
    public float waitToReload;
    private static bool reloadExists;
    public bool reloadIs;
    public PlayerStaminaManager staminaMan;
    public GameObject playerObject;
    private GlobalDataScript globalData;
    private ItemSlotManager itemSlotManagerScript;

    void Start()

    {
        thePlayer = FindObjectOfType<PlayerController>();
        loadNewAreaScript = FindObjectOfType<LoadNewArea>();

        theDM = FindObjectOfType<DialogueManager>();

        playerObject = GameObject.Find("Player");
        itemSlotManagerScript = FindObjectOfType<ItemSlotManager>();

        playerHealth = playerObject.GetComponent<PlayerHealthManager>();
        globalData = FindObjectOfType<GlobalDataScript>();

        if (!reloadExists)
        {
            reloadExists = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        reloadIs = false;

        if (playerHealth.playerCurrentHealth <= 0)
        {
            // itemSlotManagerScript.InventoryReset();
            globalData.Save(itemSlotManagerScript.listOfSlots, itemSlotManagerScript.equippedArmor);
            // loadNewAreaScript.SetAllForLvl();

            waitToReload -= Time.deltaTime;
            reloadIs = true;

            thePlayer.lastMove = new Vector2(0, -1f);

            if (waitToReload <= 0)
            {
                playerHealth.playerIsDead = false;
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Global Music Tracker", 0);
                // PlayerPrefs.SetString("Global Player Cur Lvl", "SnowyA");
                // PlayerPrefs.SetString("Global Player Start Point", "SnowyA_StartPoint");
                PlayerPrefs.SetString("Global Player Cur Lvl", "Sewers_A");
                PlayerPrefs.SetString("Global Player Start Point", "Sewers_A_StartPoint");

                thePlayer.swingBig.SetActive(false);
                thePlayer.swingBig.transform.localRotation = new Quaternion(0, 0, 0, 0);

                staminaMan.playerCurrentStamina = staminaMan.playerMaxStamina;

                playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;

                playerHealth.oldPlayerCurrentHealth = playerHealth.playerCurrentHealth;


                waitToReload = 2;

                // Debug.Log(globalData.globalPlayerCurLvl);
                // Debug.Log(globalData.globalPlayerStartPoint);
                // SceneManager.LoadScene("SnowyA", LoadSceneMode.Single);
                SceneManager.LoadScene("Sewers_A", LoadSceneMode.Single);

                // if (thePlayer.startPoint == pointName)
                // {

                // thePlayer.transform.position = GameObject.Find("SnowyA_Startpoint").transform.position;
                theDM.dialogActive = false;
                theDM.dBox.SetActive(false);
                thePlayer.canMove = true;

                playerHealth.gameObject.SetActive(true);

                theCamera = FindObjectOfType<CameraController>();
                theCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                    theCamera.transform.position.z);
                // }
            }
        }
    }
}
