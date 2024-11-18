using System.Collections;
using UnityEngine;

/// <summary>
/// Fades a canvas over time using a coroutine and a canvas group
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FadeCanvas : MonoBehaviour
{
    [Tooltip("The speed at which the canvas fades")]
    public float defaultDuration = 1.0f;
    public bool fadeOnStart = false;
    [SerializeField]
    private GameObject _background;

    public Coroutine CurrentRoutine { private set; get; } = null;

    private CanvasGroup canvasGroup = null;

    private float quickFadeDuration = 0.25f;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (fadeOnStart)
        {
            StartFadeOut();
        }
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeIn(defaultDuration));
    }

    public void StartFadeOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeOut(defaultDuration));
    }

    public void QuickFadeIn()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeIn(quickFadeDuration));
    }

    public void QuickFadeOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeOut(quickFadeDuration));
    }

    private IEnumerator FadeIn(float duration)
    {
        BackgroundActivate(true);
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            SetAlpha(elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1.0f); //ensure final value
    }

    private IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(0.5f); //added tiny delay because fading was being weird
        BackgroundActivate(true);
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            SetAlpha(1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0.0f); //ensure final value
        BackgroundActivate(false);
    }

    private void SetAlpha(float value)
    {
        canvasGroup.alpha = Mathf.Clamp01(value);
    }

    private void BackgroundActivate(bool activate)
    {
        _background.SetActive(activate);
    }
}