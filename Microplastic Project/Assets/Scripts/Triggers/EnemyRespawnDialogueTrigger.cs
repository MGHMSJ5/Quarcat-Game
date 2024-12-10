using System.Collections;
using UnityEngine;

public class EnemyRespawnDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager;
    [TextArea(3, 10)]
    public string[] respawnDialogue; // Dialogue to trigger when respawn is triggered

    private SpawningManager spawningManager;
    private bool _hasTriggeredRespawnDialogue = false; // Ensure dialogue triggers only once per respawn
    private bool _hasStartedRespawn = false; // Track if the respawn process has started

    void Start()
    {
        spawningManager = FindObjectOfType<SpawningManager>();
        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager not found in the scene.");
        }
    }

    void Update()
    {
        if (spawningManager != null)
        {
            // Check if respawn process has started and enemies are being respawned
            if (!_hasStartedRespawn && spawningManager.IsSpawning && spawningManager.CheckIfAllCatched())
            {
                _hasStartedRespawn = true; // Mark the respawn process as started

                // Trigger the respawn dialogue only once
                if (dialogueManager != null && respawnDialogue.Length > 0 && !_hasTriggeredRespawnDialogue)
                {
                    Debug.Log("Triggering respawn dialogue.");
                    dialogueManager.StartDialogue(respawnDialogue);
                    _hasTriggeredRespawnDialogue = true; // Ensure it triggers only once
                }
            }

            // Reset dialogue trigger after respawn is complete and new enemies are spawned
            if (!spawningManager.IsSpawning && _hasStartedRespawn)
            {
                _hasTriggeredRespawnDialogue = false; // Reset flag for next respawn
                _hasStartedRespawn = false; // Reset the respawn tracking
            }
        }
    }
}
