using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [Header("Fade")]
    public FadeCanvas fadeCanvas;
    public void ReturnToMainMenu()
    {
        StartCoroutine(WaitForFade());
    }
    private IEnumerator WaitForFade()
    {
        fadeCanvas.StartFadeIn();
        print("Begin");
        yield return new WaitForSeconds(fadeCanvas.defaultDuration + 0.4f);
        print("After");

        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
