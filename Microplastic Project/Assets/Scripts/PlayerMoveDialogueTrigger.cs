using UnityEngine;

public class PlayerMoveDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    public Transform playerTransform; // Reference to the player's Transform
    [TextArea(3, 10)] public string[] moveDialogue; // Dialogue lines for movement event

    private Vector3 lastPosition; // Tracks the player's previous position
    private bool hasMoved = false; // Tracks if the player has moved
    private float moveTimer = 0f; // Timer to count delay after movement
    private bool dialogueTriggered = false; // Ensures dialogue triggers only once

    [SerializeField] private float movementThreshold = 0.1f; // Minimum distance to detect movement

    void Start()
    {
        // Record the initial position of the player
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player not found! Ensure the Player has the correct tag or is assigned in the Inspector.");
                return;
            }
        }

        lastPosition = playerTransform.position;
    }

    void Update()
    {
        if (dialogueTriggered || playerTransform == null) return; // Exit if dialogue is already triggered or playerTransform is missing

        // Check if the player has moved by comparing positions
        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);
        if (!hasMoved && distanceMoved > movementThreshold)
        {
            hasMoved = true; // Mark that the player has moved
        }

        // Update the player's last known position
        lastPosition = playerTransform.position;

        // If the player has moved, start counting down the timer
        if (hasMoved)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= 2f) // 2-second delay
            {
                TriggerMoveDialogue();
            }
        }
    }

    private void TriggerMoveDialogue()
    {
        dialogueManager.StartDialogue(moveDialogue); // Start the movement dialogue
        dialogueTriggered = true; // Prevent re-triggering
    }
}
