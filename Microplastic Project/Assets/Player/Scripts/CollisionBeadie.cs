using System.Collections;
using UnityEngine;

public class CollisionBeadie : MonoBehaviour
{
    [SerializeField]
    private Catching _catchingScript;
    [SerializeField]
    private float _interruptDelay = 2f;

    private bool _canInterrupt = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interrupt" && _canInterrupt)
        {
            if (_catchingScript.IsCatching)
            {
                _catchingScript.CatchingInterrupt();
                _canInterrupt = false;
                StartCoroutine(InterruptDelay());
            }
        }
    }

    private IEnumerator InterruptDelay()
    {//check for catching a little bit after being hit
        yield return new WaitForSeconds(_interruptDelay);
        _canInterrupt = true;
    }
}
