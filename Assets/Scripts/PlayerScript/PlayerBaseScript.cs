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
    public enum PlayerState 
    {
       Idle,
       Shoot,
       Battle
    }

    public BulletType playerTypeMode = BulletType.Green;
    int damage_point = 1;
    public GameObject projectilePrefab;
    [Header("Config")]
    public Transform muzzlePosition;
    public Vector3 projectile_Placement = Vector3.zero;

    public Transform enemyTransform = null;

    public PlayerState curState = PlayerState.Idle;

    public bool can_shoot = true;


   public void EnemyDetected(Transform target)
    {
        enemyTransform = target;
        

        StartCoroutine("loopShoot");
    }

    public void EnemyCleared()
    {
        enemyTransform = null;
        curState = PlayerState.Idle;
        StopCoroutine("loopShoot");
    }

    IEnumerator loopShoot()
    {

        if (enemyTransform != null)
        {
            Debug.Log(enemyTransform.gameObject.name);
            curState = PlayerState.Shoot;
            if (can_shoot)
            {
                InvokeRepeating("ShootBullet", 0f, 1f);
                can_shoot = false;
            }
            yield return new WaitForSeconds(1.0f);

            
            can_shoot = true;

        }
    }
   public void ShootBullet()
    {
        if (curState == PlayerState.Shoot && enemyTransform != null) 
        {
            Debug.Log("Shoot");
            Vector3 spawnPos = muzzlePosition.position + projectile_Placement;

            GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            projectileScript p = proj.GetComponent<projectileScript>();

            p.shootProjectile(muzzlePosition);

            proj.name = "pea";

            curState = PlayerState.Battle;
        }

    }
}
