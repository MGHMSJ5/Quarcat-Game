using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class BasicInteractLayout : MonoBehaviour
{
    public UnityAction _interactAction;
    private Interact _interactScript;
    private void Awake()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _interactScript = other.gameObject.GetComponent<Interact>();
            _interactScript.CanInteract(true);
            _interactScript.interactEvent.AddListener(_interactAction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _interactScript.CanInteract(false);
            _interactScript.interactEvent.RemoveListener(_interactAction);
        }
    }
}
