using Firebase.Firestore;


[FirestoreData]
public class AccountDTO
{
    
    [FirestoreProperty("email")] public string Email { get; set;}
    [FirestoreProperty("passward")] public string Passward { get; set; }
    [FirestoreProperty("nickname")] public string Nickname { get; set; }

    public AccountDTO()
    {

    }

    public AccountDTO(string email, string passward, string nickname)
    {
        Email = email;
        Passward = passward;
        Nickname = nickname;
    }

    public AccountDTO(Account account)
    {
        Email = account.Email;
        Passward = account.Passward;
        Nickname = account.Nickname;
    }
}
