using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catching : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _catchableObjects = new List<GameObject>();
    [SerializeField]
    private GameObject _catchButton;

    private Button _button;
    private Image _image;
    [SerializeField]
    private Color _canUseColor;
    [SerializeField]
    private Color _cantUseColor;

    void Awake()
    {
        _button = _catchButton.GetComponent<Button>();
        _image = _catchButton.GetComponent<Image>();
    }

    private void Start()
    {
        CanCatch(false);
    }

    public void CatchMicroplastic()
    {
        //for nw destory the parent of the parent (because of the way the movement works for now)
        Destroy(_catchableObjects[0].transform.parent.parent.gameObject);
        _catchableObjects.RemoveAt(0);

        CatchCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            //add to list so that it can handle multiple microplastics
            _catchableObjects.Add(other.gameObject);
            CanCatch(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MP1")
        {
            _catchableObjects.Remove(other.gameObject);
        }

        CatchCheck();
    }
    private void CatchCheck()
    {
        if (_catchableObjects.Count <= 0)
        {
            CanCatch(false);
        }
    }

    private void CanCatch(bool can)
    {
        _button.enabled = can;
        _image.color = can ? _canUseColor : _cantUseColor;
    }
}
