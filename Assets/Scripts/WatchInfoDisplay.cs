using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Pour utiliser HoverEnterEventArgs et HoverExitEventArgs

public class WatchInfoDisplay : MonoBehaviour
{
    // Référence au panneau d'information spécifique à cette montre.
    // Il s'agit du petit panneau qui apparaît AU-DESSUS de la montre elle-même.
    [Tooltip("Glissez ici le GameObject du Canvas d'information spécifique à cette montre (le petit panneau qui apparaît au survol).")]
    [SerializeField] private GameObject infoPanel;

    void Start()
    {
        // S'assurer que le panneau d'information est désactivé au démarrage
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Fonction appelée par le composant 'XR Simple Interactable' quand le rayon du contrôleur entre sur la montre.
    public void OnHoverEnterWatch(HoverEnterEventArgs args)
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true); // Active le panneau d'information spécifique à cette montre
        }
        // Informe le WatchManager quelle montre est actuellement survolée/sélectionnée
        if (WatchManager.Instance != null)
        {
            WatchManager.Instance.SetSelectedWatch(this.gameObject);
        }
    }

    // Fonction appelée par le composant 'XR Simple Interactable' quand le rayon du contrôleur quitte la montre.
    public void OnHoverExitWatch(HoverExitEventArgs args)
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false); // Désactive le panneau d'information spécifique à cette montre
        }
        // Note : On ne désélectionne PAS la montre via ClearSelectedWatch ici.
        // C'est le WatchManager qui gérera le changement de la 'currentSelectedWatch'
        // quand une nouvelle montre sera survolée.
    }

    // Vous pouvez également ajouter OnSelectEnterWatch(SelectEnterEventArgs args) et OnSelectExitWatch(SelectExitEventArgs args)
    // si vous préférez que les informations s'affichent uniquement au clic.
}