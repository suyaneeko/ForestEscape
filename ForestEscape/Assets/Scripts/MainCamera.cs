using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 vPosition;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Vector3 lookOffset;

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
}
