using System.Collections;
using TMPro;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _blocksPrefab;
    [SerializeField]
    private GameObject _singleBlock;
    [SerializeField]
    private TextMeshProUGUI _cubeCounter;

    [Header("Values")]
    [SerializeField]
    private float _betweenSpawnTime = 1f;

    private int _counter = 0;
    
    private bool testBool = false;

    private Coroutine _spawnCoroutine;

    private void Update()
    {
        _cubeCounter.text = _counter.ToString();
        if (testBool)
        {
            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = StartCoroutine(ConstantlySpawnBlock());
            }
        }
    }
    public void SpawnBlocks()
    {
        Instantiate(_blocksPrefab);
        _counter += 13;
    }

    public void AutoSpawnBlock()
    {
        testBool = !testBool;
        _spawnCoroutine = null;
    }

    IEnumerator ConstantlySpawnBlock()
    {
        Instantiate( _singleBlock);
        _counter += 1;
        yield return new WaitForSeconds(_betweenSpawnTime);

        _spawnCoroutine = null;
    }
}
