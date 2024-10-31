using UnityEngine;

public class Replacing : MonoBehaviour
{
    public GameObject originalGameObject;
    public GameObject replaceGameObject;

    public void ReplaceItem()
    {   //gameobject will be assigned through SpawningManager script
        originalGameObject.SetActive(false);
        replaceGameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
