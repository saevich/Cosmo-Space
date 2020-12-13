using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject asteroid;
    public float minDelay, maxDelay;

    public static float nextLaunchTime;
    void Start()
    {
        
    }

    void Update()
    {
        if (!ControllerScript.isStarted)
        {
            return;
        }
        if (Time.time > nextLaunchTime)
        {
            float xSize = transform.localScale.x / 2;
            Vector3 asteroidPosition = new Vector3(Random.Range(-xSize, xSize), 0, transform.position.z);
            Instantiate(asteroid, asteroidPosition, Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
