using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 controlPoint1; // ������ 1
    private Vector3 controlPoint2; // ������ 2
    private Vector3 targetPosition; // ��ǥ ��ġ
    private float travelTime; // �̵� �ð�
    private float elapsedTime = 0f; // ��� �ð�
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
            // �̵��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            // ���� ��� �ð��� ���� �� �̵�
            float t = elapsedTime / travelTime;
            transform.position = CalculateBezierPoint(t);

            // ��ǥ ��ġ�� �����ϸ� �ı�
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

    // ������ ��� �� ���� ���
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
        // �浹�� ������Ʈ�� �߱���Ʈ �Ǵ� ��� ������Ʈ���� Ȯ��
        if (collision.gameObject.CompareTag("Bat"))
        {
            Debug.Log("Collide with bat");
            // �浹 �� ���� ���ؼ� ���ư��� ȿ��
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
            // ���� �浹�� ������ �� Ŭ���� �����Ͽ� �߰����� ������ ������ �� �ֽ��ϴ�.
        }
    }

    private float CalculateForce(float clickDuration, float collisionDuration)
    {
        // ���÷� �����ϰ� Ŭ�� ���� �ð��� ���� ���� �����մϴ�.
        float maxForce = 500f;
        float minForce = 100f;
        float forceRange = maxForce - minForce;
        float normalizedDuration = clickDuration / collisionDuration;
        return minForce + normalizedDuration * forceRange;
    }

    private Vector3 CalculateDirection(float clickDuration, float collisionDuration)
    {
        // Ŭ�� ���� �ð��� ���� ������ �����մϴ�.
        float middleThreshold = 0.5f; // Ŭ�� ���� �ð��� �� ������ ������ ��������, ũ�� ���������� ���ư��ϴ�.
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
