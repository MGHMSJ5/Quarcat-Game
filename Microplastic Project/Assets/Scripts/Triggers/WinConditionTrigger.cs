using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinConditionTrigger : MonoBehaviour
{
    [Header("References")]
    public RecyclingBin recyclingBin;
    public DialogueManager dialogueManager;
    [SerializeField]
    private FadeCanvas fadeCanvas;

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
    private bool lastDialogueAppear = false;

    private void Update()
    {
        //look for it time is normal, and the last dialogue has appeared
        if (Time.timeScale == 1 && lastDialogueAppear)
        {
            StartCoroutine(WaitForFade());
            lastDialogueAppear = false; //make it false so that this won't loop
        }
    }

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
            StartCoroutine(Cooldown());
            if (recyclePoints >= 5)
            {
                dialogueManager.StartDialogue(winConditionCompletedDialogue);
                {
                    lastDialogueAppear = true;
                }
            }
        }
    }
    private IEnumerator WaitForFade()
    {
        fadeCanvas.StartFadeIn();
        print("Begin");
        yield return new WaitForSeconds(fadeCanvas.defaultDuration + 0.4f);
        print("After");

        SceneManager.LoadScene(6);
    }
}
