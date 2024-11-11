using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class EnemyCollisionHandler : MonoBehaviour
{
    private SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        // Busca el objeto que contiene el SceneTransitionManager en la escena
        sceneTransitionManager = FindFirstObjectByType<SceneTransitionManager>();

        if (sceneTransitionManager == null)
        {
            Debug.LogError("SceneTransitionManager no encontrado en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with Enemy. Notifying SceneTransitionManager...");

            // Notifica al SceneTransitionManager para iniciar la transición
            if (sceneTransitionManager != null)
            {
                sceneTransitionManager.StartBattle(transform.position);
            }
        }
    }
}
