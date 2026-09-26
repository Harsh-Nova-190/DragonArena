using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Dragons")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health aiHealth;

    [Header("Winner UI")]
    [SerializeField] private WinnerScreen winnerScreen;

    private bool battleEnded;

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.OnDeath += OnPlayerDeath;

        if (aiHealth != null)
            aiHealth.OnDeath += OnAIDeath;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDeath -= OnPlayerDeath;

        if (aiHealth != null)
            aiHealth.OnDeath -= OnAIDeath;
    }

    private void OnPlayerDeath()
    {
        if (battleEnded)
            return;

        battleEnded = true;

        ShowWinner("AI Dragon");
    }

    private void OnAIDeath()
    {
        if (battleEnded)
            return;

        battleEnded = true;

        ShowWinner("Player Dragon");
    }

    private void ShowWinner(string winnerName)
    {
        if (winnerScreen != null)
        {
            winnerScreen.ShowWinner(winnerName);
        }
    }

    public void RestartBattle()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}