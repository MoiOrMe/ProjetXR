using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Nécessaire pour détecter la caméra XR

public class ArmPanelController : MonoBehaviour
{
    // Référence au GameObject du panneau UI à activer/désactiver
    [Tooltip("Glissez ici le GameObject du Canvas de votre panneau de contrôle sur le bras (celui qui est enfant du contrôleur gauche).")]
    [SerializeField] private GameObject panelToActivate;

    // Référence à la caméra principale (la tête du joueur en VR)
    [Tooltip("Glissez ici le Transform de la Main Camera de votre XR Origin (chemin typique : XR Origin > Camera Offset > Main Camera).")]
    [SerializeField] private Transform headCamera;

    // Angle maximum (en degrés) entre le regard de la caméra et la direction du bras pour activer le panneau.
    [Tooltip("Angle max (en degrés) entre le regard de la tête et le bras pour activer le panneau. Plus petit = plus précis.")]
    [SerializeField] private float activationAngle = 45f;

    // Distance maximum entre la tête et le bras pour l'activation du panneau.
    [Tooltip("Distance max (en mètres) entre la tête et le bras pour l'activation. Évite l'activation si le bras est trop loin.")]
    [SerializeField] private float activationDistance = 0.8f; // Par exemple, 80 cm

    private bool isPanelActive = false; // État interne du panneau

    void Start()
    {
        // S'assurer que le panneau est désactivé au démarrage du jeu
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(false);
        }

        // Vérifier si la référence à la caméra est assignée. Si non, afficher une erreur.
        if (headCamera == null)
        {
            Debug.LogError("ArmPanelController: La 'Head Camera' n'est PAS assignée dans l'Inspector ! Veuillez glisser le Transform de la Main Camera de votre XR Origin.", this); // 'this' pour pointer l'erreur sur l'objet
            enabled = false; // Désactiver le script pour éviter les erreurs NullReference
        }
    }

    void Update()
    {
        // Vérifier si la caméra est assignée avant de tenter les calculs
        if (headCamera == null) return;

        CheckArmLook(); // Vérifie en permanence si l'utilisateur regarde son bras
    }

    private void CheckArmLook()
    {
        // S'assurer que les références nécessaires sont valides avant de continuer
        if (panelToActivate == null || headCamera == null) return;

        // Calculer la position globale du panneau (qui est sur le bras gauche)
        Vector3 panelWorldPosition = panelToActivate.transform.position;

        // Calculer la direction de la tête du joueur vers le panneau
        Vector3 directionToPanel = (panelWorldPosition - headCamera.position).normalized;

        // Obtenir la direction du regard de la caméra (où le joueur regarde)
        Vector3 lookDirection = headCamera.forward;

        // Calculer l'angle entre le regard du joueur et la direction du panneau
        float angle = Vector3.Angle(lookDirection, directionToPanel);

        // Calculer la distance entre la tête du joueur et le panneau
        float distance = Vector3.Distance(headCamera.position, panelWorldPosition);

        // Conditions pour activer ou désactiver le panneau
        if (angle < activationAngle && distance < activationDistance) // Le joueur regarde son bras et il est suffisamment proche
        {
            if (!isPanelActive) // Si le panneau est actuellement désactivé
            {
                panelToActivate.SetActive(true); // Activer le panneau
                isPanelActive = true;
            }
        }
        else // Le joueur ne regarde plus son bras ou il est trop loin
        {
            if (isPanelActive) // Si le panneau est actuellement activé
            {
                panelToActivate.SetActive(false); // Désactiver le panneau
                isPanelActive = false;
            }
        }
    }
}