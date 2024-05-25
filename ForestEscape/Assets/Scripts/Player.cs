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
    [SerializeField] private Transform bat;

    private PLAYERANIM eCurAnim = PLAYERANIM.IDLE;
    private float dMoveSpeed = 3f;
    private Vector3 velocity;
    public float speed = 5f;
    public float gravity = -9.81f;
    public bool isGrounded = false;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float mouseSensitivity = 100f;
    private float rotationY = 0f;
    public float initialRotationY = 0f;
    private bool isCombat = false;

    void Start()
    {
        ChangeMotion(PLAYERANIM.IDLE);
        //transform.rotation = Quaternion.Euler(0f, 135f, 0f);
        Vector3 moveDirection = transform.forward;
        rotationY = initialRotationY;
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    void Update()
    {
        /*
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
        }*/
        if(!isCombat)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Get input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            rotationY += mouseX;
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
                ChangeMotion(PLAYERANIM.RUNNING);

            Vector3 move = transform.right * x + transform.forward * z;

            if (move.magnitude > 1)
                move = move.normalized;

            controller.Move(move * speed * Time.deltaTime);

            if (isGrounded)
                velocity.y = -2f;
            else
                velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime); // Gravity

            if (x == 0 && z == 0 && isGrounded)
            {
                velocity.x = 0;
                velocity.z = 0;
                ChangeMotion(PLAYERANIM.IDLE);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeMotion(PLAYERANIM.BATTING);
            }
        }
    }

    void ChangeMotion(PLAYERANIM eState)
    {
        if(eCurAnim == PLAYERANIM.RUNNING && eState == PLAYERANIM.RUNNING)
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

    public void ReadyCombat()
    {
        isCombat = true;
        ChangeMotion(PLAYERANIM.IDLE);
    }

    public void EndCombat()
    {
        isCombat = false;
        ChangeMotion(PLAYERANIM.IDLE);
    }

    public Vector3 GetBatPosition()
    {
        return bat.position;
    }
}
