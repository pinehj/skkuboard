using UnityEngine;

public class AccountDTO
{
    public readonly string Email;
    public readonly string Passward;
    public readonly string Nickname;

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
