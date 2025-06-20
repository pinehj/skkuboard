using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;


public class AccountRepository
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    public FirebaseUser User => _user;

    public AccountRepository()
    {
        Init();
    }

    ~AccountRepository()
    {
        OnDestroy();
    }

    private void Init()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void OnDestroy()
    {
        Logout();
        _auth.StateChanged -= AuthStateChanged;
        _auth = null;
    }

    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            bool isLogin = (_user != _auth.CurrentUser) && (_auth.CurrentUser != null);
            if (!isLogin && _user != null)
            {
                Debug.Log($"로그아웃 됨 {_user.UserId}");
            }
            _user = _auth.CurrentUser;
            if (isLogin)
            {
                Debug.Log($"로그인 됨 {_user.UserId}");
            }
        }
    }

    public FirebaseUser CurrentUser()
    {
        if (_auth.CurrentUser == null)
        {
            Debug.LogError("로그인 상태가 아닙니다.");
            return null;
        }

        return _auth.CurrentUser;
    }

    public async Task<AccountResultMessage> Register(AccountDTO accountDTO)
    {
        try
        {
            AuthResult result = await _auth.CreateUserWithEmailAndPasswordAsync(accountDTO.Email, accountDTO.Passward);
            if (result.User != null)
            {
                await result.User.UpdateUserProfileAsync(new UserProfile { DisplayName = accountDTO.Nickname });
            }
            return new AccountResultMessage()
            {
                MessageText = $"회원 가입에 성공하였습니다.",
                IsSuccess = true
            };
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e);
            return new AccountResultMessage()
            {
                MessageText = $"회원 가입에 실패하였습니다.\n {e.ErrorCode}-{e.Message}",
                IsSuccess = false
            };
        }
    }

    public async Task<AccountResultMessage> Login(AccountDTO accountDTO)
    {
        try
        {
            AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(accountDTO.Email, accountDTO.Passward);
            return new AccountResultMessage() { MessageText = "로그인에 성공하였습니다.", IsSuccess = true };
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e);
            return new AccountResultMessage() { MessageText = $"이메일 혹은 비밀번호가 잘못되었습니다.", IsSuccess = false };
        }
    }

    public void Logout()
    {
        if (_user == null)
        {
            Debug.LogError("로그인 상태가 아닙니다.");
        }

        _auth.SignOut();
        _user = null;
        Debug.Log("로그아웃 완료");
    }

    public async Task<AccountResultMessage> DeleteAccount()
    {
        if (_user != null)
        {
            return new AccountResultMessage() { MessageText = "로그인 상태가 아닙니다.", IsSuccess = false };
        }

        try
        {
            await _user.DeleteAsync();
            Debug.Log("계정이 삭제되었습니다.");
            return new AccountResultMessage() { MessageText = "계정이 삭제되었습니다.", IsSuccess = true };
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e);
            return new AccountResultMessage() { MessageText = e.Message, IsSuccess = false };
        }
    }
}
