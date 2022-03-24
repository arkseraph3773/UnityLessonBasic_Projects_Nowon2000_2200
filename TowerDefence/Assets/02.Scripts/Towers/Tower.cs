using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    public TowerInfo info;
    public LayerMask enemyLayer;
    public float detectRange;

    public Transform turretRotatePoint;
    Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(tr.position, detectRange, enemyLayer);
        //cols.OrderBy(x => (x.transform.position - WayPoints.points.Last().transform.position));

        
        if(cols.Length > 0)
        {
            cols.OrderBy(x => (x.transform.position - WayPoints.points.Last().transform.position));
            /*cols.OrderBy(x => (x.transform.position - tr.position).magnitude);*/
            turretRotatePoint.LookAt(cols[0].transform);
        }
        
    }
}
