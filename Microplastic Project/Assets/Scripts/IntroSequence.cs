using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    [Header("Panels (in order)")]
    public GameObject[] panels; // Assign panels in the inspector (01, 02, etc.)

    [Header("Audio")]
    public AudioSource audioSource; // An AudioSource to play sounds
    public AudioClip[] panelSounds; // Sound clips for each panel

    private int currentPanelIndex = 0;

    private void Start()
    {
        // Ensure only the first panel is active, the rest are hidden.
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        // Play the first panel's sound if it exists
        PlayPanelSound(currentPanelIndex);
    }

    // Called when the Fullscreen button is pressed
    public void NextPanel()
    {
        // Hide current panel if it's active
        if (currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(false);
        }

        currentPanelIndex++;

        // Show the next panel if still within array
        if (currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(true);
        }
        else
        {
            // After last panel, load scene
            SceneManager.LoadScene(0);
        }
    
    }

    // Called when Skip button is pressed
    public void SkipCutscene()
    {
        // Immediately go to next scene
        SceneManager.LoadScene(0); 
    }

    private void PlayPanelSound(int panelIndex)
    {
        if (audioSource != null && panelSounds != null &&
            panelIndex < panelSounds.Length && panelSounds[panelIndex] != null)
        {
            audioSource.clip = panelSounds[panelIndex];
            audioSource.Play();
        }
    }
}