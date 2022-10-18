using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float visionDistance;
    [SerializeField] private int amountRays;
    [SerializeField] private float angle;
    [SerializeField] private Vector3 offset;

    private EnemyController _parent;
    private Transform _target;

    private void Awake()
    {
        _parent = GetComponentInParent<EnemyController>();
    }

    private void Update()
    {
        LetOutRays();
    }

    private void LetOutRays()
    {
        float j = 0;

        for (int i = 0; i < amountRays; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += +angle * Mathf.Deg2Rad / amountRays;

            Vector3 direction = transform.TransformDirection(new Vector3(x, 0, y));

            if (GetRaycast(direction))
            {
                _parent.MoveTarget = _target;
            }

            if (x != 0)
            {
                direction = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(direction))
                {
                    _parent.MoveTarget = _target;
                }
            }
        }
    }

    private bool GetRaycast(Vector3 direction)
    {
        var result = false;

        Vector3 position = transform.position + offset;

        if (Physics.Raycast(position, direction, out var hit, visionDistance))
        {
            if (hit.transform.TryGetComponent<PlayerController>(out var player))
            {
                Debug.DrawLine(position, hit.point, Color.green);
                _target = player.transform;
                result = true;
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, direction * visionDistance, Color.red);
        }

        return result;
    }
}
