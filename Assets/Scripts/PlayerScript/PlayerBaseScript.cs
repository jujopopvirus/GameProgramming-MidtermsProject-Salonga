using System.Collections;
using UnityEngine;

public enum BulletType
{
    Green,
    Red,
    Blue,
    Yellow
}

public class PlayerBaseScript : MonoBehaviour, ISwitchables
{
    public enum PlayerState
    {
        Idle,
        Shoot,
        Battle
    }

    
    public int damage_point = 1;
    public GameObject projectilePrefab;

    [Header("Config")]
    public Transform muzzlePosition;
    public Vector3 projectile_Placement = Vector3.zero;
    public Transform enemyTransform = null;
    public PlayerState curState = PlayerState.Idle;
    [SerializeField] private Animator animator;

    [Header("Plant Type")]
    public BulletType playerTypeMode = BulletType.Green;
    private int TypeIndex = 0;
    [SerializeField] private Material[] plantTypesMaterials;
    [SerializeField] private SkinnedMeshRenderer[] plantBodyMaterial;


    private Coroutine shootCoroutine;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    public void switchColor(GameObject interactor, BulletType typeColor)
    {
        playerTypeMode = typeColor;
        switch (typeColor)
        {
            case BulletType.Green:
                ChangePlantMaterial(plantTypesMaterials[0]);
                break;
            case BulletType.Blue:
                ChangePlantMaterial(plantTypesMaterials[1]);
                break;
            case BulletType.Red:
                ChangePlantMaterial(plantTypesMaterials[2]);
                break;
            case BulletType.Yellow:
                ChangePlantMaterial(plantTypesMaterials[3]);
                break;
        }
    }
    void OnMouseDown()
    {
        if (TypeIndex < plantTypesMaterials.Length - 1)
        {
            TypeIndex += 1;
         
            switch (TypeIndex)
            {
                case 0:
                    switchColor(gameObject, BulletType.Green);
                    break;
                case 1:
                    switchColor(gameObject, BulletType.Blue);
                    break;
                case 2:
                    switchColor(gameObject, BulletType.Red);
                    break;
                case 3:
                    switchColor(gameObject, BulletType.Yellow);
                    break;
            }
        }
        else
        {
            TypeIndex = 0;
            switchColor(gameObject, BulletType.Green);

        }
    }
    void ChangePlantMaterial(Material mat)
    {
        foreach (SkinnedMeshRenderer b in plantBodyMaterial)
        {
            Material[] mats = b.materials;

            mats[0] = mat;

            b.materials = mats;
        }
    }

    public void EnemyDetected(Transform target)
    {
        enemyTransform = target;
        curState = PlayerState.Shoot;

        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        shootCoroutine = StartCoroutine(LoopShoot());
    }

    public void EnemyCleared()
    {
        enemyTransform = null;
        curState = PlayerState.Idle;
        animator.SetBool("IsShooting", false);

        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    IEnumerator LoopShoot()
    {
        while (enemyTransform != null)
        {
            ShootBullet();
            animator.SetBool("IsShooting", true);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void ShootBullet()
    {
        if (enemyTransform == null) return;

        Debug.Log("Shoot");
        Vector3 spawnPos = muzzlePosition.position + projectile_Placement;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        projectileScript p = proj.GetComponent<projectileScript>();

        p.shootProjectile(muzzlePosition);
        p.ChangeBulletType(playerTypeMode);
        p.damage = damage_point;

        curState = PlayerState.Battle;
    }
}
