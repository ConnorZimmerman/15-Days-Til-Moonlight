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
    private static bool UIExists;
    public GameObject daggerImageObject;
    public PlayerStats thePS;
    public Text daggerText;

    public GameObject playerHealthObject;

    // Use this for initialization
    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStaminaManager>();
        playerHealthObject = GameObject.Find("Player");
        playerHealth = playerHealthObject.GetComponent<PlayerHealthManager>();
        playerRanged = playerHealthObject.GetComponent<PlayerRangedAttack>();

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
        healthbar.maxValue = playerHealth.playerMaxHealth;
        healthbar.value = playerHealth.playerCurrentHealth;
        HPText.text = "HP: " + playerHealth.playerCurrentHealth +
            "/" + playerHealth.playerMaxHealth;

        daggerText.text = ": " + playerRanged.daggerCount;

        staminaBar.maxValue = playerStamina.playerMaxStamina;
        staminaBar.value = playerStamina.playerCurrentStamina;
    }
}
