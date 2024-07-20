using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Transform pointA; // 시작 지점
    public Transform pointB; // 끝 지점
    public float speed = 2f; // 이동 속도

    private Vector3 target;
    private Vector3 lastPosition;
    private List<Rigidbody2D> passengers = new List<Rigidbody2D>();

    void Start()
    {
        // 초기 목표 지점은 pointB
        target = pointB.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // 발판을 목표 지점으로 이동시킴
        Vector3 currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // 목표 지점을 변경
            target = target == pointA.position ? pointB.position : pointA.position;
        }

        // 발판의 이동량 계산
        Vector3 deltaMovement = transform.position - lastPosition;

        // 발판 위에 있는 모든 객체를 이동시킴
        foreach (Rigidbody2D passenger in passengers)
        {
            passenger.transform.position += deltaMovement;
        }

        // 이전 위치 업데이트
        lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null && !passengers.Contains(rb))
        {
            passengers.Add(rb);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null && passengers.Contains(rb))
        {
            passengers.Remove(rb);
        }
    }

    void OnDrawGizmos()
    {
        // 에디터에서 이동 경로를 시각적으로 확인하기 위해 Gizmos 사용
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }

}
