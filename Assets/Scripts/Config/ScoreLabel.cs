using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreLable : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scoreLabels;
    [SerializeField] private CanvasGroup mainMenuHud;

    private void Awake()
    {
        GameManager.OnGameOver += showMainMenu;
    }
    private void Update()
    {
        int score = GameManager.Instance.currentScore;

        foreach (TMP_Text scoreLabel in scoreLabels)
        {
            scoreLabel.text = ("Score : " + score);
        }
    }

    public void showMainMenu()
    {
        mainMenuHud.enabled = true;
        mainMenuHud.interactable = true;
        mainMenuHud.blocksRaycasts = true;
    }

    private void closeMainMenu()
    {
        mainMenuHud.enabled = false;
        mainMenuHud.interactable = false;
        mainMenuHud.blocksRaycasts = false;
    }

    public void returnGame()
    {
        SceneManager.LoadScene("PlayScene");
        GameManager.Instance.currentScore = 0;
    }
    public void RestartGame()
    {
        GameManager.Instance.ReloadCurrentScene();
        closeMainMenu();
    }
}
