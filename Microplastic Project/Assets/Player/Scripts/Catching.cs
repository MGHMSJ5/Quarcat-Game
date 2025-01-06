using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Catching : MonoBehaviour
{
    [Serializable]
    public class Beadies
    {
        public GameObject catchObject;
        public NavMeshAgent agent;
        public float originalSpeed;
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

    [SerializeField]
    private float _catchSpeed = 0.4f;
    [SerializeField]
    private float _slowedSpeed = 2f; //subtracts from speed
    private GameObject _bar;
    private bool _canCatch = false; 
    private bool _catching = false;

    void Awake()
    {
        _button = _catchButton.GetComponent<Button>();
        _image = _catchButton.GetComponent<Image>();
    }

    private void Start()
    {
        CatchCheck();
    }

    private void Update()
    {
        if (_catching && _bar != null)
        {
            Transform transform = _bar.GetComponent<Transform>();

            float decrease = _catchSpeed * Time.deltaTime;
            //make sure that it doesn't go below 0
            float newScaleX = Mathf.Max(transform.localScale.x - decrease, 0);

            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

            if (transform.localScale.x <= 0)
            {
                for (int i = 0; _beadies.Count > 0; i++)
                {
                    if (_beadies[i].catchObject = _bar.transform.parent.gameObject)
                    {
                        Destroy(_beadies[i].catchObject.transform.parent.parent.gameObject);
                        _beadies.RemoveAt(i);
                    }
                    else
                    {
                        Destroy(_bar.transform.parent.parent);
                    }
                }
                
                CatchCheck();
            }
        }
    }

    public void CatchMicroplastic()
    {
        if (!_canCatch)
        {
            return;
        }
        _bar = _beadies[0].catchObject.transform.GetChild(0).gameObject;
        NavMeshAgent agent = _beadies[0].agent;
        agent.speed -= _slowedSpeed;
        _catching = true;
        CatchCheck();
    }

    public void ReleaseMicroplastic()
    {
        if (_canCatch)
        {
            _beadies[0].agent.speed = _beadies[0].originalSpeed;
        }
        _catching = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            //add to list so that it can handle multiple microplastics
            Beadies toAdd = new Beadies()
            {
                catchObject = other.gameObject,
                agent = other.gameObject.transform.parent.GetComponent<NavMeshAgent>(),
            };
            toAdd.originalSpeed = toAdd.agent.speed;
            _beadies.Add(toAdd);
            CatchCheck();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            for (int i = 0; _beadies.Count > 0; i++)
            {
                if (_beadies[i].catchObject == other.gameObject)
                {
                    BeadieEscape(i);
                    break;
                }
            }
        }
    }
    public void CatchCheck()
    {
        if (_beadies.Count <= 0)
        {
            CanCatch(false);
        }
        else
        {
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

    public void CatchingInterrupt(GameObject beadieInterrupt)
    {
        if (_catching)
        {
            if (_beadies[0].catchObject = beadieInterrupt)
            {
                return;
            }
            CanCatch(false);
        }
        StartCoroutine(CheckCatching());
    }

    private IEnumerator CheckCatching()
    {//check for catching a little bit after being hit. In case beadie is still in trigger after being hit
        yield return new WaitForSeconds(1f);
        CatchCheck();
    }

    private void BeadieEscape(int beadieIndex)
    {
        _beadies[beadieIndex].agent.speed = _beadies[beadieIndex].originalSpeed;
        _beadies.RemoveAt(beadieIndex);

        CatchCheck();
    }
}
