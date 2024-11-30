using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class BasicInteractLayout : MonoBehaviour
{
    [Tooltip("This script will automatically be assigned through unique Interact... script")]
    public UnityAction _interactAction;//will be assigned from unique Interact... script
    private Interact _interactScript;
    private void Awake()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void EnterInteract()
    {
        _interactScript.CanInteract(true);
        _interactScript.interactEvent.AddListener(_interactAction);
    }
    public void ExitANDNotInteract(bool once)
    {
        _interactScript.CanInteract(false);
        _interactScript.interactEvent.RemoveListener(_interactAction);
        _interactAction = once ? null : _interactAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _interactScript = other.gameObject.GetComponent<Interact>();
            if (_interactAction != null)
            {
                EnterInteract();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_interactAction != null)
            {
                ExitANDNotInteract(false);
            }  
        }
    }
}
