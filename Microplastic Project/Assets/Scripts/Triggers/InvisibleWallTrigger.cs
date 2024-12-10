using UnityEngine;

public class InvisibleWallTrigger : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    public string[] noVacuumDialogue; // Dialogue to show when vacuum isn't equipped

    public GameObject separateInvisibleWall; // Reference to the invisible wall

    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        
        if (separateInvisibleWall != null)
        {
            separateInvisibleWall.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the vacuum is equipped or not
            if (!VacuumStateManager.IsVacuumEquipped)
            {
                // Trigger the dialogue if the vacuum is not equipped
                if (_dialogueManager != null && noVacuumDialogue.Length > 0)
                {
                    _dialogueManager.StartDialogue(noVacuumDialogue);
                }
            }
            else
            {
                // If the player has the vacuum, destroy the invisible wall
                if (separateInvisibleWall != null)
                {
                    Destroy(separateInvisibleWall); // Destroy the invisible wall
                }
            }
        }
    }
}
