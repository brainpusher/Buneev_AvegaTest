using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultController : MonoBehaviour
{
    [SerializeField] private HealthManager playerHealthManager;

    private void OnEnable()
    {
        playerHealthManager.OnDied += ReloadSceneOnPlayerDeath;
    }

    private void OnDisable()
    {
        playerHealthManager.OnDied -= ReloadSceneOnPlayerDeath;
    }

    private void ReloadSceneOnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
