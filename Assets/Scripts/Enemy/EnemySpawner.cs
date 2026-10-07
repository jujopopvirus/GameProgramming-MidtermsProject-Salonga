using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerPosition;
    public ScoreLable score;
    public float spawnTime = 1f;
    public int spawnCount = 1;
    float minRadius = 35f;
    float maxRadius = 45f;

    void Start()
    {
        InvokeRepeating("addEnemies", 0f, spawnTime);
    }
    private void Pause()
    {
        enabled = false;
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

            float randSpeed = UnityEngine.Random.Range(18f, 28f);
            int randHP = UnityEngine.Random.Range(1, 3);

            e.maxhealthpoints = randHP;
            e.es = this;
            e.SetUpEnemyType();

            m.playerPosition = playerPosition;
            m.TimeToMove = randSpeed;
            
        }
        
    }
}
