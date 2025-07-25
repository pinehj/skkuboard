using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public struct User
{
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string Nickname { get; set; }
}


public struct AccountResultMessage
{
    public string MessageText;
    public bool IsSuccess;
}