using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThunderUIController : MonoBehaviour
{
    //[Header("UI References")]
    //public Image thunderFill;      // ThunderBarFill
    //public GameObject barRoot;     // ThunderBarBG

    //private Coroutine timerCoroutine;

    //void Start()
    //{
    //    // ?n khi ch?a buff
    //    if (barRoot != null) barRoot.SetActive(false);
    //    if (thunderFill != null) thunderFill.fillAmount = 0f;
    //}

    //public void ShowThunderBar(float duration)
    //{
    //    if (timerCoroutine != null)
    //        StopCoroutine(timerCoroutine);

    //    barRoot.SetActive(true);
    //    timerCoroutine = StartCoroutine(UpdateThunderBar(duration));
    //}

    //private IEnumerator UpdateThunderBar(float duration)
    //{
    //    float elapsed = 0f;
    //    thunderFill.fillAmount = 1f;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        thunderFill.fillAmount = Mathf.Clamp01(1f - (elapsed / duration));
    //        yield return null;
    //    }

    //    thunderFill.fillAmount = 0f;
    //    barRoot.SetActive(false);
    //}

    //public void HideThunderBar()
    //{
    //    if (timerCoroutine != null)
    //    {
    //        StopCoroutine(timerCoroutine);
    //        timerCoroutine = null;
    //    }
    //    barRoot.SetActive(false);
    //    thunderFill.fillAmount = 0f;
    //}
    [Header("UI References")]
    public Image thunderFill;
    public GameObject barRoot;

    // THÊM M?I: ??nh ngh?a 2 màu ?? chuy?n ??i
    [Header("Bar Colors")]
    public Color fullColor = Color.green; // Màu khi thanh ??y
    public Color lowColor = Color.red;    // Màu khi thanh s?p h?t

    private Coroutine timerCoroutine;

    void Start()
    {
        if (barRoot != null) barRoot.SetActive(false);
        if (thunderFill != null)
        {
            thunderFill.fillAmount = 0f;
            thunderFill.color = fullColor; // ??t màu m?c ??nh là màu ??y
        }
    }

    public void ShowThunderBar(float duration)
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        barRoot.SetActive(true);
        timerCoroutine = StartCoroutine(UpdateThunderBar(duration));
    }

    public void HideThunderBar()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        if (barRoot.activeSelf)
        {
            barRoot.SetActive(false);
            thunderFill.fillAmount = 0f;
            thunderFill.color = fullColor; // Reset l?i màu khi ?n
        }
    }

    private IEnumerator UpdateThunderBar(float duration)
    {
        float elapsed = 0f;
        thunderFill.fillAmount = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentFill = Mathf.Clamp01(1f - (elapsed / duration));

            thunderFill.fillAmount = currentFill;

            // C?P NH?T: Thay ??i màu s?c d?a trên % còn l?i
            // Lerp(màu_A, màu_B, t): Chuy?n t? màu A sang B.
            // Khi t = 0 -> màu A. Khi t = 1 -> màu B.
            // Ta dùng (lowColor, fullColor) và currentFill (?i t? 1 v? 0)
            // ?? màu chuy?n t? fullColor v? lowColor.
            thunderFill.color = Color.Lerp(lowColor, fullColor, currentFill);

            yield return null;
        }

        // ??m b?o cu?i cùng thanh n?ng l??ng v? ?úng tr?ng thái
        thunderFill.fillAmount = 0f;
        thunderFill.color = fullColor;
        barRoot.SetActive(false);
        timerCoroutine = null;
    }
}
