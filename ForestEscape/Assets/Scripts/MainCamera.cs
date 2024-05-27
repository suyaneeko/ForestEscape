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

        // 부드럽게 카메라 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 카메라가 플레이어를 바라보도록 설정
        Vector3 lookTarget = player.position + player.TransformDirection(lookOffset);
        transform.LookAt(lookTarget);
    }

    public void SetToInitPos()
    {
        transform.position = initPos;
    }
}
