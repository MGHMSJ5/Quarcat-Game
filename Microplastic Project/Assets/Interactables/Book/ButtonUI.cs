using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField]
    private bool _startActivated;
    private Animator _animator;
    private Button _button;

    private bool _started = false;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Pressed);
    }
    private void Start()
    {
        if (_startActivated)
        {
            _button.onClick.Invoke();
        }
        _started = true;
    }
    private void OnEnable()
    {
        if (_startActivated && _started)
        {
            _button.onClick.Invoke();
        }
    }

    public void OtherPressed()
    {
        _animator.SetTrigger("Normal");
    }

    private void Pressed()
    {
        _animator.SetTrigger("Pressed");
    }
}
