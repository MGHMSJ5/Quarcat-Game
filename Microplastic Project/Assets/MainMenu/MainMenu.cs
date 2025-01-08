using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;    // The main menu UI
    public GameObject optionsPanel;    // The options menu UI
    public GameObject creditsPanel;    // The credits menu UI

    public void PlayGame()
    {
        SceneManager.LoadScene(6); 
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit(); 
    }

    public void OpenOptions()
    {
        // Show the options menu and hide the main menu
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        // Show the credits menu and hide the main menu
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        // Hide both the options and credits menus, show the main menu
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void AdjustSound(float value)
    {
        // Adjust the global sound volume
        Debug.Log("Sound adjusted to: " + value);
        AudioListener.volume = value;
    }
}