using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMoveScript : MonoBehaviour
{
    public Transform playerPosition;
    public float TimeToMove = 16f;
    private float elapsedTime;
    EnemyBase en;

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
        enabled = false;
    }
    private void Awake()
    {
        en = gameObject.GetComponent<EnemyBase>();
        GameManager.OnGameOver += GameOver;
    }
    public void Update()
    {
        if (playerPosition == null) MoveToPlayer(transform.position, new Vector3(0, 2f, 0));

        MoveToPlayer(transform.position, playerPosition.position);
    }
    public void MoveToPlayer(Vector3 startPos, Vector3 toPos)
    {
            Vector3 targetPosition = toPos;
            startPos.y = toPos.y;

            transform.LookAt(toPos);    

            elapsedTime += Time.deltaTime * 0.01f;
            float t = elapsedTime / TimeToMove;

            transform.position = Vector3.Lerp(startPos, targetPosition, t);

            float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
            float reachT = 5.0f;

            if (distanceToPlayer <= reachT)
            {
                transform.position = targetPosition;
                GameOver();
            }
    }
}

