using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawningGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(_spawningGameObject, transform.position, Quaternion.identity);
        }
    }
}
