using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // N�cessaire pour d�tecter la cam�ra XR

public class ArmPanelController : MonoBehaviour
{
    // R�f�rence au GameObject du panneau UI � activer/d�sactiver
    [Tooltip("Glissez ici le GameObject du Canvas de votre panneau de contr�le sur le bras (celui qui est enfant du contr�leur gauche).")]
    [SerializeField] private GameObject panelToActivate;

    // R�f�rence � la cam�ra principale (la t�te du joueur en VR)
    [Tooltip("Glissez ici le Transform de la Main Camera de votre XR Origin (chemin typique : XR Origin > Camera Offset > Main Camera).")]
    [SerializeField] private Transform headCamera;

    // Angle maximum (en degr�s) entre le regard de la cam�ra et la direction du bras pour activer le panneau.
    [Tooltip("Angle max (en degr�s) entre le regard de la t�te et le bras pour activer le panneau. Plus petit = plus pr�cis.")]
    [SerializeField] private float activationAngle = 45f;

    // Distance maximum entre la t�te et le bras pour l'activation du panneau.
    [Tooltip("Distance max (en m�tres) entre la t�te et le bras pour l'activation. �vite l'activation si le bras est trop loin.")]
    [SerializeField] private float activationDistance = 0.8f; // Par exemple, 80 cm

    private bool isPanelActive = false; // �tat interne du panneau

    void Start()
    {
        // S'assurer que le panneau est d�sactiv� au d�marrage du jeu
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(false);
        }

        // V�rifier si la r�f�rence � la cam�ra est assign�e. Si non, afficher une erreur.
        if (headCamera == null)
        {
            Debug.LogError("ArmPanelController: La 'Head Camera' n'est PAS assign�e dans l'Inspector ! Veuillez glisser le Transform de la Main Camera de votre XR Origin.", this); // 'this' pour pointer l'erreur sur l'objet
            enabled = false; // D�sactiver le script pour �viter les erreurs NullReference
        }
    }

    void Update()
    {
        // V�rifier si la cam�ra est assign�e avant de tenter les calculs
        if (headCamera == null) return;

        CheckArmLook(); // V�rifie en permanence si l'utilisateur regarde son bras
    }

    private void CheckArmLook()
    {
        // S'assurer que les r�f�rences n�cessaires sont valides avant de continuer
        if (panelToActivate == null || headCamera == null) return;

        // Calculer la position globale du panneau (qui est sur le bras gauche)
        Vector3 panelWorldPosition = panelToActivate.transform.position;

        // Calculer la direction de la t�te du joueur vers le panneau
        Vector3 directionToPanel = (panelWorldPosition - headCamera.position).normalized;

        // Obtenir la direction du regard de la cam�ra (o� le joueur regarde)
        Vector3 lookDirection = headCamera.forward;

        // Calculer l'angle entre le regard du joueur et la direction du panneau
        float angle = Vector3.Angle(lookDirection, directionToPanel);

        // Calculer la distance entre la t�te du joueur et le panneau
        float distance = Vector3.Distance(headCamera.position, panelWorldPosition);

        // Conditions pour activer ou d�sactiver le panneau
        if (angle < activationAngle && distance < activationDistance) // Le joueur regarde son bras et il est suffisamment proche
        {
            if (!isPanelActive) // Si le panneau est actuellement d�sactiv�
            {
                panelToActivate.SetActive(true); // Activer le panneau
                isPanelActive = true;
            }
        }
        else // Le joueur ne regarde plus son bras ou il est trop loin
        {
            if (isPanelActive) // Si le panneau est actuellement activ�
            {
                panelToActivate.SetActive(false); // D�sactiver le panneau
                isPanelActive = false;
            }
        }
    }
}