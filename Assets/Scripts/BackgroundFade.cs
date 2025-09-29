using System.Collections;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    public SpriteRenderer[] backgrounds;  // Danh sách background (Day, Sunset, Night...)
    public float holdTime = 5f;           // Thời gian giữ 1 background
    public float fadeDuration = 2f;       // Thời gian fade dần
    public int startIndex = 0;            // Background bắt đầu

    private int currentIndex;

    private void Start()
    {
        // Ẩn tất cả trừ background startIndex
        for (int i = 0; i < backgrounds.Length; i++)
        {
            SetAlpha(backgrounds[i], (i == startIndex) ? 1f : 0f);
        }

        currentIndex = startIndex;
        StartCoroutine(CycleBackgrounds());
    }

    private IEnumerator CycleBackgrounds()
    {
        while (true)
        {
            // Giữ background hiện tại
            yield return new WaitForSeconds(holdTime);

            // Tìm background tiếp theo
            int nextIndex = (currentIndex + 1) % backgrounds.Length;

            // Fade từ current → next
            yield return StartCoroutine(Fade(backgrounds[currentIndex], backgrounds[nextIndex]));

            // Cập nhật index hiện tại
            currentIndex = nextIndex;
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