using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyCounter : MonoBehaviour
{
    public TMP_Text counterText;
    public TMP_Text youWinText;
    public int totalEnemies = 17;

    private int enemiesKilled = 0;

    void Start()
    {
        youWinText.gameObject.SetActive(false);
        UpdateText();
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateText();

        if (enemiesKilled >= totalEnemies)
        {
            Win();
        }
    }

    void UpdateText()
    {
        counterText.text = "Enemies defeated: " + enemiesKilled + " / " + totalEnemies;
    }

    void Win()
    {
        youWinText.gameObject.SetActive(true);
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(6f); // espera 6 segundos
        SceneManager.LoadScene("Menu");
    }
}
