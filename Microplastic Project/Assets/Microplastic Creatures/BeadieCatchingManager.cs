using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BeadieCatchingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private Image _healthImage;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _canCatchIndicator;

    [Header("Changeable Variables")]
    public float catchSpeed = 0.1f;
    [SerializeField]
    private float _normalSpeed = 5f;
    [SerializeField]
    private float _slowedSpeed = 3f;

    private Transform healthbar;

    private bool _isCatching = false;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = transform.GetChild(0);
        _canvas.SetActive(false);
        _canCatchIndicator.SetActive(false);
        _agent.speed = _normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCatching)
        {
            float decrease = catchSpeed * Time.deltaTime;
            //make sure that it doesn't go below 0
            float newScaleX = Mathf.Max(healthbar.localScale.x - decrease, 0);

            healthbar.localScale = new Vector3(newScaleX, healthbar.localScale.y, healthbar.localScale.z);
        }

        Vector3 newSize = new Vector3(healthbar.localScale.x, _healthImage.rectTransform.localScale.y, _healthImage.rectTransform.localScale.z);

        _healthImage.rectTransform.localScale = newSize;

        if (healthbar.localScale.x <= 0)
        {
            Destroy(transform.parent.parent);
        }
    }

    public void InRangeToCatch()
    {
        _canvas.SetActive(true);
        _canCatchIndicator.SetActive(true);
    }

    public void OutOfRangeToCatch()
    {
        _canvas.SetActive(false);
        _canCatchIndicator.SetActive(false);
        StoppedCatching();
    }

    public void Catching()
    {
        _agent.speed = _slowedSpeed;
        _isCatching = true;
    }

    public void StoppedCatching()
    {
        _agent.speed = _normalSpeed;
        _isCatching = false;
    }
}