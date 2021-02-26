using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject projectile;
    public int playerDirection;
    public float throwForce;
    private Vector2 daggerDirectionVector2;
    private Vector3 daggerRotationVector3;
    public GameObject rotatingObject;
    public Vector3 targetDir;
    public int daggerCount;
    private GlobalDataScript globalData;
    private PlayerStats playerStats;

    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.Find("Player");
        throwForce = 350;
        rotatingObject = this.gameObject.transform.GetChild(9).gameObject;
        daggerCount = GlobalDataScript.globalPlayerDaggerCount;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = playerObject.GetComponent<PlayerController>().DeterminePlayerDirection();

        if (playerDirection == 0)
        {
            targetDir = new Vector3(0, 1, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);

        }
        if (playerDirection == 1)
        {
            targetDir = new Vector3(1, 0, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (playerDirection == 2)
        {
            targetDir = new Vector3(0, -1, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (playerDirection == 3)
        {
            targetDir = new Vector3(-1, 0, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (playerStats.dexterity >= 12 && Input.GetButtonDown("Throw") && daggerCount > 0 || playerStats.dexterity >= 12 && Input.GetKeyDown(KeyCode.Space) && daggerCount > 0)
        {
            daggerCount--;
            GameObject newKnife = Instantiate(projectile, rotatingObject.transform.position, rotatingObject.transform.rotation);
            newKnife.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, throwForce));
        }
    }
}
