using UnityEngine;

public class WatchController : MonoBehaviour
{
    [Header("R�f�rences")]
    public GameObject storyPanel;
    public Animator watchAnimator; // � assigner manuellement si besoin
    public RuntimeAnimatorController watchAnimatorController;

    void Awake()
    {
        // Si l'Animator n�est pas assign� manuellement, on le cherche dans les enfants
        if (watchAnimator == null)
            watchAnimator = GetComponentInChildren<Animator>();

        if (watchAnimator != null && watchAnimatorController != null)
            watchAnimator.runtimeAnimatorController = watchAnimatorController;

        if (storyPanel != null)
            storyPanel.SetActive(false);
    }

    public void OnWatchSelected()
    {
        if (storyPanel != null)
            storyPanel.SetActive(true);

        if (watchAnimator != null)
            watchAnimator.SetTrigger("ExplodeTrigger");
    }

    public void OnWatchDeselected()
    {
        if (storyPanel != null)
            storyPanel.SetActive(false);

        if (watchAnimator != null)
            watchAnimator.SetTrigger("ResetTrigger");
    }
}
