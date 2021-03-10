using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Script to choose class at the beginning of game
public class CharacterSelect : MonoBehaviour
{
    public EventSystem eventSystem;
    private GameObject WarriorButton;
    private GameObject RogueButton;
    private GameObject ScholarButton;

    // Use this for initialization
    void Start()
    {
        WarriorButton = GameObject.Find("WarriorButton");
        RogueButton = GameObject.Find("RogueButton");
        ScholarButton = GameObject.Find("ScholarButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == WarriorButton)
        {
            // vitalityObject.SetActive(true);
            // vitalityText.text = "Will increase max health by 1";
        }
    }
}