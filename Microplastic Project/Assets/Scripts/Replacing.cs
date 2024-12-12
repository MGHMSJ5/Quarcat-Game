using UnityEngine;

public class Replacing : MonoBehaviour
{
    [Header("Will be self referenced")]
    [Tooltip("These variables will be set through the SpawningManager script")]
    public GameObject originalGameObject;
    public GameObject replaceGameObject;

    public int spawnId;

    [Header("Recycling")]
    [Tooltip("Recicle bin that needs to be referenced manually " +
        "The script is in the trigger")]
    public RecyclingBin recyclingScript;

    public void ReplaceItem()
    {   //gameobject & script reference will be assigned through SpawningManager script
        originalGameObject.SetActive(false);
        replaceGameObject.SetActive(true);
        SpawnBoolManager.SetHasReplaced(spawnId, true);

        recyclingScript.GrabRecycleObject(originalGameObject);

        ResetVariables();
        gameObject.SetActive(false);
    }

    public void ResetVariables()
    {
        originalGameObject = null;
        replaceGameObject = null;
        spawnId = -1;
    }
}
