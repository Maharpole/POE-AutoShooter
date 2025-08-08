using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnIntervalSeconds = 2.5f;
    [SerializeField] private int maxAlive = 25;
    [SerializeField] private Vector2 spawnAreaHalfExtents = new Vector2(12f, 12f);

    private float timeSinceLastSpawn;
    private int aliveCount;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnIntervalSeconds && aliveCount < maxAlive)
        {
            timeSinceLastSpawn = 0f;
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (enemyPrefab == null)
        {
            return;
        }

        Vector2 randomOffset = new Vector2(
            Random.Range(-spawnAreaHalfExtents.x, spawnAreaHalfExtents.x),
            Random.Range(-spawnAreaHalfExtents.y, spawnAreaHalfExtents.y)
        );

        Vector3 spawnPos = new Vector3(transform.position.x + randomOffset.x, transform.position.y, transform.position.z + randomOffset.y);
        GameObject instance = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        aliveCount++;
        var notifier = instance.AddComponent<SpawnLifecycleNotifier>();
        notifier.onDestroyed = () => { aliveCount = Mathf.Max(0, aliveCount - 1); };
    }
}

public class SpawnLifecycleNotifier : MonoBehaviour
{
    public System.Action onDestroyed;
    private void OnDestroy()
    {
        onDestroyed?.Invoke();
    }
}


