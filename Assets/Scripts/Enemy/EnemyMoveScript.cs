using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    [SerializeField] private Material[] ColorMaterial;
    [SerializeField] private BulletType[] ColorType = new BulletType[]
    {
        BulletType.Green,
        BulletType.Blue,
        BulletType.Red,
        BulletType.Yellow
    };

    public BulletType enemyType;
    public int hitpoints = 1;
    private MeshRenderer mesh;

     public Transform playerPosition;
    private float elapsedTime;

    public void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    public void SetUpEnemyType()
    {
        int randIndex = UnityEngine.Random.Range(0,ColorMaterial.Length);
        mesh.material = ColorMaterial[randIndex];

        enemyType = ColorType[randIndex];
    }

    public void Update()
    {
        MoveToPlayer(transform.position);
    }
    public void MoveToPlayer(Vector3 startPos)
    {
        if (playerPosition != null)
        {
            Vector3 targetPosition = playerPosition.position;
            startPos.y = playerPosition.position.y;

            
            elapsedTime += Time.deltaTime * 0.01f;
            float t = elapsedTime / 16f;

            transform.position = Vector3.Lerp(startPos, targetPosition, t);
        }
        else
        {
            Debug.Log("No player found");
        }
    }
}
