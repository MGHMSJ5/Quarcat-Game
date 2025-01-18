using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class WinConditionTrigger : MonoBehaviour
{
    [Header("References")]
    public RecyclingBin recyclingBin;
    public DialogueManager dialogueManager;

    [Header("Cooldown Timer")]
    public float cooldownDuration = 2f;

    [Header("Dialogue Sequence")]
    [TextArea]
    public string[] winCondition0Dialogue;
    [TextArea]
    public string[] winCondition1Dialogue;
    [TextArea]
    public string[] winCondition2Dialogue;
    [TextArea]
    public string[] winCondition3Dialogue;
    [TextArea]
    public string[] winCondition4Dialogue;
    [TextArea]
    public string[] winConditionCompletedDialogue;

    private int recyclePoints;
    private bool canTrigger = true;

    private IEnumerator Cooldown()
    {
        canTrigger = false;
        yield return new WaitForSeconds(cooldownDuration);
        canTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger && other.CompareTag("Player"))
        {
            recyclePoints = recyclingBin.RecyclePoitns;
            if (recyclePoints == 0)
            {
                dialogueManager.StartDialogue(winCondition0Dialogue);
            }
            if (recyclePoints == 1)
            {
                dialogueManager.StartDialogue(winCondition1Dialogue);
            }
            if (recyclePoints == 2)
            {
                dialogueManager.StartDialogue(winCondition2Dialogue);
            }
            if (recyclePoints == 3)
            {
                dialogueManager.StartDialogue(winCondition3Dialogue);
            }
            if (recyclePoints == 4)
            {
                dialogueManager.StartDialogue(winCondition4Dialogue);
            }
            if (recyclePoints == 5)
            {
                dialogueManager.StartDialogue(winConditionCompletedDialogue);
            }
            StartCoroutine(Cooldown());
        }
    }
}
