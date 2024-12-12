using UnityEngine;
using System.Collections;

public class EnemySpawnDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] 
    public string[] spawnDialogue; // Dialogue to trigger when enemies spawn

    private SpawningManager spawningManager;

    void Start()
    {
        spawningManager = FindObjectOfType<SpawningManager>();

        if (spawningManager == null)
        {
            Debug.LogError("SpawningManager was not found in the scene!");
            return;
        }

        StartCoroutine(TriggerDialogueOnSpawn());
    }

    private IEnumerator TriggerDialogueOnSpawn()
    {
        
        while (!SpawnBoolManager.GetIsSpawning(spawningManager.identity))
        {
            yield return null;
        }

        // Triggers dialogue when enemies spawn
        if (dialogueManager != null && spawnDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(spawnDialogue);
        }
        else
        {
            Debug.LogError("spawnDialogue is empty! Add dialogue!!!");
        }
    }
}