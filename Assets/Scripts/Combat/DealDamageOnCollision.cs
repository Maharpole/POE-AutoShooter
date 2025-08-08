using UnityEngine;

public class DealDamageOnCollision : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private bool alsoTriggerOnEnter = true; // supports trigger-based colliders

    private void OnCollisionEnter(Collision collision)
    {
        TryDealDamage(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alsoTriggerOnEnter)
        {
            return;
        }
        TryDealDamage(other);
    }

    private void TryDealDamage(Component hit)
    {
        if (hit == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(targetTag) && !hit.CompareTag(targetTag))
        {
            return;
        }

        Health health = hit.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}


