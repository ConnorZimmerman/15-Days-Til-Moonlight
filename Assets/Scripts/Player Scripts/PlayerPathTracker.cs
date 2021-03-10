using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathTracker : MonoBehaviour
{
    public string pathName;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PathMaster")
        {
            pathName = other.gameObject.name;
        }
    }
}