using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private float maximumTime = 3f;
    private float Timer;
   
    [SerializeField] private PlatformSystem platformSystem;

    [SerializeField] private BoxCollider2D enemyBound;

    private void Start()
    {
        Timer = maximumTime;



    }
    public void SpawnEnemy(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        }

    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = enemyBound.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

}
