using System.Collections;
using UnityEngine;

public class EnemyRespawnDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager;
    [TextArea(3, 10)]
    public string[] respawnDialogue; // Dialogue to trigger when respawn is triggered

    private SpawningManager spawningManager;
    private bool _hasTriggeredRespawnDialogue = false; // dialogue triggers only once
    private bool _hasStartedRespawn = false;

    void Start()
    {
        spawningManager = FindObjectOfType<SpawningManager>();
        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager not found!");
        }
    }

    void Update()
    {
        if (spawningManager != null)
        {
            if (!_hasStartedRespawn && spawningManager.IsSpawning && spawningManager.CheckIfAllCatched())
            {
                _hasStartedRespawn = true;

                if (dialogueManager != null && respawnDialogue.Length > 0 && !_hasTriggeredRespawnDialogue)
                {
                    Debug.Log("Triggering respawn dialogue!");
                    dialogueManager.StartDialogue(respawnDialogue);
                    _hasTriggeredRespawnDialogue = true;
                }
            }

            if (!spawningManager.IsSpawning && _hasStartedRespawn)
            {
                _hasTriggeredRespawnDialogue = false;
                _hasStartedRespawn = false;
            }
        }
    }
}
