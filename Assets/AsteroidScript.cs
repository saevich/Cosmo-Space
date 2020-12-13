using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public float rotation;
    public float speed;
    public float weight;

    float multiplier;
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere*rotation;

        multiplier = Random.Range(0.5f, 2.0f);

        asteroid.velocity = new Vector3(0, 0, -(speed / multiplier));
        transform.localScale *= multiplier;
        weight *= multiplier;
    }

    void Update()
    {
        if (ControllerScript.isStoped)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Asteroid" || other.tag == "Boundary")
        {
            return;
        }

        GameObject newExplosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        newExplosion.transform.localScale *= multiplier;
        
        if (other.tag == "Player")
        {
            ControllerScript.decreaseHealth(weight);
            //Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
        }
        if (other.tag != "Player")
        {
            ControllerScript.increaseScore((int)weight);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
