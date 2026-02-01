using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // --- MAIN MENU BUTTONS ---
    public void StartGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void OpenGallery()
    {
        SceneManager.LoadScene("Gallery");
    }

    // --- GALLERY & ENDING SCREEN BUTTONS ---
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // --- ENDING SCREEN SPECIALS ---
    public void PlayAgain()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}