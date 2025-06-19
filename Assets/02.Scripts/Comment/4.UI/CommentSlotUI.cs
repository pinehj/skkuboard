using TMPro;
using UnityEngine;

public class CommentSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _writerName;
    [SerializeField] private TextMeshProUGUI _postTime;
    [SerializeField] private TextMeshProUGUI _content;

    public void RefreshCommentSlot(CommentDTO dto)
    {
        _writerName.text = dto.WriterName;
        _postTime.text = dto.PostTime.ToString();
        _content.text = dto.Content;
    }
}
