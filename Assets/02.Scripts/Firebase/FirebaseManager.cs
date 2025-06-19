using UnityEngine;
using Firebase.Extensions;
using Firebase;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    public FirebaseApp _app;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                Debug.Log("파이어베이스 연동 완료");
            }
            else
            {
                Debug.LogError($"파이어베이스 연동 실패: {dependencyStatus}");
            }
        });
    }
}
