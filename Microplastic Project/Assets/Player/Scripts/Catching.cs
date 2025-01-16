using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catching : MonoBehaviour
{
    [Serializable]
    public class Beadies
    {
        public GameObject catchObject;
        public BeadieCatchingManager beadieCatchingManager;
    }

    [SerializeField]
    private List<Beadies> _beadies = new List<Beadies>();

    [SerializeField]
    private GameObject _catchButton;

    private Button _button;
    private Image _image;
    [SerializeField]
    private Color _canUseColor;
    [SerializeField]
    private Color _cantUseColor;
    [Header("")]
    [SerializeField]
    private PlayerController _playerController;
    public float originalSpeed;
    private bool _canCatch = false;
    private bool _catching = false;

    [SerializeField]
    private Animator _animator; 

    public static float catchSpeed = 1f;
    public static float invulnerabilityTime = 2f;

    public float CatchSpeed
    {
        get { return catchSpeed; }
        set { catchSpeed = value; }
    }
    public float InvulnerabilityTime
    {
        get { return invulnerabilityTime; }
        set { invulnerabilityTime = value; }
    }

    public bool IsCatching
    {
        get { return _catching; }
    }

    void Awake()
    {
        _button = _catchButton.GetComponent<Button>();
        _image = _catchButton.GetComponent<Image>();
    }

    private void Start()
    {
        CatchCheck();
        originalSpeed = _playerController.speed;
    }

    private void Update()
    {
        if (_beadies.Count > 0)
        {
            // Remove any rows where Beadie has been deleted
            for (int i = 0; i < _beadies.Count; i++)
            {
                if (_beadies[i].catchObject == null)
                {
                    _beadies.RemoveAt(i);
                    CatchCheck();
                }
            }
        }

        // Change the speed of the player to be the slowed speed of the Beadie
        if (_catching)
        {
            float slowedSpeed = _beadies[0].beadieCatchingManager.SlowedSpeed;
            if (_playerController.speed != slowedSpeed)
            {
                _playerController.speed = slowedSpeed;
            }
        }
        else
        {
            if (_playerController.speed != originalSpeed)
            {
                _playerController.speed = originalSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            // Add to list so that it can handle multiple microplastics
            Beadies toAdd = new Beadies()
            {
                catchObject = other.gameObject,
                beadieCatchingManager = other.gameObject.GetComponent<BeadieCatchingManager>(),
            };
            _beadies.Add(toAdd);
            CatchCheck();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            for (int i = 0; i < _beadies.Count; i++)
            {
                if (_beadies[i].catchObject == other.gameObject)
                {
                    BeadieEscape(i);
                    break;
                }
            }
        }
    }

    public void CatchMicroplastic()
    {
        _catching = true;
        CatchCheck();
        if (!_canCatch)
        {
            return;
        }
        _beadies[0].beadieCatchingManager.catchSpeed = catchSpeed;
        _beadies[0].beadieCatchingManager.Catching();
        _animator.SetBool("isCatching", true); 
    }

    public void ReleaseMicroplastic()
    {
        _catching = false;
        if (_canCatch && _beadies.Count > 0)
        {
            _beadies[0].beadieCatchingManager.StoppedCatching();
        }
        _animator.SetBool("isCatching", false); 
    }

    private void CatchCheck()
    {
        if (_beadies.Count <= 0)
        {
            CanCatch(false);
        }
        else
        {
            // Run method of beadie first in the list
            _beadies[0].beadieCatchingManager.InRangeToCatch();
            CanCatch(true);
        }
    }

    private void CanCatch(bool can)
    {
        _canCatch = can;
        _button.enabled = can;
        _image.color = can ? _canUseColor : _cantUseColor;
        if (!can && _catching)
        {
            ReleaseMicroplastic();
        }
    }

    public void CatchingInterrupt()
    {
        CanCatch(false);
        _beadies[0].beadieCatchingManager.OutOfRangeToCatch();
        StartCoroutine(CheckCatching());
    }

    private IEnumerator CheckCatching()
    {
        // Check for catching a little bit after being hit. In case beadie is still in trigger after being hit
        yield return new WaitForSeconds(invulnerabilityTime);
        CatchCheck();
    }

    private void BeadieEscape(int beadieIndex)
    {
        _beadies[beadieIndex].beadieCatchingManager.OutOfRangeToCatch();
        _beadies.RemoveAt(beadieIndex);
        CatchCheck();
    }
}
