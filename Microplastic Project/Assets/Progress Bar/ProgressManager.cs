using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Slider _progressSlider;
    [SerializeField]
    private Animator _animator;
    [Header("Number of Microplastic Sources in Floor")]
    [Tooltip("You need to manually set the value of this variable on " +
        "how many Microplastic Sources there are in total in this room")]
    [SerializeField]
    private int _totalSourcesOnFloor;

    private static float currentProces = 0;

    private float _addValueAnimationTime = 1.2f;

    void Start()
    {
        _progressSlider.maxValue = _totalSourcesOnFloor;
        _progressSlider.value = currentProces;
    }

    public void AddToProgress(int items)
    {
        currentProces += items;
        _animator.SetTrigger("Enter");
        StartCoroutine(BarAnimate(_addValueAnimationTime, items));
    }

    private IEnumerator BarAnimate(float duration, int items)
    {
        yield return new WaitForSeconds(2f);
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            SetValue(elapsedTime / duration, items);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _progressSlider.value = currentProces;
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Leave");
    }

    private void SetValue(float value, int items)
    {
        _progressSlider.value = Mathf.Lerp(currentProces - items, currentProces, value);
    }
}
