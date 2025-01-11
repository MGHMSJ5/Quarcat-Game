using System.Collections;
using UnityEngine;

public class SpawnDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] public string[] spawnDialogue;

    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        dialogueManager.StartDialogue(spawnDialogue);
    }
}