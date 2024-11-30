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

    void Awake()
    {
        _button = _interactButton.GetComponent<Button>();
        _image = _interactButton.GetComponent<Image>();
    }

    private void Start()
    {
        CanInteract(false);
    }

    public void CanInteract(bool can)//CanInteract will be called from BasicInteractLayout
    {
        _button.enabled = can;
        _image.color = can ? _canUseColor : _cantUseColor;
    }

    public void StartInteracting()//Event will be added on through BasicInteractLayout & the unique interact script
    {
        interactEvent?.Invoke();
    }
}
