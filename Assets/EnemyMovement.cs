using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed;

    private Vector3 TargetPoint;

    void Start()
    {
        TargetPoint = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPoint, speed * Time.deltaTime);

        // �������� ��������� �������� ��� �������� �� ����� ����������
        if (Vector3.Distance(transform.position, TargetPoint) < 0.05f)
        {
            // ������ ����, ����� ���� �������� �� �������
            TargetPoint = TargetPoint == pointB ? pointA : pointB;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);
    }
}
