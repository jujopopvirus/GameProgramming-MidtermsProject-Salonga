using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Game Configuration")]
    [SerializeField] private Material[] ColorMaterial;
    BulletType[] ColorType = new BulletType[]
    {
        BulletType.Green,
        BulletType.Blue,
        BulletType.Red,
        BulletType.Yellow
    };
    MeshRenderer mesh;
    GameObject bloodParticles;

    [Header("Enemy Stats")]
    public BulletType enemyType;
    public int maxhealthpoints = 1;
    private int healthpoints = 1;
    

    public void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        healthpoints = maxhealthpoints;
    }
    public void SetUpEnemyType()
    {
        int randIndex = UnityEngine.Random.Range(0, ColorMaterial.Length);
        mesh.material = ColorMaterial[randIndex];

        enemyType = ColorType[randIndex];
    }

    public void damage(GameObject interactor, BulletType bullet, int damagePoint)
    {
        if (enemyType == bullet)
        {
            if (healthpoints > 0)
            {
                Debug.Log("Enemy Health : " + healthpoints + "/" + maxhealthpoints);
                return;
            }

            Vector3 pPosition = transform.position;
            Instantiate(bloodParticles, pPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
