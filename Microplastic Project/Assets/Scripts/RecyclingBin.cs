﻿using System.Collections;
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
    [Tooltip("Only the script in the hallway needs a reference to this")]
    [SerializeField]
    private ProgressManager _progressManager;
    [Tooltip("Only need the reference in the Hallway")]
    [SerializeField]
    private UpgradeManager _upgradeManager;

    [Header("Audio Feedback")]
    [Tooltip("Assign the AudioSource named 'aud_Recycle' here")]
    public AudioSource audioSource; // Drag aud_Recycle here in the Inspector

    [Tooltip("Optional: Assign the specific sound clip to play")]
    public AudioClip replaceSound;

    private float _offset = 0f;

    public static int recyclePoints = 0;
    [Tooltip("To get the recyclepoints, use this variable for reference↓")]
    public int RecyclePoitns
    {
        get { return recyclePoints; }
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {//wait for the next frame. This all will run after Start().
        yield return null;
        GameObject movedObject = GameObject.FindGameObjectWithTag("SceneMoved");
        if (movedObject != null)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in movedObject.transform)
            {
                children.Add(child);
            }
            foreach (Transform child in children)
            {
                print(child);
                child.parent = null;
                child.gameObject.name = "OriginalObjectParent";
                GrabRecycleObject(child.gameObject);
                Destroy(child.gameObject);
            }
            Destroy(movedObject);
        }
    }

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
        int recycledObjectsCount = recycledObjects.Count;
        if (recycledObjectsCount == 0)
        {
            return;
        }

        if (audioSource != null && replaceSound != null)
        {
            audioSource.PlayOneShot(replaceSound);
        }

        _progressManager.AddToProgress(recycledObjectsCount);

        for (int i = 0; i < recycledObjectsCount; i++)
        {
            Destroy(recycledObjects[i]);
            _offset = 0f;
            recycleButton.SetActive(false);
        }
        recycledObjects.Clear();
        recyclePoints += recycledObjectsCount;
        _upgradeManager.AddRemoveUpgradePoint(recycledObjectsCount);
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