using UnityEngine;
using System;

public class ButtonBounceEffect : MonoBehaviour
{
    public RectTransform target; // what componenet will be animated
    public float bounceDistance = 10f; // How far the component will bounce
    public float bounceDuration = 0.1f; // How long the bounce takes

    private Vector3 originalPosition;

    private void Awake()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned for ButtonBounceEffect.");
            return;
        }

        originalPosition = target.localPosition;
    }

    public void PlayBounce(Action onComplete = null)
    {
        if (target == null) return;

        StopAllCoroutines();
        StartCoroutine(BounceRoutine(onComplete));
    }

    private System.Collections.IEnumerator BounceRoutine(Action onComplete)
    {
        Vector3 downPosition = originalPosition - new Vector3(0, bounceDistance, 0);
        float elapsedTime = 0;

        // Move the component down
        while (elapsedTime < bounceDuration)
        {
            target.localPosition = Vector3.Lerp(originalPosition, downPosition, elapsedTime / bounceDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        target.localPosition = downPosition;

        // Reset the component to original position
        elapsedTime = 0;
        while (elapsedTime < bounceDuration)
        {
            target.localPosition = Vector3.Lerp(downPosition, originalPosition, elapsedTime / bounceDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        target.localPosition = originalPosition;

        // Trigger the callback after the bounce finishes
        onComplete?.Invoke();
    }
}
