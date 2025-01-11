using UnityEngine;

public class DialogueTriggerOnce : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string[] dialogueSequence;
    public DialogueManager dialogueManager;  // Reference to our DialogueManager

    // Static variable to track if dialogue has been played. Important!
    private static bool hasDialoguePlayed = false;

    private void Start()
    {
        // Checks if the dialogue has already been played
        if (!hasDialoguePlayed)
        {
            // If not alreayd played, starts the dialogue with a 1-second delay
            Invoke("TriggerDialogue", 1f);
        }
    }

    private void TriggerDialogue()
    {
        if (dialogueManager != null)
        {
            // Starts the dialogue
            dialogueManager.StartDialogue(dialogueSequence);

            // Marks the dialogue as played so it doesn't trigger again
            hasDialoguePlayed = true;
        }
    }
}
