using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerPosition;
    public float spawnTime = 1f;
    public int spawnCount = 1;
    float minRadius = 20f;
    float maxRadius = 35f;
    
    void Start()
    {
        InvokeRepeating("addEnemies", 0f, spawnTime);
    }

    public void addEnemies()
    {
        for (int i = 1; i <= spawnCount; i++)
        {
            Vector2 randDir = Random.insideUnitCircle.normalized;

            float randomDis = Random.Range(minRadius, maxRadius);
            Vector3 spawnOffset = new Vector3(randDir.x, 0f, randDir.y) * randomDis;
            Vector3 spawnPos = transform.position + spawnOffset;
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyBase e = enemy.GetComponent<EnemyBase>();
            EnemyMoveScript m = enemy.GetComponent<EnemyMoveScript>();

            e.SetUpEnemyType();
            m.playerPosition = playerPosition;

            Debug.Log("Spawn only " + i.ToString());
        }
        
    }
    void Update()
    {
        
    }
}
