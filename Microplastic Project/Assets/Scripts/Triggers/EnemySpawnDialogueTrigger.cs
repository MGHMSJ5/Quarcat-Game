using UnityEngine;
using System.Collections; // Required for IEnumerator and coroutines

public class EnemySpawnDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    [TextArea(3, 10)] 
    public string[] spawnDialogue; // Dialogue to trigger when enemies spawn

    private SpawningManager spawningManager;

    void Start()
    {
        // Find the SpawningManager in the scene
        spawningManager = FindObjectOfType<SpawningManager>();

        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager not found in the scene.");
            return;
        }

        // Start listening for spawning
        StartCoroutine(TriggerDialogueOnSpawn());
    }

    private IEnumerator TriggerDialogueOnSpawn()
    {
        // Wait until spawning starts
        while (!spawningManager.IsSpawning)
        {
            yield return null; // Wait for the next frame
        }

        // Trigger dialogue once spawning begins
        if (dialogueManager != null && spawnDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(spawnDialogue);
        }
        else
        {
            Debug.LogError("DialogueManager not assigned or spawnDialogue is empty.");
        }
    }
}