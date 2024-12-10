using UnityEngine;

public class ItemReplacementDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    [TextArea(3, 10)] 
    public string[] replacementDialogue; // Dialogue to trigger when item is replaced

    private SpawningManager spawningManager;
    private bool dialogueTriggered = false; // Ensures the dialogue is only triggered once

    void Start()
    {
        // Find SpawningManager in the scene
        spawningManager = FindObjectOfType<SpawningManager>();

        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager not found in the scene.");
        }
    }

    void Update()
    {
        // Check if the item has been replaced and dialogue hasn't been triggered yet
        if (spawningManager.HasReplaced && !dialogueTriggered)
        {
            TriggerReplacementDialogue();
        }
    }

    private void TriggerReplacementDialogue()
    {
        // Trigger the dialogue when HasReplaced is true
        if (dialogueManager != null && replacementDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(replacementDialogue);
            dialogueTriggered = true; // Ensure the dialogue plays only once
        }
        else
        {
            Debug.LogError("DialogueManager not assigned or replacementDialogue is empty.");
        }
    }
}
