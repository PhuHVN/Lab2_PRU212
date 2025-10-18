using System.Collections.Generic;
using UnityEngine;

public class HeartUIManager : MonoBehaviour
{
    public List<GameObject> heartIcons;

    public void UpdateHearts(int currentHealth)
    {
        // V�ng l?p qua t?t c? c�c icon tr�i tim
        for (int i = 0; i < heartIcons.Count; i++)
        {
            // N?u index (i) nh? h?n s? m�u hi?n t?i, b?t icon ?� l�n
            if (i < currentHealth)
            {
                heartIcons[i].SetActive(true);
            }
            // Ng??c l?i, t?t n� ?i
            else
            {
                heartIcons[i].SetActive(false);
            }
        }
    }
}
