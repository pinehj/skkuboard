using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountManager : Singleton<AccountManager>
{
    public AccountDTO User { get; private set; }

    private AccountRepository _repository;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TryDeleteAccount();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LogOut();
        }
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }

    
    public async Task<AccountResultMessage> TryRegister(string email, string passward, string nickname)
    {
        AccountDTO newAccount = new AccountDTO(email, CryptoUtil.Encryption(passward, email), nickname);
        return await _repository.Register(newAccount);
    }

    public async Task<AccountResultMessage> TryLogin(string email, string passward)
    {
        AccountDTO loginAccount = new AccountDTO(email, CryptoUtil.Encryption(passward, email), "");

        AccountResultMessage result = await _repository.Login(loginAccount);
        if (result.IsSuccess)
        {
            User = new AccountDTO(_repository.User.Email, "PASSWARD", _repository.User.DisplayName);
            Debug.Log($"{User.Nickname} :: {User.Email}");
            SceneManager.LoadScene(1);
        }

        return result;
    }

    public async Task<AccountResultMessage> TryDeleteAccount()
    {
        return await _repository.DeleteAccount();
    }

    public async Task<AccountResultMessage> TryChangePassward(string email, string passward)
    {
        AccountDTO newPasswardDTO = new AccountDTO(email, CryptoUtil.Encryption(passward, User.Email), "");
        return await _repository.ChangePassward(newPasswardDTO);
    }

    public void LogOut()
    {
        User = null;
        _repository.Logout();
        SceneManager.LoadScene(0);
    }
}
