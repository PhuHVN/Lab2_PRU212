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

    // TH�M M?I: ??nh ngh?a 2 m�u ?? chuy?n ??i
    [Header("Bar Colors")]
    public Color fullColor = Color.green; // M�u khi thanh ??y
    public Color lowColor = Color.red;    // M�u khi thanh s?p h?t

    private Coroutine timerCoroutine;

    void Start()
    {
        if (barRoot != null) barRoot.SetActive(false);
        if (thunderFill != null)
        {
            thunderFill.fillAmount = 0f;
            thunderFill.color = fullColor; // ??t m�u m?c ??nh l� m�u ??y
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
            thunderFill.color = fullColor; // Reset l?i m�u khi ?n
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

            // C?P NH?T: Thay ??i m�u s?c d?a tr�n % c�n l?i
            // Lerp(m�u_A, m�u_B, t): Chuy?n t? m�u A sang B.
            // Khi t = 0 -> m�u A. Khi t = 1 -> m�u B.
            // Ta d�ng (lowColor, fullColor) v� currentFill (?i t? 1 v? 0)
            // ?? m�u chuy?n t? fullColor v? lowColor.
            thunderFill.color = Color.Lerp(lowColor, fullColor, currentFill);

            yield return null;
        }

        // ??m b?o cu?i c�ng thanh n?ng l??ng v? ?�ng tr?ng th�i
        thunderFill.fillAmount = 0f;
        thunderFill.color = fullColor;
        barRoot.SetActive(false);
        timerCoroutine = null;
    }
}
