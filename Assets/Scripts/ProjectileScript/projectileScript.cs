using UnityEngine;

public class projectileScript : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer sm;
    [HideInInspector] public int proj_speed = 6;
    [HideInInspector] public int damage = 1;
    [HideInInspector] public BulletType type = BulletType.Green;
    [SerializeField] private Material[] plantTypesMaterials;
    [SerializeField] private LayerMask enemyLayer;
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
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            if (enemy == null) Debug.Log("No Enemy Base");
            enemy.damage(gameObject, type, damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
