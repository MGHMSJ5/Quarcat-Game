using System.Collections;
using UnityEngine;

public class CollisionBeadie : MonoBehaviour
{
    [SerializeField]
    private Catching _catching;
    [SerializeField]
    private float _interruptDelay = 4f;

    private bool _canInterrupt = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MP1" && _canInterrupt)
        {
            _catching.CatchingInterrupt(other.gameObject);
            _canInterrupt = false;
            StartCoroutine(InterruptDelay());
        }
    }

    private IEnumerator InterruptDelay()
    {//check for catching a little bit after being hit
        yield return new WaitForSeconds(_interruptDelay);
        _canInterrupt = true;
    }
}
