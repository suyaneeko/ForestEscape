using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum PLAYERANIM
    {
        IDLE,
        RUNNING,
        BATTING,
        DISAPPOINT
    }

    [SerializeField] private GameObject[] playerAnims;
    void Start()
    {
        playerAnims[(int)PLAYERANIM.BATTING].SetActive(true);
        playerAnims[(int)PLAYERANIM.IDLE].SetActive(false);
        playerAnims[(int)PLAYERANIM.RUNNING].SetActive(false);
    }

    void Update()
    {
        float moveZ = 0f;
        float moveX = 0f;
        //if (Input.GetKey(KeyCode.W))
        if (Input.GetKeyDown(KeyCode.W))
        {
            //playerAnims[(int)PLAYERANIM.IDLE].SetActive(false);
            //playerAnims[(int)PLAYERANIM.RUNNING].SetActive(false);
            //playerAnims[(int)PLAYERANIM.BATTING].SetActive(true);
            //playerAnims[1].SetActive(true);
            playerAnims[(int)PLAYERANIM.BATTING].GetComponent<Animation>().Play();
            //moveZ += 1f;
        }
     
        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }
     
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
     
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }
     
        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    }
}
