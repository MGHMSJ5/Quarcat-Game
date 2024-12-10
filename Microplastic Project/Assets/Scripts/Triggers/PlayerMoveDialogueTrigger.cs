using UnityEngine;

public class PlayerMoveDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Transform playerTransform;
    [TextArea(3, 10)] public string[] moveDialogue;

    private Vector3 lastPosition;
    private bool hasMoved = false;
    private float moveTimer = 0f;
    private bool dialogueTriggered = false;

    [SerializeField] private float movementThreshold = 0.1f;

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player not found!");
                return;
            }
        }

        lastPosition = playerTransform.position;
    }

    void Update()
    {
        if (dialogueTriggered || playerTransform == null) return;

        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);
        if (!hasMoved && distanceMoved > movementThreshold)
        {
            hasMoved = true;
        }

        lastPosition = playerTransform.position;

        if (hasMoved)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= 2f)
            {
                TriggerMoveDialogue();
            }
        }
    }

    private void TriggerMoveDialogue()
    {
        dialogueManager.StartDialogue(moveDialogue);
        dialogueTriggered = true;
    }
}
