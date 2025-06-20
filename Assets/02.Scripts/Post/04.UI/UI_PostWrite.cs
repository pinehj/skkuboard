using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostWrite : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    public event Action OnUploaded;

    private void OnEnable()
    {
         _inputField.text = string.Empty;
    }
    public async void OnUploadButtonClicked()
    {
        Debug.Log(PostManager.Instance.name);

        // 글 내용 검증
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(_inputField.text))
        {
            Debug.Log(contentSpecification.ErrorMassage);
            return;
        }

        if (await PostManager.Instance.TryAddPost(new User()
        {
            Email = AccountManager.Instance.User.Email,
            Nickname = AccountManager.Instance.User.Nickname
        }, _inputField.text))
        {
            gameObject.SetActive(false);
            OnUploaded?.Invoke();
        }
        else
        {
            Debug.Log("업로드 실패");
        }
    }


    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
