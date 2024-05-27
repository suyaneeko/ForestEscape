using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Vector3 lookOffset;
    private Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // �ε巴�� ī�޶� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ī�޶� �÷��̾ �ٶ󺸵��� ����
        Vector3 lookTarget = player.position + player.TransformDirection(lookOffset);
        transform.LookAt(lookTarget);
    }

    public void SetToInitPos()
    {
        transform.position = initPos;
    }
}
