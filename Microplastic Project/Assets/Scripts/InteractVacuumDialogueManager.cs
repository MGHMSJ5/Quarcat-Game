using UnityEngine;

public class InteractVacuumDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] public string[] vacuumDialogue;

    private InteractVacuumCleaner vacuumCleaner;

    void Awake()
    {
        vacuumCleaner = FindObjectOfType<InteractVacuumCleaner>();
        
        if (vacuumCleaner != null)
        {
            vacuumCleaner.GetComponent<BasicInteractLayout>()._interactAction += TriggerDialogue;
        }
        else
        {
            Debug.LogError("InteractVacuumCleaner script is not found in the scene.");
        }
    }

    private void TriggerDialogue()
    {
        if (vacuumCleaner != null)
        {
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(vacuumDialogue);
            }
            else
            {
                Debug.LogError("DialogueManager is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("InteractVacuumCleaner object is destroyed, cannot trigger dialogue.");
        }
    }

    void OnDestroy()
    {
        if (vacuumCleaner != null)
        {
            vacuumCleaner.GetComponent<BasicInteractLayout>()._interactAction -= TriggerDialogue;
        }
    }
}
