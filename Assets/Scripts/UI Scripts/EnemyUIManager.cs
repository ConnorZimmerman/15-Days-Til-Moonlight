using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{

    public Slider healthbar;
    public Slider staminaBar;

    public EnemyHealthManager enemyHealth;
    public EnemyStaminaManager enemyStamina;

    public Text staminaTellText;
    public object staminaTellObject;

    private EnemyTestScript enemyScript;
    private BasicRangedEnemy enemyRangedScript;

    public GameObject enemyCanvasObject;

    GameObject enemyObject;

    private EnemyMasterScript enemyMaster;
    public string enemyType;

    // Use this for initialization
    void Start()
    {
        enemyObject = this.gameObject.transform.parent.gameObject;
        if(enemyObject.tag == "Enemy"){
            enemyScript = enemyObject.GetComponent<EnemyTestScript>();
            enemyType = "BasicEnemy";
            
        }
        else
        {
            enemyRangedScript = enemyObject.GetComponent<BasicRangedEnemy>();
            enemyType = "BasicRangedEnemy";
        }
        enemyMaster = enemyObject.GetComponent<EnemyMasterScript>();
        //enemyScript = FindObjectOfType<EnemyTestScript>();

        //enemyCanvasObject = GameObject.Find("Enemy Canvas");
        enemyCanvasObject = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == "BasicEnemy")
        {

            if (enemyScript.deathSeven)
            {
                //GameObject."Enemy".GetEnumerator
                enemyCanvasObject.SetActive(false);
            }

            healthbar.maxValue = enemyHealth.MaxHealth;
            healthbar.value = enemyHealth.CurrentHealth;

            staminaBar.maxValue = enemyStamina.enemyMaxStamina;
            staminaBar.value = enemyStamina.enemyCurrentStamina;

            if (enemyScript.staminaLockBool)
            {
                staminaTellText.text = "-STAMINA";
            }
            else
            {
                staminaTellText.text = "";
            }
        }
        else
        {
            if (enemyRangedScript.deathSeven)
            {
                //GameObject."Enemy".GetEnumerator
                enemyCanvasObject.SetActive(false);
            }
            healthbar.maxValue = enemyHealth.MaxHealth;
            healthbar.value = enemyHealth.CurrentHealth;

            staminaBar.maxValue = enemyStamina.enemyMaxStamina;
            staminaBar.value = enemyStamina.enemyCurrentStamina;

            staminaTellText.text = "";
        }
    }
}
