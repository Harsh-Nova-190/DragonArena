using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Dragons")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health aiHealth;

    [Header("Winner UI")]
    [SerializeField] private WinnerScreen winnerScreen;

    [Header("Victory Delay")]
    [SerializeField] private float winnerDelay = 2f;

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
        StartCoroutine(ShowWinnerAfterDelay("AI Dragon"));
    }

    private void OnAIDeath()
    {
        if (battleEnded)
            return;

        battleEnded = true;
        StartCoroutine(ShowWinnerAfterDelay("Player Dragon"));
    }

    private IEnumerator ShowWinnerAfterDelay(string winnerName)
    {
        yield return new WaitForSeconds(winnerDelay);

        ShowWinner(winnerName);
    }

    private void ShowWinner(string winnerName)
    {
        if (winnerScreen != null)
            winnerScreen.ShowWinner(winnerName);
    }

    public void RestartBattle()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}