using UnityEngine;

public interface IDamageable
{
    void damage(GameObject interactor, BulletType bullet, int damagePoint);

}
