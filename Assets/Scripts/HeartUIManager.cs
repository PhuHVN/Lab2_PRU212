using System.Collections.Generic;
using UnityEngine;

public class HeartUIManager : MonoBehaviour
{
    public List<GameObject> heartIcons;

    public void UpdateHearts(int currentHealth)
    {
        // Vòng l?p qua t?t c? các icon trái tim
        for (int i = 0; i < heartIcons.Count; i++)
        {
            // N?u index (i) nh? h?n s? máu hi?n t?i, b?t icon ?ó lên
            if (i < currentHealth)
            {
                heartIcons[i].SetActive(true);
            }
            // Ng??c l?i, t?t nó ?i
            else
            {
                heartIcons[i].SetActive(false);
            }
        }
    }
}
