using UnityEngine;
using System.Collections.Generic;

public class WatchManager : MonoBehaviour
{
    public static WatchManager Instance { get; private set; }

    // La liste des montres peut toujours �tre utile pour d'autres op�rations globales
    [Tooltip("Glissez ici tous les GameObjects de vos montres.")]
    [SerializeField] private List<GameObject> allWatches;

    // �tat de gel (si vous utilisez toujours cette fonctionnalit�)
    private bool isWatchFrozen = false;
    private Vector3 frozenPosition;

    // La montre actuellement grab�e, pour les fonctions globales comme FreezeWatch()
    private GameObject currentlyGrabbedWatchInstance; // Nouveau nom pour �viter confusion

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

    // Vous pouvez mettre � jour cette variable via les �v�nements de grab du XRGrabInteractable
    // si vous avez besoin de savoir quelle montre est activement tenue par le joueur
    public void SetCurrentlyGrabbedWatch(GameObject watch)
    {
        currentlyGrabbedWatchInstance = watch;
        Debug.Log("WatchManager: Montre actuellement saisie : " + watch.name);
    }

    public void ClearCurrentlyGrabbedWatch()
    {
        if (currentlyGrabbedWatchInstance != null)
        {
            Debug.Log("WatchManager: Montre " + currentlyGrabbedWatchInstance.name + " n'est plus saisie.");
            currentlyGrabbedWatchInstance = null;
        }
    }

    // Fonctions de Geler/D�geler, elles agiront sur la montre actuellement saisie
    public void FreezeWatch()
    {
        if (currentlyGrabbedWatchInstance != null)
        {
            isWatchFrozen = true;
            frozenPosition = currentlyGrabbedWatchInstance.transform.position;
            Debug.Log("WatchManager: " + currentlyGrabbedWatchInstance.name + " est marqu�e comme GEL�E.");
            // Si vous voulez vraiment qu'elle se fige, vous pourriez d�sactiver le Rigidbody.isKinematic ici.
            // Rigidbody rb = currentlyGrabbedWatchInstance.GetComponent<Rigidbody>();
            // if (rb != null) rb.isKinematic = true; // Exemple
        }
        else
        {
            Debug.LogWarning("WatchManager: Aucune montre saisie pour geler.");
        }
    }

    public void UnfreezeWatch()
    {
        if (currentlyGrabbedWatchInstance != null)
        {
            isWatchFrozen = false;
            Debug.Log("WatchManager: " + currentlyGrabbedWatchInstance.name + " est marqu�e comme D�GEL�E.");
            // Si vous voulez vraiment la d�geler et qu'elle retombe/redevienne physique:
            // Rigidbody rb = currentlyGrabbedWatchInstance.GetComponent<Rigidbody>();
            // if (rb != null) rb.isKinematic = false; // Exemple
        }
        else
        {
            Debug.LogWarning("WatchManager: Aucune montre saisie pour d�geler.");
        }
    }

    public bool IsWatchFrozen
    {
        get { return isWatchFrozen; }
    }
}