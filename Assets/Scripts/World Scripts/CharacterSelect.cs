using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        // else
        // {
        //     vitalityObject.SetActive(false);
        // }
        // if (eventSystem.currentSelectedGameObject == StrengthButton)
        // {
        //     strengthObject.SetActive(true);
        //     strengthText.text = StrengthText(playerStats.strength);
        // }
        // else
        // {
        //     strengthObject.SetActive(false);
        // }
        // if (eventSystem.currentSelectedGameObject == DexterityButton)
        // {
        //     dexterityObject.SetActive(true);
        //     dexterityText.text = DexterityText(playerStats.dexterity);
        // }
        // else
        // {
        //     dexterityObject.SetActive(false);
        // }
    }
}
