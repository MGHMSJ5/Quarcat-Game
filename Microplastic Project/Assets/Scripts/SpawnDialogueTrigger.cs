using UnityEngine;

public class SpawnDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] public string[] spawnDialogue;

    void Start()
    {
        dialogueManager.StartDialogue(spawnDialogue);
    }
}
