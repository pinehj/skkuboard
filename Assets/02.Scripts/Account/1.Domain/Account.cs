using System;

public class Account
{
    public readonly string Email;
    public readonly string Passward;
    public readonly string Nickname;

    public Account(string email, string passward, string nickname)
    {
        EmailSpecification emailSpecification = new EmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMassage);
        }

        PasswardSpecification passwardSpecification = new PasswardSpecification();
        if (!passwardSpecification.IsSatisfiedBy(passward))
        {
            throw new Exception(passwardSpecification.ErrorMassage);
        }

        NicknameSpecification nicknameSpecification = new NicknameSpecification();
        if (!nicknameSpecification.IsSatifiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMassage);
        }

        Email = email;
        Passward = passward;
        Nickname = nickname;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(this);
    }
}
