using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountManager : Singleton<AccountManager>
{
    private Account _userAccount;
    public AccountDTO UserAccount => _userAccount.ToDTO();

    public FirebaseUser User { get; private set; }

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

    
    public async Task<NewAccountResultMessage> TryRegister(string email, string passward, string nickname)
    {
        AccountDTO newAccount = new AccountDTO(email, CryptoUtil.Encryption(passward, email), nickname);
        return await _repository.Register(newAccount);
    }

    public async Task<NewAccountResultMessage> TryLogin(string email, string passward)
    {
        AccountDTO loginAccount = new AccountDTO(email, CryptoUtil.Encryption(passward, email), "");

        NewAccountResultMessage result = await _repository.NewLogin(loginAccount);
        if (result.IsSuccess && result.User != null)
        {
            // _userAccount = new Account(result.DTO);
            User = result.User;
            Debug.Log($"{User.DisplayName} :: {User.Email} :: {User.UserId}");
            SceneManager.LoadScene(1);
        }

        return result;
    }

    public async Task<AccountResultMessage> TryDeleteAccount()
    {
        return await _repository.DeleteAccount(UserAccount);
    }

    public void LogOut()
    {
        _userAccount = null;
        SceneManager.LoadScene(0);
    }
}
