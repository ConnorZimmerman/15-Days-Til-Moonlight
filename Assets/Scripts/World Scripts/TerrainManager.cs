using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    private PlayerController thePlayer;
    private SFXManager sFX;
    public bool inWater;
    private bool isPlaying;

    // // Use this for initialization
    // void Start()
    // {
    //     thePlayer = FindObjectOfType<PlayerController>();
    //     inWater = false;
    //     sFX = FindObjectOfType<SFXManager>();
    // 	isPlaying = true;
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // public void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Water")
    //     {
    //         inWater = true;
    //         if (thePlayer.playerMoving)
    //         {
    //             if (isPlaying)
    //             {
    //             	isPlaying = false;;
    //                 sFX.waterWalk.Play();
    //             }
    //         }
    //         else
    //         {
    // 			isPlaying = true;
    //             sFX.waterWalk.Stop();
    //         }
    //         // thePlayer.moveSpeed = 2;
    //         Debug.Log("working");
    //         // wallBlock = true;
    //         // //enemy.following = false;
    //         // //enemy.enemyMoving = true;
    //         // engaged = false;
    //         // colliderOn = false;

    //     }
    // }

    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Water")
    //     {
    //         inWater = false;
    //         // thePlayer.moveSpeed = 2;
    //         // Debug.Log("working");
    //         // wallBlock = true;
    //         // //enemy.following = false;
    //         // //enemy.enemyMoving = true;
    //         // engaged = false;
    //         // colliderOn = false;

    //     }
    // }
}