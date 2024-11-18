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
    private static bool _hasReplaced = false; //use bool so that replacing button won't appear again when replaced

    public bool HasReplaced
    {
        get { return _hasReplaced; }
        set { _hasReplaced = value; }
    }

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

    private void Start()
    {
        _replacing = _replaceButton.GetComponent<Replacing>();
        _replacingText = _replaceButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (_hasReplaced)
        {
            _originalGameObjectParent.SetActive(false);
            _replacngGameObjectParent.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   //make sure that the microplastics only spawn once
            if (_canSpawn && !HasReplaced)
            {
                StartCoroutine(SpawnMicroplastics());
                _canSpawn = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !HasReplaced)
        {
            //check for if all the microplastics are caught
            if (_spawnedMicroplastics.Count == _spawnNumber)
            {
                if (CheckIfAllCatched())
                {   //if all microplastics are caught, then have the replacing ready
                    ReadyReplacing();
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
    {
        for (int i = 0; i < _spawnNumber; i++)
        {
            GameObject microplastic = Instantiate(_spawningGameObject, transform.position, Quaternion.identity);
            _spawnedMicroplastics.Add(microplastic);
            yield return new WaitForSeconds(GetRandomTime(_spawnBreakMax, _spawnBreakMin));
        }
    }

    private void ReadyReplacing()
    {
        _replacing.originalGameObject = _originalGameObjectParent;
        _replacing.replaceGameObject = _replacngGameObjectParent;
        _replacingText.text = _textForReplacing;
        _replacing.spawningManager = gameObject.GetComponent<SpawningManager>(); //the replacing script will change the hasReplaced bool
        _replaceButton.SetActive(true);
    }
    private bool CheckIfAllCatched()
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
