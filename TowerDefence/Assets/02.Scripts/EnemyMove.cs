using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform tr;

    public int wayPointIndex;
    public float speed = 0.5f;
    public Transform nextWayPoint;
    float originPosY;
    private void Awake()
    {
        tr = transform;
        originPosY = tr.position.y;
    }
    private void Start()
    {
        nextWayPoint = WayPoints.points[0]; // 첫번째 웨이포인트 
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(nextWayPoint.position.x, originPosY, nextWayPoint.position.z);
        Vector3 dir = (targetPos - tr.position).normalized;
        Debug.Log($"target Pos : {targetPos}");
        if (Vector3.Distance(tr.position, targetPos) < 0.1f) //더 정확하게 하려면 더 작은숫자로
        {
            if (WayPoints.TryGetNextWayPoint(wayPointIndex, out nextWayPoint))
            {
                wayPointIndex++;
            }
            else
            {
                OnReachedToEnd();
            }
        }
        
        tr.Translate(dir * speed * Time.fixedDeltaTime, Space.World);
    }
    private void OnReachedToEnd()
    {
        Destroy(gameObject);
    }
    
}
