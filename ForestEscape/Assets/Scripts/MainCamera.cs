using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Vector3 vPosition;

    void Start()
    {
    }

    void Update()
    {
        //transform.LookAt(Player.transform.position + new Vector3(0, 0, -2.5f));
        //vPosition = Player.transform.position;
        //vPosition = vPosition + new Vector3(0.1f, 0.57f, -0.47f);
        //transform.position = vPosition;
        //transform.rotation = new Quaternion(transform.rotation.x + 10f, -17f, 0f, 1f);
    }
}
