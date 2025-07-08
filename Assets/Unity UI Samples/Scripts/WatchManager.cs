using UnityEngine;
using System.Collections.Generic;

public class WatchManager : MonoBehaviour
{
    public static WatchManager Instance { get; private set; }

    [Tooltip("Glissez ici tous les GameObjects de vos montres.")]
    [SerializeField] private List<GameObject> allWatches;

    // La montre actuellement s�lectionn�e
    private GameObject currentSelectedWatch;

    // �tat de gel de la montre s�lectionn�e
    private bool isWatchFrozen = false;

    // Position � laquelle la montre a �t� gel�e
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

    // --- Gestion de la Montre S�lectionn�e ---

    // D�finit la montre sur laquelle les actions seront appliqu�es
    public void SetSelectedWatch(GameObject watch)
    {
        if (currentSelectedWatch != watch)
        {
            // Quand on s�lectionne une nouvelle montre, l'ancienne n'est plus "gel�e"
            if (currentSelectedWatch != null && isWatchFrozen)
            {
                // Note : Pour l'instant, on se contente de r�initialiser l'�tat isWatchFrozen.
                // L'effet concret de "d�gel" sera g�r� par la logique qui utilise isWatchFrozen.
                isWatchFrozen = false;
            }
            currentSelectedWatch = watch;
            isWatchFrozen = false; // La nouvelle montre n'est pas gel�e par d�faut

            // Enregistre sa position actuelle comme point de gel potentiel
            if (currentSelectedWatch != null)
            {
                frozenPosition = currentSelectedWatch.transform.position;
            }
        }
    }

    // D�s�lectionne la montre actuelle
    public void ClearSelectedWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = false; // La d�s�lection d�g�le implicitement
            currentSelectedWatch = null;
        }
    }


    // --- Fonctions des Boutons "Geler" et "D�geler" la POSITION ---

    // G�le la position de la montre s�lectionn�e.
    public void FreezeWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = true;
            // Enregistre la position actuelle de la montre pour la figer.
            frozenPosition = currentSelectedWatch.transform.position;
            // C'est cette variable 'isWatchFrozen' qui sera lue par le syst�me de mouvement
            // pour emp�cher la montre de bouger.
            // (Le code qui utilise cette variable pour bloquer le mouvement sera ajout� plus tard,
            // pour l'instant, on g�re juste l'�tat).
        }
    }

    // D�g�le la position de la montre s�lectionn�e.
    public void UnfreezeWatch()
    {
        if (currentSelectedWatch != null)
        {
            isWatchFrozen = false;
            // La montre peut de nouveau �tre d�plac�e.
        }
    }

    // --- Propri�t� pour l'�tat de gel (� lire par les syst�mes de mouvement externes) ---
    public bool IsWatchFrozen
    {
        get { return isWatchFrozen; }
    }
}