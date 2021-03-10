// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyStaminaManager : MonoBehaviour
// {
//     public int enemyMaxStamina;
//     public int enemyCurrentStamina;
//     public EnemyTestScript basicEnemy;
//     public BasicRangedEnemy rangedEnemy;
//     public bool staminaCharge;
//     public bool enemyActions; /*checks if enemy is currently spending stamina before allowing the
//     enemy to regain stamina*/
//     public float enemyStaminaPercent; /*All health/stamina has percentages for the enemy to calculate
//     its priorities more efficiently*/
//     private GameObject enemyGameObject;
//     public bool enemyShield;
//     public float staminaLock;

//     void Start()
//     {
//         enemyGameObject = this.gameObject;
//         basicEnemy = enemyGameObject.GetComponent<EnemyTestScript>();
//         rangedEnemy = enemyGameObject.GetComponent<BasicRangedEnemy>();
//     }

//     // Update is called once per frame
//     void Update()
//     {

//         if (enemyGameObject.tag == "Enemy")
//         {
//             //enemyInt = enemy.moveDirectionX;
//             enemyShield = basicEnemy.enemyShield;
//             staminaLock = basicEnemy.staminaLock;
//         }
//         if (enemyGameObject.tag == "BasicRangedEnemy")
//         {
//             enemyShield = rangedEnemy.enemyShield;
//             staminaLock = rangedEnemy.staminaLock;
//         }
//         if (enemyCurrentStamina < 0)
//         {
//             enemyCurrentStamina = 0;
//         }

//         if (enemyCurrentStamina <= 0)
//         {
//             enemyCurrentStamina = 2;
//         }

//         if (!enemyActions && enemyCurrentStamina < 2000
//             && !enemyShield && staminaLock == 2) /*Makes sure the enemies not
//             spending stamina, needs stamina, shields down, and that its stamina is not currently 
//             locked*/
//         {
//             enemyCurrentStamina += 9;
//         }
//         else if (enemyShield && enemyCurrentStamina < 2000) /*Gives half stamina recharge when
//             shield is up*/
//         {
//             enemyCurrentStamina += 3;
//         }

//         enemyStaminaPercent = (float)(double)enemyCurrentStamina / enemyMaxStamina * 100;/*(float)(double) 
//         was just something I found online on how to convert to percentages (not sure how it works)*/
//     }
// }