using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentPostUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _postButton;
    [SerializeField] private Button _likeButton;

    [SerializeField] private CommentsUI _commentsUI; // 댓글 리스트 새로고침용

    private void Start()
    {
        _postButton.onClick.AddListener(OnPostButtonClicked);
    }

    private void OnPostButtonClicked()
    {
        string content = _inputField.text.Trim();

        if (string.IsNullOrEmpty(content))
        {
            Debug.LogWarning("댓글 내용이 비어 있습니다.");
            return;
        }

        CommentManager.Instance.AddComment(content);

        _inputField.text = "";

        // 즉시 리스트 새로고침
        _commentsUI.Refresh();
    }
}