using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStream : MonoBehaviour {
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;

    public float Force = 10f;

    private void FixedUpdate()
    {
        var hits = Physics2D.LinecastAll(_startPoint.position, _endPoint.position);
        foreach (var hit in hits)
        {
            var platformEffector = hit.transform.GetComponent<PlatformEffector2D>();
            if (platformEffector)
            {
                continue;
            }

            var rigidbody = hit.transform.GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                rigidbody.AddForce(transform.up * Force);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_startPoint == null || _endPoint == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_startPoint.position, _endPoint.position);
    }
}
