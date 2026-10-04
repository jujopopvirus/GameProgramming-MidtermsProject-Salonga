using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerRotationScript : MonoBehaviour
{
    float detectionRadius = 15f;
    public float rotation_speed = 32.5f;
    public LayerMask enemyLayer;
    public PlayerBaseScript player;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private float FireSpeed = 1.2f;
    [SerializeField] private float nextBulletFire = 0.0f;
    private Transform currentEnemy = null;

    private void Awake()
    {
        player = GetComponent<PlayerBaseScript>();
    }
    void Update()
    {
        Transform targetEnemy = OnDetectEnemies();

        if (targetEnemy != null)
        {
            currentEnemy = targetEnemy;

            Vector3 rayPosition = transform.position;
            lookAtEnemy(targetEnemy);

            Debug.DrawRay(rayPosition, transform.forward * maxRayDistance, Color.red);

            if (currentEnemy != null)
            {

                if (Physics.Raycast(rayPosition, transform.forward, out RaycastHit hit, maxRayDistance, enemyLayer))
                {
                    if (Time.time >= nextBulletFire)
                    {
                        Debug.Log("Enemy Detected : " + hit.transform.name);
                        player.EnemyDetected(currentEnemy);

                        nextBulletFire = Time.time + FireSpeed;
                    }
                }

                else

                {
                    if (currentEnemy != null)
                    {
                        currentEnemy = null;
                        player.EnemyCleared();
                    }
                }
            }

        }
    }

    private void lookAtEnemy(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotation_speed);
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
                
            }
        }


        if (current_enemy == null) player.EnemyCleared();

        return current_enemy;
    }

}
