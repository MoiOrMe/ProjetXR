using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Pour utiliser HoverEnterEventArgs et HoverExitEventArgs

public class WatchInfoDisplay : MonoBehaviour
{
    // R�f�rence au panneau d'information sp�cifique � cette montre.
    // Il s'agit du petit panneau qui appara�t AU-DESSUS de la montre elle-m�me.
    [Tooltip("Glissez ici le GameObject du Canvas d'information sp�cifique � cette montre (le petit panneau qui appara�t au survol).")]
    [SerializeField] private GameObject infoPanel;

    void Start()
    {
        // S'assurer que le panneau d'information est d�sactiv� au d�marrage
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Fonction appel�e par le composant 'XR Simple Interactable' quand le rayon du contr�leur entre sur la montre.
    public void OnHoverEnterWatch(HoverEnterEventArgs args)
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true); // Active le panneau d'information sp�cifique � cette montre
        }
        // Informe le WatchManager quelle montre est actuellement survol�e/s�lectionn�e
        if (WatchManager.Instance != null)
        {
            WatchManager.Instance.SetSelectedWatch(this.gameObject);
        }
    }

    // Fonction appel�e par le composant 'XR Simple Interactable' quand le rayon du contr�leur quitte la montre.
    public void OnHoverExitWatch(HoverExitEventArgs args)
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false); // D�sactive le panneau d'information sp�cifique � cette montre
        }
        // Note : On ne d�s�lectionne PAS la montre via ClearSelectedWatch ici.
        // C'est le WatchManager qui g�rera le changement de la 'currentSelectedWatch'
        // quand une nouvelle montre sera survol�e.
    }

    // Vous pouvez �galement ajouter OnSelectEnterWatch(SelectEnterEventArgs args) et OnSelectExitWatch(SelectExitEventArgs args)
    // si vous pr�f�rez que les informations s'affichent uniquement au clic.
}