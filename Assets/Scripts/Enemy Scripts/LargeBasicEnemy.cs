using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBasicEnemy : MonoBehaviour {

    public int locateEnemyClass;
    public int enemyMaxHealth;
    public int damageToGive;

    public LargeBasicEnemy()
    {
        locateEnemyClass = 2;
        enemyMaxHealth = 20;
        damageToGive = 3;
    }

    // Use this for initialization
    void Start () {
        damageToGive = 3;
    }

    // Update is called once per frame
    void Update () {
        damageToGive = 3;
    }
}
