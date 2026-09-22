using System.Collections;
using UnityEngine;

public enum BulletType
{
    Green,
    Red,
    Blue,
    Yellow

}
public class PlayerBaseScript : MonoBehaviour
{

    public BulletType playerTypeMode = BulletType.Green;
    int damage_point = 1;
    public GameObject projectilePrefab;
    [Header("Config")]
    public Transform muzzlePosition;
    public Vector3 projectile_Placement = Vector3.zero;

    public Transform enemyTransform = null;

    public bool can_shoot = true;


   public void EnemyDetected(Transform target)
    {
        enemyTransform = target;

        StartCoroutine("loopShoot");
    }

    IEnumerator loopShoot()
    {
        if (enemyTransform != null) 
            InvokeRepeating("ShootBullet", 0f, 1f);

        yield return new WaitForSeconds(1.0f);
    }
   public void ShootBullet()
    {
            Debug.Log("Shoot");
            Vector3 spawnPos = muzzlePosition.position + projectile_Placement;

            GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            projectileScript p = proj.GetComponent<projectileScript>();

            p.shootProjectile(muzzlePosition);

            proj.name = "pea";


    }
}
