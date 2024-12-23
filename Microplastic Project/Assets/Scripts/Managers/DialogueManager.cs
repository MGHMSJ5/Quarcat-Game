using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button nextArrowButton; // Reference to the arrow button

    private string[] dialogueSequence;
    private int currentLineIndex;
    private bool isDialogueActive;

    private float originalTimeScale;

    [Header("Gameplay Input Settings")]
    public GameObject inputManager; // Reference to input manager
    public Joystick joystick;       // Reference to the joystick component

    private void Start()
    {
        if (nextArrowButton != null)
        {
            nextArrowButton.onClick.AddListener(ProgressDialogue);
        }
    }

    public void StartDialogue(string[] sequence)
    {
        joystick.ResetInput();

        isDialogueActive = true;
        dialogueSequence = sequence;
        currentLineIndex = 0;

        // Pause game time when dialogue box is open
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (inputManager != null)
        {
            inputManager.SetActive(false); // Disable input systems
        }

        dialogueBox.SetActive(true);
        DisplayText(dialogueSequence[currentLineIndex]);

        // Enable the arrow button
        if (nextArrowButton != null)
        {
            nextArrowButton.gameObject.SetActive(true);
        }
    }

    private void ProgressDialogue()
    {
        if (!isDialogueActive) return;

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

        // Resume game time and re-enable gameplay input
        Time.timeScale = originalTimeScale;

        if (inputManager != null)
        {
            inputManager.SetActive(true);
        }

        // Hide the dialogue box and arrow button
        dialogueBox.SetActive(false);
        if (nextArrowButton != null)
        {
            nextArrowButton.gameObject.SetActive(false);
        }
    }

    private void ResetJoystick()
    {
        if (joystick != null)
        {
            // this a reference to our Joystick Pack
            RectTransform handle = joystick.transform.GetChild(0) as RectTransform;
            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero; // Resets the joystick position prior to entering dialogue
            }
        }
    }
}
