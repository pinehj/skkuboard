using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostModify : MonoBehaviour
{
    [SerializeField] private string _postID;

    [SerializeField] private TMP_InputField _inputField;
    public event Action OnUploaded;

    public void StartWrite(PostDTO post)
    {
        gameObject.SetActive(true);
        _postID = post.ID;
        _inputField.text = post.Content;
    }
    public async void OnUploadButtonClicked()
    {
        // 글 내용 검증
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(_inputField.text))
        {
            Debug.Log(contentSpecification.ErrorMassage);
            return;
        }

        if (await PostManager.Instance.TryModifyPost(_postID, _inputField.text))
        {
            gameObject.SetActive(false);
            OnUploaded?.Invoke();
        }
        else
        {
            Debug.Log("수정 실패");
        }
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
