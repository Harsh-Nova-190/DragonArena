using UnityEngine;
using TMPro;

public class WinnerScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text winnerText;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowWinner(string winnerName)
    {
        if (panel != null)
            panel.SetActive(true);

        if (winnerText != null)
        {
            winnerText.text = winnerName + "\nWINS!";
        }
    }
}