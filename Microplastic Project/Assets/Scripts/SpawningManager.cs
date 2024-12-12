using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    private GameObject _spawningGameObject;

    [SerializeField]
    private int _spawnNumber = 4;

    [SerializeField]
    //keep track of the spawned microplastics
    private List<GameObject> _spawnedMicroplastics = new List<GameObject>();

    private float _spawnBreakMax = 2f;
    private float _spawnBreakMin = 1f;

    private bool _canSpawn = true; //added bool so that spawning only happens once
    //private static bool _hasReplaced = false; //use bool so that replacing button won't appear again when replaced
    //private static bool _isSpawning = false; //bool that will be true when microplastics have been spawned
    ////Will be used to spawn enemies again if they are not caught and player goes through different scenes

    //public bool HasReplaced
    //{
    //    get { return _hasReplaced; }
    //    set { _hasReplaced = value; }
    //}

    //public bool IsSpawning
    //{
    //    get { return _isSpawning; }
    //}

    [Header("Replacing")]
    [SerializeField]
    private GameObject _originalGameObjectParent;
    [SerializeField]
    private GameObject _replacngGameObjectParent;
    [SerializeField]
    private GameObject _replaceButton;
    private Replacing _replacing;
    private TextMeshProUGUI _replacingText;
    [SerializeField]
    private string _textForReplacing;

    [Header("Respawn")]
    [SerializeField]
    private float _respawnSeconds = 10f;
    private bool _isCoroutineRunning = false;

    public int identity;

    private void Start()
    {
        SpawnBoolManager.AddToList(identity, false, false);

        _replacing = _replaceButton.GetComponent<Replacing>();
        _replacingText = _replaceButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (SpawnBoolManager.GetHasReplaced(identity))
        {
            _originalGameObjectParent.SetActive(false);
            _replacngGameObjectParent.SetActive(true);
        }
    }

    private void Update()
    {
        if (SpawnBoolManager.GetHasReplaced(identity))
        {
            SpawnBoolManager.SetIsSpawning(identity, false);
        }
        
        if (SpawnBoolManager.GetIsSpawning(identity) && _canSpawn)
        {
            StartCoroutine(SpawnMicroplastics());
            _canSpawn = false;
        }
        
        if (CheckIfAllCatched() && _spawnedMicroplastics.Count == _spawnNumber && !_isCoroutineRunning)
        {
            StartCoroutine(RespawnCounter(_respawnSeconds));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   //make sure that the microplastics only spawn once
            if (_canSpawn && !SpawnBoolManager.GetHasReplaced(identity))
            {
                StartCoroutine(SpawnMicroplastics());
                _canSpawn = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !SpawnBoolManager.GetHasReplaced(identity))
        {
            //check for if all the microplastics are caught
            if (_spawnedMicroplastics.Count == _spawnNumber)
            {
                if (CheckIfAllCatched())
                {   //if all microplastics are caught, then have the replacing ready
                    ReadyReplacing(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   //if the button was visible, then deactivate it
            if (_replaceButton.activeInHierarchy)
            {
                _replaceButton.SetActive(false);
            }
        }
    }
    //spawn microplastics with X time between the spawning
    IEnumerator SpawnMicroplastics()
    {//wait for end of frame to let microplastic spawn (fixes 'Missing Object' problem)
        yield return new WaitForEndOfFrame();
        SpawnBoolManager.SetIsSpawning(identity, true);
        for (int i = 0; i < _spawnNumber; i++)
        {
            GameObject microplastic = Instantiate(_spawningGameObject, transform.position, Quaternion.identity);
            _spawnedMicroplastics.Add(microplastic);
            yield return new WaitForSeconds(GetRandomTime(_spawnBreakMax, _spawnBreakMin));
        }
    }

    private void ReadyReplacing(bool ready)
    {
        if (ready)
        {
            _replacing.originalGameObject = _originalGameObjectParent;
            _replacing.replaceGameObject = _replacngGameObjectParent;
            _replacingText.text = _textForReplacing;
            _replacing.spawnId = identity;
        }
        else
        {
            _replacing.ResetVariables();
        }
        
        _replaceButton.SetActive(ready);
    }

    private IEnumerator RespawnCounter(float time)
    {
        _isCoroutineRunning = true;
        yield return new WaitForSeconds(time);
        _spawnedMicroplastics.Clear(); //reset the list
        if (!SpawnBoolManager.GetHasReplaced(identity))
        {//if player hasn't replaced the item, then make the microplastics respawn
            ReadyReplacing(false);
            StartCoroutine(SpawnMicroplastics());
        }
        _isCoroutineRunning = false;
    }
    public bool CheckIfAllCatched()
    {
        for (int i = 0; i < _spawnedMicroplastics.Count; i++)
        {
            if (_spawnedMicroplastics[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    private float GetRandomTime(float maxTime, float minTime)
    {
        return Random.Range(maxTime, minTime);
    }
}
