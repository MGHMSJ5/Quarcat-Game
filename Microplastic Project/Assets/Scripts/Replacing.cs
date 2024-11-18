using UnityEngine;

public class Replacing : MonoBehaviour
{
    public GameObject originalGameObject;
    public GameObject replaceGameObject;

    public SpawningManager spawningManager;

    public void ReplaceItem()
    {   //gameobject & script reference will be assigned through SpawningManager script
        originalGameObject.SetActive(false);
        replaceGameObject.SetActive(true);
        spawningManager.hasReplaced = true;
        ResetVariables();
        gameObject.SetActive(false);
    }

    private void ResetVariables()
    {
        originalGameObject = null;
        replaceGameObject = null;
        spawningManager = null;
    }
}
