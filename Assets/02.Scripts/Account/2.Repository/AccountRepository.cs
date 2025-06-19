using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;

public class AccountRepository
{
    public async Task Register(AccountDTO accountDTO)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("users").Document(accountDTO.Email);
        Debug.Log("DB통신중...");
        await docRef.SetAsync(accountDTO);
    }

    public async Task Login(AccountDTO accountDTO)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("users").Document(accountDTO.Email);
    }
}
