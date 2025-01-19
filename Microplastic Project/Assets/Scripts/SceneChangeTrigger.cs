using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeTrigger : MonoBehaviour
{
    [Header("Scene Change Settings")]
    [Tooltip("The name of the scene to load when the player enters the trigger.")]
    [SerializeField] private string sceneName;

    [Tooltip("Tag to identify the player object.")]
    [SerializeField] private string playerTag = "Player";

    [Header("Black Fade Reference")]
    [SerializeField]
    private FadeCanvas _fadeCanvas;

    [Header("Recycle Object")]
    [SerializeField]
    private GameObject playerHead;
    [SerializeField]
    private string _moveTag = "SceneMoved";

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationAmount;
    [SerializeField] private float rotationDuration = 1.0f;

    [Header("UI Settings")]
    [SerializeField] private GameObject buttonPrefab;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorAudio;

    [Header("Blanket Fix")]
    [Tooltip("Place 'SpawnArea' of the Blanket Source here to fix the weird Beadie issue")]
    [SerializeField] private GameObject _spawningManager;

    private GameObject instantiatedButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            
            // Check if the current scene is "Working_Hallway"
            if (SceneManager.GetActiveScene().name == "Working_Hallway")
            {
                // Prevent scene change if dialogue hasn't been played
                if (!RecyclingDialogueListener.HasDialoguePlayed())
                {
                    Debug.Log("You must first complete the recycling dialogue before changing scenes!");
                    return; // Exit early if the dialogue hasn't been played
                }
            }

            if (buttonPrefab != null && instantiatedButton == null)
            {
                // Instantiate the button and set it up
                instantiatedButton = Instantiate(buttonPrefab, transform);
                instantiatedButton.GetComponentInChildren<Button>().onClick.AddListener(() => StartCoroutine(RotateAndChangeScene()));
                instantiatedButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && instantiatedButton != null)
        {
            Destroy(instantiatedButton);
        }
    }

    IEnumerator RotateAndChangeScene()
    {
        // Perform rotation
        yield return StartCoroutine(RotateObject());

        if (_spawningManager)
        {
            _spawningManager.SetActive(false);
        }
        // Fade and transition scene
        _fadeCanvas.StartFadeIn();
        yield return new WaitForSeconds(_fadeCanvas.defaultDuration + 0.4f); // Add tiny delay to scene switching

        // Set current scene
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;

        // Wait until the scene is fully loaded
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                // Allow scene activation when loading is done
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        playerHead.transform.parent = null;
        playerHead.tag = _moveTag;
        Debug.Log(playerHead);
        SceneManager.MoveGameObjectToScene(playerHead, SceneManager.GetSceneByName(sceneName));

        // Unload the current scene
        SceneManager.UnloadSceneAsync(currentScene);
    }

    IEnumerator RotateObject()
    {
        audioSource.PlayOneShot(doorAudio);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount);
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Ensure final rotation is set
    }
}
