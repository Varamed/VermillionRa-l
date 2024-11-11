using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public string battleSceneName = "BattleScene";
    public string explorationSceneName = "ExplorationScene";
    public float transitionDuration = 0.5f;

    private Vector3 playerPosition;

    void Start()
    {
        // Asegúrate de que este objeto no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }

    public void StartBattle(Vector3 position)
    {
        playerPosition = position;
        Debug.Log("Transition to battle starting.");

        // Sin animación, directamente cargamos la escena
        Invoke(nameof(LoadBattleScene), transitionDuration);
    }

    private void LoadBattleScene()
    {
        Debug.Log("Loading battle scene: " + battleSceneName);
        SceneManager.LoadSceneAsync(battleSceneName, LoadSceneMode.Single);
    }

    public void EndBattle()
    {
        // Sin animación, directamente cargamos la escena
        Invoke(nameof(LoadExplorationScene), transitionDuration);
    }

    private void LoadExplorationScene()
    {
        Debug.Log("Loading exploration scene: " + explorationSceneName);
        SceneManager.LoadSceneAsync(explorationSceneName, LoadSceneMode.Single);
        StartCoroutine(RestorePlayerPosition());
    }

    private IEnumerator RestorePlayerPosition()
    {
        yield return null;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
        }
    }
}
