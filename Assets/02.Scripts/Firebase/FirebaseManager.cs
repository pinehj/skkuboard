using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase;

public class FirebaseManager : Singleton<FirebaseManager>
{
    private FirebaseApp _app;
    public FirebaseApp APP => _app;

    private FirebaseFirestore _db;
    public FirebaseFirestore DB => _db;

    protected override void Awake()
    {
        Init();
    }

    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;
                Debug.Log("파이어베이스 연동 완료");
            }
            else
            {
                Debug.LogError($"파이어베이스 연동 실패: {dependencyStatus}");
            }
        });
    }
}
