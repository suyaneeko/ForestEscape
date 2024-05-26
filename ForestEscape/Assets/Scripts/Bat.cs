using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] float hitForce = 10f;
    public Vector3 target;
    void Start()
    {
    }

    void LateUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                Vector3 directionToTarget = (target - collision.contacts[0].point).normalized;
                ballRigidbody.AddForce(directionToTarget * hitForce, ForceMode.Impulse);
            }
        }
    }

    public void SetTartget(Vector3 target)
    {
        this.target = target;
    }
}
