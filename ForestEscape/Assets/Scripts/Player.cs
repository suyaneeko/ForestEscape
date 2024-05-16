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
    [SerializeField] private GameObject testBall;
    [SerializeField] private CharacterController controller;
    private PLAYERANIM eCurAnim = PLAYERANIM.IDLE;
    private float dMoveSpeed = 3f;

    void Start()
    {
        ChangeMotion(PLAYERANIM.IDLE);
    }

    void Update()
    {
        // 플레이어 이동
        if (Input.GetKey(KeyCode.W))
        {
            ChangeMotion(PLAYERANIM.RUNNING);
            controller.Move(new Vector3(0, 0, -dMoveSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S))
        {
            ChangeMotion(PLAYERANIM.RUNNING);
            controller.Move(new Vector3(0, 0, dMoveSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A))
        {
            ChangeMotion(PLAYERANIM.RUNNING);
            controller.Move(new Vector3(dMoveSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            ChangeMotion(PLAYERANIM.RUNNING);
            controller.Move(new Vector3(-dMoveSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChangeMotion(PLAYERANIM.BATTING);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(testBall);
            testBall.SetActive(true);
            Vector3 ballPos = transform.position + new Vector3(-3, 5f, -5f);
            testBall.GetComponent<Ball>().SetPosition(ballPos);
            testBall.GetComponent<Ball>().ThrowBall();
        }
        if (!Input.anyKey)
        {
            ChangeMotion(PLAYERANIM.IDLE);
        }
    }

    void ChangeMotion(PLAYERANIM eState)
    {
        if (eCurAnim == eState)
            return;

        playerAnims[(int)PLAYERANIM.IDLE].SetActive(false);
        playerAnims[(int)PLAYERANIM.BATTING].SetActive(false);
        playerAnims[(int)PLAYERANIM.RUNNING].SetActive(false);
        //playerAnims[(int)PLAYERANIM.DISAPPOINT].SetActive(false);

        switch (eState)
        {
            case PLAYERANIM.IDLE:
                playerAnims[(int)PLAYERANIM.IDLE].SetActive(true);
                break;
            case PLAYERANIM.RUNNING:
                playerAnims[(int)PLAYERANIM.RUNNING].SetActive(true);
                break;
            case PLAYERANIM.BATTING:
                playerAnims[(int)PLAYERANIM.BATTING].SetActive(true);
                break;
            case PLAYERANIM.DISAPPOINT:
                //playerAnims[(int)PLAYERANIM.DISAPPOINT].SetActive(true);
                break;
        }

        eCurAnim = eState;
    }
}
