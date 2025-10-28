using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LogoClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Animation")]
    [SerializeField] private float clickScale = 0.9f;
    [SerializeField] private float animationDuration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ClickAnimation());
    }

    private IEnumerator ClickAnimation()
    {
        Vector3 targetClick = originalScale * clickScale;

        // Scale down
        yield return ScaleOverTime(transform.localScale, targetClick);
        // Scale back
        yield return ScaleOverTime(targetClick, originalScale);
    }

    private IEnumerator ScaleOverTime(Vector3 from, Vector3 to)
    {
        float timer = 0f;
        while (timer < animationDuration)
        {
            timer += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(from, to, timer / animationDuration);
            yield return null;
        }
        transform.localScale = to;
    }
}
