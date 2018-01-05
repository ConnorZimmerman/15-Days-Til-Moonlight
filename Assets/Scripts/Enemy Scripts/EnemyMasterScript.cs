using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterScript : MonoBehaviour
{

    public int locateEnemyClass;
    public int enemyMaxHealth;
    public int damageToGive;

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.transform.tag == "Enemy")
        {
            enemyMaxHealth = 2;
            locateEnemyClass = 1;
            damageToGive = 2;
        }
        if (this.gameObject.transform.tag == "LargeEnemyBasic")
        {
            locateEnemyClass = 2;
            enemyMaxHealth = 20;
            damageToGive = 3;
        }
        if(this.gameObject.transform.tag == "BasicRangedEnemy"){
            enemyMaxHealth = 1;
            damageToGive = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.tag == "Enemy")
        {
            enemyMaxHealth = 2;
            locateEnemyClass = 1;
            damageToGive = 2;
            return;
        }
        if (this.gameObject.transform.tag == "LargeEnemyBasic")
        {
            locateEnemyClass = 2;
            enemyMaxHealth = 20;
            damageToGive = 3;
            return;
        }
        if(this.gameObject.transform.tag == "BasicRangedEnemy"){
            enemyMaxHealth = 1;
            damageToGive = 2;
            return;
        }
    }

    public void enemyStats()
    {
        if (locateEnemyClass == 1)
        {
        }
        if (locateEnemyClass == 2)
        {
            damageToGive = 3;
        }
    }
}

