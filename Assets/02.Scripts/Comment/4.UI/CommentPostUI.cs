using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentPostUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _postButton;
    [SerializeField] private Button _likeButton;
    [SerializeField] private Image _postButtonImage;

    private Color activeColor = new Color32(0xFA, 0xE1, 0x00, 0xFF);
    private Color inactiveColor = new Color32(0xE9, 0xE9, 0xE9, 0xFF);

    [SerializeField] private CommentsUI _commentsUI; // 댓글 리스트 새로고침용

    private void Start()
    {
        _postButton.onClick.AddListener(OnPostButtonClicked);
        _inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private async void OnPostButtonClicked()
    {
        string content = _inputField.text.Trim();

        if (string.IsNullOrEmpty(content))
        {
            Debug.Log("댓글 내용이 비어 있습니다.");
            return;
        }

        await CommentManager.Instance.AddComment(content);

        _inputField.text = "";

        // 즉시 리스트 새로고침
        _commentsUI.Refresh();
        StartCoroutine(ScrollToBottomNextFrame());
    }

    private void OnInputValueChanged(string input)
    {
        bool isEmpty = string.IsNullOrWhiteSpace(input);
        _postButtonImage.color = isEmpty ? inactiveColor : activeColor;
        _postButton.interactable = !isEmpty;
    }

    private System.Collections.IEnumerator ScrollToBottomNextFrame()
    {
        yield return null; // 다음 프레임까지 대기
        _commentsUI.ScrollDown();
    }
}