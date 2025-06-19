using Firebase.Firestore;
using System.Threading.Tasks;
using System;
using UnityEngine;
using Firebase.Auth;


public class AccountRepository
{
    public async Task<NewAccountResultMessage> Register(AccountDTO accountDTO)
    {
        try
        {
            AuthResult result = await FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(accountDTO.Email, accountDTO.Passward);
            return new NewAccountResultMessage(){MessageText = $"회원 가입에 성공하였습니다.: {result.User.DisplayName} {result.User.UserId}",
                                                 User = result.User,
                                                 IsSuccess = true};
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return new NewAccountResultMessage(){MessageText = $"회원 가입에 실패하였습니다.\n {e.Message}",
                                                 User = null,
                                                 IsSuccess = false};
        }
    }

    public async Task<NewAccountResultMessage> NewLogin(AccountDTO accountDTO)
    {
        try
        {
            AuthResult result = await _auth.SignInWithEmailAndPasswordAsync(accountDTO.Email, accountDTO.Passward);
            return new NewAccountResultMessage(){ MessageText = "로그인에 성공하였습니다.", User = result.User, IsSuccess = true};
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return new NewAccountResultMessage() { MessageText = $"이메일 혹은 비밀번호가 잘못되었습니다.", User = null, IsSuccess = false };
        }
    }

    public async Task<AccountResultMessage> DeleteAccount(AccountDTO accountDTO)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("users").Document(accountDTO.Email);
        try
        {
            await docRef.DeleteAsync();
            return new AccountResultMessage() { MessageText = "계정이 삭제되었습니다.", IsSuccess = true };
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return new AccountResultMessage() { MessageText = e.Message, IsSuccess = false };
        }
    }
}
