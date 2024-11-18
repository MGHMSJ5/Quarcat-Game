using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingBin : MonoBehaviour
{
    public List<GameObject> recycledObjects = new List<GameObject>();

    [Header("Reference")]
    [Tooltip("playerHead is the GameObject where the new recycleObject will spawn under." +
        "This GameObject needs to be under the player GameObject")]
    public GameObject playerHead;
    public GameObject recycleButton;

    private float _offset = 0f;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (recycledObjects.Count != 0)
            {
                recycleButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (recycleButton.activeInHierarchy)
            {
                recycleButton.SetActive(false);
            }
        }
    }

    public void RecycleObjects()
    {
        for (int i = 0; i < recycledObjects.Count; i++)
        {
            Destroy(recycledObjects[i]);
            _offset = 0f;
            recycleButton.SetActive(false);
        }
    }

    //function that'll add the object on the player's head, and add it to list
    public void GrabRecycleObject(GameObject recycleObject)
    {
        Vector3 playerHeadPosition = new Vector3(
            playerHead.transform.position.x,
            playerHead.transform.position.y + _offset,
            playerHead.transform.position.z);
        GameObject newRecycleObject = Instantiate(recycleObject, playerHeadPosition, Quaternion.identity);
        newRecycleObject.SetActive(true);
        newRecycleObject.transform.parent = playerHead.transform;

        recycledObjects.Add(newRecycleObject);
        _offset += 1f; //add to offset so that the next object will be above it
    }
}