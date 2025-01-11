using UnityEngine;

public class RecyclingDialogueListener : MonoBehaviour
{
    [Header("References")]
    public RecyclingBin recyclingBin; // Reference to our RecyclingBin script
    public DialogueManager dialogueManager; // Reference to our DialogueManager

    [Header("Dialogue Sequence")]
    [TextArea]
    public string[] recyclingDialogue; // Lines of dialogue to display

    private int lastRecyclePoints = 0;
    
    // This tracks if the script played already. Doesn't trigger dialogue if the case, on scene reload
    private static bool hasPlayedDialogue = false; // This will persist across scenes

    private void Start()
    {
        if (recyclingBin == null)
        {
            Debug.LogError("RecyclingBin reference is not set!.");
            return; // Exit early to avoid null references
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager reference is not set!");
            return; // Same like above
        }

        lastRecyclePoints = recyclingBin.RecyclePoitns;
    }

    private void Update()
    {
        if (recyclingBin == null || dialogueManager == null || recyclingDialogue.Length == 0 || hasPlayedDialogue) return;

        if (recyclingBin.RecyclePoitns > lastRecyclePoints)
        {
            lastRecyclePoints = recyclingBin.RecyclePoitns; // Updates the tracked points
            TriggerRecyclingDialogue();
        }
    }

    private void TriggerRecyclingDialogue()
    {
        dialogueManager.StartDialogue(recyclingDialogue); // Triggers the dialogue
        hasPlayedDialogue = true; // Marks the dialogue as played

        Debug.Log("Recycling dialogue triggered.");
    }

    public static bool HasDialoguePlayed()
    {
        return hasPlayedDialogue;
    }
}
