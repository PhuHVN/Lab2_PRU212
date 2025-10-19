using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject pausePanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;

        }
    }
   
}
