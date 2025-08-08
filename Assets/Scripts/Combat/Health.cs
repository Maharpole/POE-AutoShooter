using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private bool destroyOnDeath = true;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0f;
    public float Normalized => maxHealth > 0f ? Mathf.Clamp01(currentHealth / maxHealth) : 0f;

    public event System.Action<float> Damaged; // arg: damage amount
    public event System.Action<float> Healed;  // arg: heal amount
    public event System.Action Died;

    private void Awake()
    {
        if (currentHealth <= 0f)
        {
            currentHealth = maxHealth;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void SetMaxHealth(float newMax, bool refill)
    {
        maxHealth = Mathf.Max(1f, newMax);
        if (refill)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public void SetDestroyOnDeath(bool shouldDestroy)
    {
        destroyOnDeath = shouldDestroy;
    }

    public void Heal(float amount)
    {
        if (amount <= 0f || IsDead)
        {
            return;
        }
        float prev = currentHealth;
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        float healed = currentHealth - prev;
        if (healed > 0f)
        {
            Healed?.Invoke(healed);
        }
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f || IsDead)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        Damaged?.Invoke(amount);

        if (currentHealth <= 0f)
        {
            Died?.Invoke();
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }
}


