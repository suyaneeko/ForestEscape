using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 controlPoint1; // 제어점 1
    private Vector3 controlPoint2; // 제어점 2
    private Vector3 targetPosition; // 목표 위치
    private float travelTime; // 이동 시간
    private float elapsedTime = 0f; // 경과 시간
    private Rigidbody rb;
    private bool collided = false;
    private bool firstThrow = true;
    private float collisionStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(firstThrow)
        {
            // 이동한 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 현재 경과 시간에 따라 공 이동
            float t = elapsedTime / travelTime;
            transform.position = CalculateBezierPoint(t);

            // 목표 위치에 도달하면 파괴
            if (elapsedTime >= travelTime)
            {
                firstThrow = false;
            }
        }

        if (collided)
        {
            float collisionDuration = Time.time - collisionStartTime;
            if (Input.GetMouseButtonDown(0))
            {
                float clickDuration = Time.time - collisionStartTime;
                float force = CalculateForce(clickDuration, collisionDuration);
                Vector3 direction = CalculateDirection(clickDuration, collisionDuration);
                rb.AddForce(direction * force);
            }
        }
    }

    public void InitiateMovement(Vector3 control1, Vector3 control2, Vector3 target, float time)
    {
        controlPoint1 = control1;
        controlPoint2 = control2;
        targetPosition = target;
        travelTime = time;
    }

    // 베지어 곡선의 한 지점 계산
    private Vector3 CalculateBezierPoint(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * transform.position; // (1-t)^3 * P0
        p += 3 * uu * t * controlPoint1; // 3 * (1-t)^2 * t * P1
        p += 3 * u * tt * controlPoint2; // 3 * (1-t) * t^2 * P2
        p += ttt * targetPosition; // t^3 * P3

        return p;
    }

    public void SetPosition(Vector3 pos)
    {

    }

    public void ThrowBall()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
        // 충돌된 오브젝트가 야구배트 또는 충격 오브젝트인지 확인
        if (collision.gameObject.CompareTag("Bat"))
        {
            Debug.Log("Collide with bat");
            // 충돌 시 힘을 가해서 날아가는 효과
            rb.AddForce((collision.contacts[0].point - transform.position).normalized * 500f);
        }

        /*
         if (!collided)
        {
            collided = true;
            collisionStartTime = Time.time;
        }*/
    }

    void OnCollisionExit(Collision collision)
    {
        if (collided)
        {
            collided = false;
            // 공이 충돌이 끝났을 때 클릭을 감지하여 추가적인 동작을 수행할 수 있습니다.
        }
    }

    private float CalculateForce(float clickDuration, float collisionDuration)
    {
        // 예시로 간단하게 클릭 지속 시간에 따라 힘을 조절합니다.
        float maxForce = 500f;
        float minForce = 100f;
        float forceRange = maxForce - minForce;
        float normalizedDuration = clickDuration / collisionDuration;
        return minForce + normalizedDuration * forceRange;
    }

    private Vector3 CalculateDirection(float clickDuration, float collisionDuration)
    {
        // 클릭 지속 시간에 따라 방향을 조절합니다.
        float middleThreshold = 0.5f; // 클릭 지속 시간이 이 값보다 작으면 왼쪽으로, 크면 오른쪽으로 날아갑니다.
        float normalizedDuration = clickDuration / collisionDuration;
        if (normalizedDuration < middleThreshold)
        {
            return Vector3.left;
        }
        else if (normalizedDuration > (1 - middleThreshold))
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
