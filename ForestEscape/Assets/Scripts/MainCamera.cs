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
        transform.LookAt(Player.transform);
        vPosition = Player.transform.position;
        vPosition = vPosition + new Vector3(0, 3f, 2f);
        transform.position = vPosition;
    }
}
