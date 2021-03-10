using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    private DialogueManager dMan;
    public string[] dialogLines;
    public GameObject thePlayer;
    public Transform target;
    public VillagerMovement villagerScript;
    public bool dialogLock;

    // Use this for initialization
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
        dialogLock = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Select"))
            {
                if (!dMan.dialogActive)
                {
                    dialogLock = true;

                    if (dialogLock == true)
                    {
                        villagerScript.anim.SetFloat("IdolDirectionX", villagerScript.directionInt);
                        villagerScript.anim.SetFloat("IdolDirectionY", villagerScript.directionInt);
                        villagerScript.anim.SetBool("DialogueActive", true);

                        dMan.ShowDialogue();
                        dMan.currentLine = 0;
                        dMan.dialogLines = dialogLines;
                    }
                }
                if (transform.parent.GetComponent<VillagerMovement>() != null)
                {
                    transform.parent.GetComponent<VillagerMovement>()
                        .canMove = false;
                }
            }
        }
    }
}