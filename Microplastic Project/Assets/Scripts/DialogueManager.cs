using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueBox; 
    public TextMeshProUGUI dialogueText; 

    private string[] dialogueSequence; 
    private int currentLineIndex; 
    private bool isDialogueActive; 

    public void StartDialogue(string[] sequence)
    {
        isDialogueActive = true;
        dialogueSequence = sequence;
        currentLineIndex = 0;
        dialogueBox.SetActive(true);
        DisplayText(dialogueSequence[currentLineIndex]);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            ProgressDialogue();
        }
    }

    private void ProgressDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueSequence.Length)
        {
            DisplayText(dialogueSequence[currentLineIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayText(string text)
    {
        dialogueText.text = text;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
    }
}
