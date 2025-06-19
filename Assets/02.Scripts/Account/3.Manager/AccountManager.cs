

public class AccountManager : Singleton<AccountManager>
{
    private Account _userAccount;
    public AccountDTO UserAccount => _userAccount.ToDTO();

    private AccountRepository _repository;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }


    public bool TryRegister(string email, string passward, string nickname)
    {
        AccountDTO newAccount = new AccountDTO(email, passward, nickname);
        return false;
    }

    public bool TryLogin()
    {
        return false;
    }
}
