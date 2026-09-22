using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public Rigidbody rb;
    public int proj_speed = 5;

    public BulletType type = BulletType.Green;
    public LayerMask enemyLayer;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private float currTime;


    public void shootProjectile(Transform spawnTransform)
    {
        currTime += Time.deltaTime;
        float normTime = currTime / 1.0f;

        rb.AddForce((spawnTransform.forward * proj_speed), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log("Attacked!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
