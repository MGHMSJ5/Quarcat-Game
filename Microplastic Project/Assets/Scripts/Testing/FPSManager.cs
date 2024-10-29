using TMPro;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _uiText;

    private int _lastFrameIndex;
    private float[] _frameDeltaTimeArray;

    private void Awake()
    {
        _frameDeltaTimeArray = new float[50];
    }

    private void Start()
    {
        //set the target frames
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;

        _uiText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in _frameDeltaTimeArray)
        {
            total += deltaTime;
        }

        return _frameDeltaTimeArray.Length / total;
    }
}
