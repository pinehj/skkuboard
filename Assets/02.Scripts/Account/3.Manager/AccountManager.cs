using System.Threading.Tasks;
using UnityEngine;

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


    public async Task<bool> TryRegister(string email, string passward, string nickname)
    {
        AccountDTO newAccount = new AccountDTO(email, CryptoUtil.Encryption(passward, email), nickname);
        try
        {
            await _repository.Register(newAccount);
            Debug.LogError("회원가입에 성공하였습니다.");
            return true;    
        }
        catch(System.Exception e)
        {
            Debug.LogError($"회원가입에 실패하였습니다.: {e}");
            return false;
        }
    }

    public bool TryLogin()
    {
        return false;
    }
}
