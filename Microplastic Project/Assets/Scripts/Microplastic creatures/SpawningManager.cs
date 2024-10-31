using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawningGameObject;
    [SerializeField]
    private int _spawnNumber = 4;

    private float _spawnBreakMax = 2f;
    private float _spawnBreakMin = 1f;

    private bool _canSpawn = true; //added bool so that spawning only happens once
    

    private void OnTriggerEnter(Collider other)
    {
        if (_canSpawn)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(SpawnMicroplastics());
                _canSpawn = false;
            }
        }
    }

    IEnumerator SpawnMicroplastics()
    {
        for (int i = 0; i < _spawnNumber; i++)
        {
            Instantiate(_spawningGameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(GetRandomTime(_spawnBreakMax, _spawnBreakMin));
        }
    }

    private float GetRandomTime(float maxTime, float minTime)
    {
        return Random.Range(maxTime, minTime);
    }
}
