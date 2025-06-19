using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _writerName;
    [SerializeField] private TextMeshProUGUI _postTime;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private Button _deleteButton;

    public void RefreshCommentSlot(CommentDTO dto)
    {
        _writerName.text = dto.WriterName;
        _postTime.text = dto.PostTime.ToString();
        _content.text = dto.Content;

        // 현재 계정과 같으면 삭제 버튼 활성화

    }
}