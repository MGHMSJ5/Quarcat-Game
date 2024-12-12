using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private string[] dialogueSequence;
    private int currentLineIndex;
    private bool isDialogueActive;

    private float originalTimeScale;

    // Put Joystick here
    [Header("Gameplay Input Settings")]
    public GameObject inputManager; 

    public void StartDialogue(string[] sequence)
    {
        isDialogueActive = true;
        dialogueSequence = sequence;
        currentLineIndex = 0;

        // Pauses game time when dialogue box open
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (inputManager != null)
        {
            inputManager.SetActive(false); // Disables input systems
        }

        dialogueBox.SetActive(true);
        DisplayText(dialogueSequence[currentLineIndex]);
    }

    void Update()
    {
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || IsScreenTapped()))
        {
            ProgressDialogue();
        }
    }

    private bool IsScreenTapped()
    {
        // Detect screen tap for mobile devices
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private void ProgressDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueSequence.Length)
        {
            DisplayText(dialogueSequence[currentLineIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayText(string text)
    {
        dialogueText.text = text;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;

        // Resume game times and re-enable gameplay input
        Time.timeScale = originalTimeScale;

        if (inputManager != null)
        {
            inputManager.SetActive(true);
        }

        dialogueBox.SetActive(false);
    }
}
