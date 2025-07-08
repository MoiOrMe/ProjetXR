using UnityEngine;
using System.Collections.Generic;

public class WatchManager : MonoBehaviour
{
    public static WatchManager Instance { get; private set; }

    [Tooltip("Glissez ici tous les GameObjects de vos montres.")]
    [SerializeField] private List<GameObject> allWatches;

    // La montre actuellement sélectionnée
    private GameObject currentSelectedWatch;

    // État de gel de la montre sélectionnée
    private bool isWatchFrozen = false;

    // Position à laquelle la montre a été gelée
    private Vector3 frozenPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- Gestion de la Montre Sélectionnée ---

    // Définit la montre sur laquelle les actions seront appliquées
    public void SetSelectedWatch(GameObject watch)
    {
        if (currentSelectedWatch != watch)
        {
            // Quand on sélectionne une nouvelle montre, l'ancienne n'est plus "gelée"
            if (currentSelectedWatch != null && isWatchFrozen)
            {
                // Note : Pour l'instant, on se contente de réinitialiser l'état isWatchFrozen.
                // L'effet concret de "dégel" sera géré par la logique qui utilise isWatchFrozen.
                isWatchFrozen = false;
            }
            currentSelectedWatch = watch;
            isWatchFrozen = false; // La nouvelle montre n'est pas gelée par défaut

            // Enregistre sa position actuelle comme point de gel potentiel
            if (currentSelectedWatch != null)
            {
                frozenPosition = currentSelectedWatch.transform.position;
            }
        }
    }

    // Désélectionne la montre actuelle
    public void ClearSelectedWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = false; // La désélection dégèle implicitement
            currentSelectedWatch = null;
        }
    }


    // --- Fonctions des Boutons "Geler" et "Dégeler" la POSITION ---

    // Gèle la position de la montre sélectionnée.
    public void FreezeWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = true;
            // Enregistre la position actuelle de la montre pour la figer.
            frozenPosition = currentSelectedWatch.transform.position;
            // C'est cette variable 'isWatchFrozen' qui sera lue par le système de mouvement
            // pour empêcher la montre de bouger.
            // (Le code qui utilise cette variable pour bloquer le mouvement sera ajouté plus tard,
            // pour l'instant, on gère juste l'état).
        }
    }

    // Dégèle la position de la montre sélectionnée.
    public void UnfreezeWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = false;
            // La montre peut de nouveau être déplacée.
        }
    }

    // --- Propriété pour l'état de gel (à lire par les systèmes de mouvement externes) ---
    public bool IsWatchFrozen
    {
        get { return isWatchFrozen; }
    }
}