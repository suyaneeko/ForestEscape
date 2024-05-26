using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float hitForce = 10f; // ��Ʈ�� ���� ���ϴ� ���� ũ��
    private Vector3 previousPosition;
    public float baseHitForce = 10f; // �⺻ �� ũ��
    public float additionalForceFactor = 1.5f; // �߰� �� ����
    public float centerBiasFactor = 0.5f;
    public Vector3 target;
    void Start()
    {
        previousPosition = transform.position;
    }

    void LateUpdate()
    {
        // ���� �������� ��ġ�� ����
        Vector3 currentPosition = transform.position;

        // ��Ʈ�� �ӵ��� ��� (���� �����Ӱ� ���� �������� ��ġ ����)
        Vector3 batVelocity = (currentPosition - previousPosition) / Time.deltaTime;

        // ��Ʈ�� ���� ��ġ�� ���� ��ġ�� ������Ʈ
        previousPosition = currentPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� ������ Ȯ��
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Collide with ball");
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                /*
                // ��Ʈ�� �̵� �ӵ��� LateUpdate���� ���� ������ ���
                Vector3 batVelocity = (transform.position - previousPosition) / Time.deltaTime;

                // �浹 ������ ���� ���͸� ����
                Vector3 contactNormal = collision.contacts[0].normal;

                // ���� ������ ���� ���� ��� (���� ���Ϳ� �ݴ� ����)
                Vector3 forceDirection = -contactNormal;

                // ���� ������ ���� ũ�� ��� (��Ʈ�� �ӵ��� ���� ��� �ӵ��� ����Ͽ� ����)
                float relativeSpeed = Vector3.Dot(batVelocity, forceDirection);
                float hitForce = baseHitForce + relativeSpeed * additionalForceFactor;

                // ���� �� ���ϱ�
                ballRigidbody.AddForce(forceDirection * hitForce, ForceMode.Impulse);
                */

                Vector3 directionToTarget = (target - collision.contacts[0].point).normalized;

                // ���� �� ���ϱ�
                ballRigidbody.AddForce(directionToTarget * hitForce, ForceMode.Impulse);
            }
        }
    }

    public void SetTartget(Vector3 target)
    {
        this.target = target;
    }
}
