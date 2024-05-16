using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
     
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void ThrowBall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Vector3 speed = new Vector3(0, 3, 5);
        GetComponent<Rigidbody>().AddForce(speed);
        Debug.Log("throw ball");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bat"))
            Debug.Log("Get Collide with Bat");

        Debug.Log("Get Collide with Something");

    }
}
