using UnityEngine;
using UnityEngine.SceneManagement;

public class ForSceneChange : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
