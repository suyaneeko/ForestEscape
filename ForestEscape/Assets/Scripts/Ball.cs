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
    private bool firstThrow = true;
    private float dieTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(dieTimer > 0f)
        {
            dieTimer -= Time.deltaTime;
            if(dieTimer <= 0f)
            {
                dieTimer = 0f;
                CombatManager.Instance.CheckCombat(0);
                Destroy(gameObject);
            }
        }
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
                GetComponent<Rigidbody>().useGravity = true;
                dieTimer = 3f;
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
        if(collision.gameObject.CompareTag("Monster"))
        {
            dieTimer = 3f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
    }
}
