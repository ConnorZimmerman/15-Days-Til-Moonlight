using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeStalkZone : MonoBehaviour
{

    public CircleCollider2D stalkZone;
    public bool stalkZoneOn;

    // Use this for initialization
    void Start()
    {

        stalkZoneOn = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.name == "StalkZone")
    //     {
    //         stalkZoneOn = true;

    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.name == "StalkZone")
    //     {
    //         stalkZoneOn = false;

    //     }
    // }
}
