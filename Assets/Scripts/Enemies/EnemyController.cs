using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1.5f;

    private Transform target;

    private void Awake()
    {
        AcquireTargetIfNeeded();
    }

    private void OnEnable()
    {
        AcquireTargetIfNeeded();
    }

    private void Update()
    {
        if (target == null)
        {
            AcquireTargetIfNeeded();
            return;
        }

        Vector3 vectorToTarget = target.position - transform.position;
        float distanceToTarget = vectorToTarget.magnitude;
        if (distanceToTarget <= stoppingDistance)
        {
            return;
        }

        Vector3 moveDirection = vectorToTarget.normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(
            transform.forward,
            moveDirection,
            1f - Mathf.Exp(-10f * Time.deltaTime)
        );
    }

    private void AcquireTargetIfNeeded()
    {
        if (target != null)
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }
}


