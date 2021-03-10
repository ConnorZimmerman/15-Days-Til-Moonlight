using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDialogue : MonoBehaviour
{
    // public GameObject dBox;
    // public Text dText;

    // public bool enemyDialogActive;

    // public string[] dialogLines;
    // public int currentLine;

    // public float talkTime;
    // public float specialTalkTime;
    // public int randomLine;

    // private bool timeStart;

    // private PlayerController thePlayer;

    // Use this for initialization
    void Start()
    {
        // thePlayer = FindObjectOfType<PlayerController>();
        // currentLine = 1;
        // dBox.SetActive(false);
        // talkTime = 1;
        // timeStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        // dText.text = dialogLines[currentLine];
        // if (talkTime <= 0)
        // {
        //     timeStart = false;
        //     dBox.SetActive(false);
        //     talkTime = 1;
        // }
        // if (thePlayer.soFast)
        // {
        //     Speed();
        // }
        // if(timeStart){
        //     talkTime -= Time.deltaTime;
        // }

    }

    public void FearCounterZero()
    {
        // if (talkTime == 1)
        // {
        //     randomLine = Random.Range(0, 300);
        //     talkTime -= Time.deltaTime;
        // }
        // else
        // {
        //     talkTime -= Time.deltaTime;
        //     dBox.SetActive(false);
        // }
        // switch (randomLine)
        // {
        //     case 0:
        //         currentLine = 0;
        //         dBox.SetActive(true);
        //         break;
        //     case 1:
        //         currentLine = 1;
        //         dBox.SetActive(true);
        //         break;
        //     case 3:
        //         currentLine = 3;
        //         dBox.SetActive(true);
        //         break;
        //     case 4:
        //         currentLine = 4;
        //         dBox.SetActive(true);
        //         break;
        //     case 5:
        //         currentLine = 5;
        //         dBox.SetActive(true);
        //         break;
        //     case 6:
        //         currentLine = 6;
        //         dBox.SetActive(true);
        //         break;
        //     case 7:
        //         currentLine = 7;
        //         dBox.SetActive(true);
        //         break;
        //     case 8:
        //         currentLine = 8;
        //         dBox.SetActive(true);
        //         break;
        //     case 9:
        //         currentLine = 9;
        //         dBox.SetActive(true);
        //         break;
        // }
        // if (randomLine > 9)
        // {
        //     dBox.SetActive(false);
        // }

    }

    //if the player is moving quickly there is a chance the enemy will comment on it
    public void Speed()
    {
        // if(specialTalkTime == 1){
        //     timeStart = true;
        //     randomLine = Random.Range(0 , 100);
        // }
        // switch (randomLine)
        // {
        //     case 0:
        //         dBox.SetActive(true);
        //         currentLine = 2;
        //         break;
        // }
        // if (randomLine > 0)
        // {
        //     dBox.SetActive(false);
        // }
    }
}