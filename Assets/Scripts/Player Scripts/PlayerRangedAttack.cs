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

    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.Find("Player");
        throwForce = 350;
        rotatingObject = this.gameObject.transform.GetChild(9).gameObject;
        daggerCount = 5;
        // targetDir = new Vector3(0,0,10);
        // float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg -90f; //-90f (for enemy direction)
        // Quaternion q = Quaternion.AngleAxis(angle * 10, Vector3.forward);
        // rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 90 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        playerDirection = playerObject.GetComponent<PlayerController>().directionInt;
        if (playerDirection == 0)
        {
            // daggerRotationVector3 = -transform.forward;
            // daggerDirectionVector2 = new Vector2(0f, -throwForce);
            targetDir = new Vector3(0, 1, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);

        }
        if (playerDirection == 1)
        {
            // daggerRotationVector3 = -transform.right;
            // daggerDirectionVector2 = new Vector2(-throwForce, 0f);
            targetDir = new Vector3(1, 0, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (playerDirection == 2)
        {
            // daggerRotationVector3 = -transform.right;
            // daggerDirectionVector2 = new Vector2(0f, throwForce);
            // daggerRotationVector3 = -transform.right;
            // daggerDirectionVector2 = new Vector2(throwForce, 0f);
            targetDir = new Vector3(0, -1, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (playerDirection == 3)
        {
            // daggerRotationVector3 = transform.right;
            // daggerDirectionVector2 = new Vector2(throwForce, 0f);
            targetDir = new Vector3(-1, 0, 0);
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 1000 * Time.deltaTime);
        }
        if (Input.GetButtonDown("Throw") && daggerCount > 0)
        {
            // Vector3 targetDir = daggerRotationVector3 - playerObject.transform.position;
            // float angle = Mathf.Atan2(throwForce, throwForce) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
            //     Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //     newKnife.transform.rotation = Quaternion.RotateTowards(daggerRotationVector3.transform.rotation, q, 90 * Time.deltaTime);

            // GameObject newKnife = Instantiate(projectile, playerObject.transform.position, playerObject.transform.rotation);
            daggerCount--;
            GameObject newKnife = Instantiate(projectile, rotatingObject.transform.position, rotatingObject.transform.rotation);
            // newKnife.transform.Rotate(daggerDirectionVector2 * 90);
            // Vector3 vForce = transform.forward * throwForce + transform.up * throwForce;
            // newKnife.GetComponent<Rigidbody2D>().AddForce(vForce, ForceMode2D.Impulse);
            // newKnife.transform.rotation = Quaternion.LookRotation(vForce);
            newKnife.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, throwForce));
            // newKnife.GetComponent<Rigidbody2D>().AddRelativeForce(-daggerDirectionVector2);
        }
    }
}
