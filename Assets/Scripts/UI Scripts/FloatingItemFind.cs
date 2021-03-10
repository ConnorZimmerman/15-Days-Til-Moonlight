using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingItemFind : MonoBehaviour
{
    public float moveSpeed;
    public int daggerCount;
    public string itemType;
    public Text displayNumber;

    // Update is called once per frame
    void Update()
    {
        displayNumber.text = "+" + daggerCount + itemType;
        transform.position = new Vector3(transform.position.x,
            transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }
}