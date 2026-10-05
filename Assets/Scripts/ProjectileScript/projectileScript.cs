using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public Rigidbody rb;
    public MeshRenderer sm;
    public int proj_speed = 6;

    public BulletType type = BulletType.Green;
    [SerializeField] private Material[] plantTypesMaterials;
    public LayerMask enemyLayer;
    [SerializeField] private ParticleSystem ps;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sm = GetComponent<MeshRenderer>();
    }

    private float currTime;

    public void ChangeBulletType(BulletType BT)
    {
        type = BT;
        switch (BT)
        {
            case BulletType.Green:
                sm.material = plantTypesMaterials[0];
                break;
            case BulletType.Blue:
                sm.material = plantTypesMaterials[1];
                break;
            case BulletType.Red:
                sm.material = plantTypesMaterials[2];
                break;
            case BulletType.Yellow:
                sm.material = plantTypesMaterials[3];
                break;
        }
    }
    public void shootProjectile(Transform spawnTransform)
    {
        var main = ps.main;
        main.playOnAwake = true;

        currTime += Time.deltaTime;
        float normTime = currTime / 1.0f;

        transform.rotation = spawnTransform.rotation;
        rb.AddForce((spawnTransform.forward * proj_speed), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            EnemyMoveScript enemy = collision.gameObject.GetComponent<EnemyMoveScript>();

            if (enemy.enemyType == type)
            {
                Debug.Log("Attacked!");
                enemy.EnemyDead();
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
