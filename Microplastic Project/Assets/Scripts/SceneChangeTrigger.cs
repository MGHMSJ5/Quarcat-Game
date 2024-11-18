using System.Collections;
using Unity.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                StartCoroutine(ChangeScene());
            }
            else
            {
                Debug.LogError("Invalid scene name!");
            }
        }
    }

    IEnumerator ChangeScene()
    {
        _fadeCanvas.StartFadeIn();
        yield return new WaitForSeconds(_fadeCanvas.defaultDuration + 0.4f); //add tiny delay to scene switching

        //set current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //load the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;


        //wait until the scene is fully loaded
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                //allow scene activation when loading is done
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;            
        }
        
        playerHead.transform.parent = null;
        playerHead.tag = _moveTag;
        SceneManager.MoveGameObjectToScene(playerHead, SceneManager.GetSceneByName(sceneName));

        //unload the current scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
