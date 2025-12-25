using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Inicia un juego nuevo
    public void StartGame()
    {
        // Borra cualquier dato guardado y comienza el juego
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Game");
    }
}

