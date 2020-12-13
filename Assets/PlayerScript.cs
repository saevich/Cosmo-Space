using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public GameObject laserShot;
    public Transform laserGun;
    public float speed = 30;
    public float xMin, xMax, zMin, zMax;
    public float tilt;
    public float shotDelay;

    float nextShotTime;
    public static Rigidbody ship;
    public static bool isGameOver = false;
    void Start()
    {
        ship = GetComponent<Rigidbody>();
        //ship.velocity = new Vector3(15, 0, 10);
    }

    void Update()
    {
        if (ControllerScript.isStarted && !ControllerScript.isStoped)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float clampedX = Mathf.Clamp(ship.position.x, xMin, xMax);
            float clampedZ = Mathf.Clamp(ship.position.z, zMin, zMax);

            ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
            ship.position = new Vector3(clampedX, 0, clampedZ);
            ship.rotation = Quaternion.Euler(ship.velocity.z * tilt, 0, -ship.velocity.x * tilt);

            if(Time.time > nextShotTime && Input.GetButton("Fire1"))
            {
                Instantiate(laserShot, laserGun.position, Quaternion.identity);
                nextShotTime = Time.time + shotDelay;
            }
        }

        else if (ControllerScript.isStoped)
        {
            GameObject effect = Instantiate(ControllerScript.playerExplosionEffect, transform.position, Quaternion.identity);
            GameObject effect2 = Instantiate(ControllerScript.playerExplosionEffect2, transform.position, Quaternion.identity);
            effect2.transform.localScale *= 20;

            isGameOver = true;
            Destroy(gameObject);
        }
    }
}
