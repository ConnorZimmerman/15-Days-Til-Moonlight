using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthbar;
    public Slider staminaBar;
    public Text HPText;
    public PlayerHealthManager playerHealth;
    public PlayerStaminaManager playerStamina;
    private PlayerRangedAttack playerRanged;
    private ShieldBlock shieldBlockScript;
    private PlayerStats playerStatsScript;
    private static bool UIExists;
    public GameObject daggerImageObject;
    public PlayerStats thePS;
    private GameObject daggerTextObject;
    private GameObject shieldTextObject;
    private GameObject shieldImageObject;
    public Text daggerText;
    public Text shieldText;
    public GameObject playerHealthObject;

    // Use this for initialization
    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStaminaManager>();
        playerHealthObject = GameObject.Find("Player");
        playerHealth = playerHealthObject.GetComponent<PlayerHealthManager>();
        playerRanged = playerHealthObject.GetComponent<PlayerRangedAttack>();
        shieldBlockScript = FindObjectOfType<ShieldBlock>();
        playerStatsScript = FindObjectOfType<PlayerStats>();

        daggerTextObject = GameObject.Find("DaggerText");
        shieldTextObject = GameObject.Find("ShieldText");
        shieldImageObject = GameObject.Find("ShieldImage");

        shieldTextObject.SetActive(false);
        shieldImageObject.SetActive(false);

        // daggerImageObject.GetComponent<Image>().Sprite = YourSprite;

        if (!UIExists)
        {
            UIExists = true;
            //DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            //Destroy(gameObject);
        }

        thePS = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldBlockScript.shieldOn)
        {
            shieldImageObject.SetActive(true);
            shieldTextObject.SetActive(true);
        }
        else
        {
            shieldImageObject.SetActive(false);
            shieldTextObject.SetActive(false);
        }

        healthbar.maxValue = playerHealth.playerMaxHealth;
        healthbar.value = playerHealth.playerCurrentHealth;
        HPText.text = "HP: " + playerHealth.playerCurrentHealth +
            "/" + playerHealth.playerMaxHealth;

        daggerText.text = ": " + playerRanged.daggerCount;
        shieldText.text = ": " + shieldBlockScript.shieldBlocksLeft;

        staminaBar.maxValue = playerStamina.playerMaxStamina;
        staminaBar.value = playerStamina.playerCurrentStamina;
    }
}
