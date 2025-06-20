using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostWrite : MonoBehaviour
{
    [SerializeField] private Button _postButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private TextMeshProUGUI _inputField;

    public event Action OnPosted;
    public async void OnPostButtonClicked()
    {
        Debug.Log(PostManager.Instance.name);
        if(await PostManager.Instance.TryAddPost(new User()
        {
            Email = AccountManager.Instance.User.Email,
            Nickname = AccountManager.Instance.User.Nickname
        }, _inputField.text))
        {
            gameObject.SetActive(false);
            OnPosted?.Invoke();
        }
    }
}
