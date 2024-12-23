using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button nextArrowButton; // Reference to the continue button
    public Image dialogueIcon; // Reference to the icon image

    private string[] dialogueSequence;
    private int currentLineIndex;
    private bool isDialogueActive;

    private float originalTimeScale;

    [Header("Gameplay Input Settings")]
    public GameObject inputManager; // Reference to input manager
    public Joystick joystick;       // Reference to the joystick component

    private Button[] allButtons; // An array to store all our buttons
    private Button[] disabledButtons; // Keeps track of temporarily disabled buttons while in dialogue

    public Sprite defaultIcon; // The icon to be displayed during the dialogue

    private void Start()
    {
        // Ensure the icon is visible for all dialogues.
        if (dialogueIcon != null && defaultIcon != null)
        {
            dialogueIcon.sprite = defaultIcon; // Set the default icon
            dialogueIcon.gameObject.SetActive(true); // Make sure the icon is visible
        }

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

        // Pauses game time when dialogue box is open
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (inputManager != null)
        {
            inputManager.SetActive(false); // Disables input systems
        }

        // Disables all buttons except the dialogue button
        DisableAllButtonsExcept(nextArrowButton);

        dialogueBox.SetActive(true);
        DisplayText(dialogueSequence[currentLineIndex]);

        // Enables the continue button
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

        // Restores the interactivity of all our buttons
        RestoreAllButtons();

        // Hides the dialogue box and continue button
        dialogueBox.SetActive(false);
        if (nextArrowButton != null)
        {
            nextArrowButton.gameObject.SetActive(false);
        }

        // Optionally, hide the icon at the end (if needed)
        // If you want the icon to disappear at the end of the dialogue, uncomment this line.
        // dialogueIcon.gameObject.SetActive(false);
    }

    private void DisableAllButtonsExcept(Button exceptionButton)
    {
        allButtons = FindObjectsOfType<Button>(); // Finds all our buttons in the scene
        disabledButtons = new Button[allButtons.Length];

        int index = 0;
        foreach (Button button in allButtons)
        {
            if (button != exceptionButton && button.interactable)
            {
                disabledButtons[index++] = button; // Tracks disabled buttons
                button.interactable = false; // Disables button interactivity
            }
        }
    }

    private void RestoreAllButtons()
    {
        foreach (Button button in disabledButtons)
        {
            if (button != null)
            {
                button.interactable = true; // Restores button interactivity
            }
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
