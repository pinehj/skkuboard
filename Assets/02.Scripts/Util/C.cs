using Firebase.Auth;

public struct Writer
{
    public string email;
    public string nickname;
}


public struct AccountResultMessage
{
    public string MessageText;
    public AccountDTO DTO;
    public bool IsSuccess;
}

public struct NewAccountResultMessage
{
    public string MessageText;
    public FirebaseUser User;
    public bool IsSuccess;
}