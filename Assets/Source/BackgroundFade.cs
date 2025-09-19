using System.Collections;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    public SpriteRenderer[] background;    // nền ngày

    public float holdTime = 5f;             // thời gian hiển thị trước khi chuyển
    public float fadeDuration = 2f;         // thời gian fade dần

    private void Start()
    {
        // Ban đầu: hiển thị Day
        SetAlpha(background[0], 1f);
        SetAlpha(background[1], 0f);

        // bắt đầu vòng lặp ngày đêm
        StartCoroutine(DayNightCycle());
    }

    private IEnumerator DayNightCycle()
    {
        while (true)
        {
            // 1. Giữ Day
            yield return new WaitForSeconds(holdTime);

            // 2. Fade Day -> Night
            yield return StartCoroutine(Fade(background[0], background[1]));

            // 3. Giữ Night
            yield return new WaitForSeconds(holdTime);

            // 4. Fade Night -> Day
            yield return StartCoroutine(Fade(background[1], background[0]));
        }
    }

    private IEnumerator Fade(SpriteRenderer from, SpriteRenderer to)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            SetAlpha(from, 1f - t);
            SetAlpha(to, t);

            yield return null;
        }

        SetAlpha(from, 0f);
        SetAlpha(to, 1f);
    }

    private void SetAlpha(SpriteRenderer sr, float alpha)
    {
        if (sr != null)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}
