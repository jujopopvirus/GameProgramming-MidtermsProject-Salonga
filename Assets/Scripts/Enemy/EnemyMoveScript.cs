using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    public AnimationCurve moveCurve;
    [HideInInspector] public BulletType Enemytype = BulletType.Green;
    public int hitpoints = 1;

     public Transform playerPosition;
    public float TimeOfArrival = 25.0f;
    private float elapsedTime;

    public void Update()
    {
        MoveToPlayer(transform.position);
    }
    public void MoveToPlayer(Vector3 startPos)
    {
        if (playerPosition != null)
        {
            elapsedTime += Time.deltaTime;

            float toa = TimeOfArrival * 100;

            float t = elapsedTime / toa;

            float curveM = moveCurve.Evaluate(t);

            transform.position = Vector3.Lerp(startPos, playerPosition.position, curveM);
        }
        else
        {
            Debug.Log("No player found");
        }
    }
}
