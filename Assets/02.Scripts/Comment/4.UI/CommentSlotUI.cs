using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _writerName;
    [SerializeField] private TextMeshProUGUI _postTime;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private Button _deleteButton;
    private CommentDTO _currentDTO;

    public void RefreshCommentSlot(CommentDTO dto)
    {
        _currentDTO = dto;
        _writerName.text = dto.WriterName;

        DateTime localtime = dto.PostTime.ToLocalTime();
        _postTime.text = localtime.ToString();
        _content.text = dto.Content;

        // 현재 계정과 같으면 삭제 버튼 활성화
        // if(AccountManager.Instance.UserAccount.Email == dto.WriterEmail)
        if("test@emai.com" == dto.WriterEmail)
        {
            _deleteButton.gameObject.SetActive(true);
        }
        else
        {
            _deleteButton.gameObject.SetActive(false);
        }
    }

    public async void OnDeleteButtonClicked()
    {
        await CommentManager.Instance.DeleteComment(_currentDTO.CommentID);
        Destroy(this.gameObject);
    }
}