using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Game Configuration")]
    
    BulletType[] ColorType = new BulletType[]
    {
        BulletType.Green,
        BulletType.Blue,
        BulletType.Red,
        BulletType.Yellow
    };
    [SerializeField] private GameObject bloodParticles;
    [Header("Body Colors")]
    [SerializeField] private Material[] ClothesMaterials;
    [SerializeField] private Material[] BodyMaterials;
    [SerializeField] private Material[] OutlineMaterials;
    [SerializeField] private GameObject mesh;
    private SkinnedMeshRenderer smr;

    [Header("Enemy Stats")]
    public BulletType enemyType;
    public int maxhealthpoints = 1;
    private int healthpoints = 1;
    public EnemySpawner es;


    public void Awake()
    {
        smr = mesh.GetComponent<SkinnedMeshRenderer>();
    }
    public void SetUpEnemyType()
    {
        healthpoints = maxhealthpoints;

        int randIndex = UnityEngine.Random.Range(0, ClothesMaterials.Length);

        enemyType = ColorType[randIndex];
        SetUpEnemyMat(randIndex);

        Debug.Log("Enemy Color : " + ColorType[randIndex].ToString());
    }

    private void SetUpEnemyMat(int MatIndex)
    {
        Material[] mats = smr.materials;

        mats[0] = BodyMaterials[MatIndex];
        mats[2] = ClothesMaterials[MatIndex];
        mats[3] = OutlineMaterials[MatIndex];

        smr.materials = mats;
    }

    public void damage(GameObject interactor, BulletType bullet, int damagePoint)
    {
        if (enemyType != bullet) return;

        healthpoints -= damagePoint;

        if (healthpoints <= 0)
        {
            Vector3 pPosition = transform.position;
            Instantiate(bloodParticles, pPosition, transform.rotation);

            GameManager.Instance.addScore(maxhealthpoints);

            Destroy(gameObject);
            Debug.Log("Killed Enemy! Enemy type : " + enemyType);
        } 
    }
}
