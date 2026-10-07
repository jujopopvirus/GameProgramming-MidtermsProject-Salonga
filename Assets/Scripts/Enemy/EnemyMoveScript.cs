using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    public Transform playerPosition;
    public float TimeToMove = 16f;
    private float elapsedTime;


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
            float t = elapsedTime / TimeToMove;

            transform.position = Vector3.Lerp(startPos, targetPosition, t);

            float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
            float reachT = 5.0f;

            if (distanceToPlayer <= reachT)
            {
                transform.position = targetPosition;

                Debug.Log("Enemy Reached!");
                Destroy(gameObject);
            }

        }
        else
        {
            Debug.Log("No player found");
        }
    }
}

