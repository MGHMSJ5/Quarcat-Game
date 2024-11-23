using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public UnityEvent interactEvent;
    [SerializeField]
    private GameObject _interactButton;

    private Button _button;
    private Image _image;

    [SerializeField]
    private Color _canUseColor;
    [SerializeField]
    private Color _cantUseColor;
    // Start is called before the first frame update
    void Awake()
    {
        _button = _interactButton.GetComponent<Button>();
        _image = _interactButton.GetComponent<Image>();
    }

    private void Start()
    {
        CanInteract(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interact")
        {
            CanInteract(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interact")
        {
            CanInteract(false);
        }
    }
    public void CanInteract(bool can)
    {
        _button.enabled = can;
        _image.color = can ? _canUseColor : _cantUseColor;
    }

    public void StartInteracting()
    {
        interactEvent?.Invoke();
    }
}
