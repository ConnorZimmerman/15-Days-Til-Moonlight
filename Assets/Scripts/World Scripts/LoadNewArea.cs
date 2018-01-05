using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{

    public string levelToLoad;

    public string exitPoint;

    private PlayerController thePlayer;

    public GameObject startPoint;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //SceneManager.LoadScene(levelToLoad);
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
    }
}
