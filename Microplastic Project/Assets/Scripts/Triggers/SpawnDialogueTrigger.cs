using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [TextArea(3, 10)] public string[] spawnDialogue;
    private static bool hasPlayedInWorkingHallway = false;

    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);

        // Check if we are in the "WorkingHallway" scene and if the dialogue has not played already
        if (SceneManager.GetActiveScene().name == "Working_Hallway")
        {
            if (!hasPlayedInWorkingHallway)
            {
                dialogueManager.StartDialogue(spawnDialogue);
                hasPlayedInWorkingHallway = true;
            }
        }
        else
        {
            // Plays dialogue every time in other scenes
            dialogueManager.StartDialogue(spawnDialogue);
        }
    }
}
