using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    float detectionRadius = 15f;
    public float rotation_speed = 6.5f;
    public LayerMask enemyLayer;
    public PlayerBaseScript player;

    private void Awake()
    {
        player = GetComponent<PlayerBaseScript>();
    }
    void Update()
    {
        Transform currentEnemy = OnDetectEnemies();
        if (currentEnemy != null)
        {
            transform.LookAt(OnDetectEnemies());
            player.EnemyDetected(currentEnemy);
        }
        
    }

    public Transform OnDetectEnemies()
    {
        Collider[] hitColl = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        Vector3 currPos = transform.position;
        float closePos = Mathf.Infinity;

        Transform current_enemy = null;


        foreach (Collider enemyCol in hitColl)
        {
            
            EnemyMoveScript enemy = enemyCol.GetComponent<EnemyMoveScript>();
            Vector3 dirToCol = enemy.transform.position - currPos;
            float enemyPos = dirToCol.sqrMagnitude;

            if (enemyPos > detectionRadius)
            {
                closePos = enemyPos;
                current_enemy = enemyCol.transform;
                Debug.Log("Enemy Detected");
            }
        }


        if (current_enemy == null) player.EnemyCleared();

        return current_enemy;
    }

}
