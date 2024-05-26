using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float hitForce = 10f; // 배트가 공에 가하는 힘의 크기
    private Vector3 previousPosition;
    public float baseHitForce = 10f; // 기본 힘 크기
    public float additionalForceFactor = 1.5f; // 추가 힘 비율
    public float centerBiasFactor = 0.5f;
    public Vector3 target;
    void Start()
    {
        previousPosition = transform.position;
    }

    void LateUpdate()
    {
        // 현재 프레임의 위치를 저장
        Vector3 currentPosition = transform.position;

        // 배트의 속도를 계산 (이전 프레임과 현재 프레임의 위치 차이)
        Vector3 batVelocity = (currentPosition - previousPosition) / Time.deltaTime;

        // 배트의 이전 위치를 현재 위치로 업데이트
        previousPosition = currentPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 공인지 확인
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Collide with ball");
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                /*
                // 배트의 이동 속도를 LateUpdate에서 계산된 값으로 사용
                Vector3 batVelocity = (transform.position - previousPosition) / Time.deltaTime;

                // 충돌 지점의 법선 벡터를 얻음
                Vector3 contactNormal = collision.contacts[0].normal;

                // 공에 가해질 힘의 방향 계산 (법선 벡터와 반대 방향)
                Vector3 forceDirection = -contactNormal;

                // 공에 가해질 힘의 크기 계산 (배트의 속도와 공의 상대 속도를 고려하여 조정)
                float relativeSpeed = Vector3.Dot(batVelocity, forceDirection);
                float hitForce = baseHitForce + relativeSpeed * additionalForceFactor;

                // 공에 힘 가하기
                ballRigidbody.AddForce(forceDirection * hitForce, ForceMode.Impulse);
                */

                Vector3 directionToTarget = (target - collision.contacts[0].point).normalized;

                // 공에 힘 가하기
                ballRigidbody.AddForce(directionToTarget * hitForce, ForceMode.Impulse);
            }
        }
    }

    public void SetTartget(Vector3 target)
    {
        this.target = target;
    }
}
