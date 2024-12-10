using UnityEngine;

public class ItemReplacementDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] 
    public string[] replacementDialogue;

    [Header("Object Settings")]
    public GameObject objectToActivate; // Add here the object to activate to take you to next scene

    private SpawningManager spawningManager;
    private bool dialogueTriggered = false;

    void Start()
    {
        spawningManager = FindObjectOfType<SpawningManager>();

        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager not found !");
        }
    }

    void Update()
    {
        // Checks if item replaced
        if (spawningManager.HasReplaced && !dialogueTriggered)
        {
            TriggerReplacementDialogue();
        }
    }

    private void TriggerReplacementDialogue()
    {
        // Trigger dialogue when HasReplaced is true
        if (dialogueManager != null && replacementDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(replacementDialogue);
            dialogueTriggered = true;
            
            // Activate object when/after the dialogue is triggered
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogError("Object to activate missing!");
            }
        }
        else
        {
            Debug.LogError("replacementDialogue is empty!");
        }
    }
}
