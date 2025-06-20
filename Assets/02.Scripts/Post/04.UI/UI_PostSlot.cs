using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostSlot : MonoBehaviour
{
    [SerializeField] private PostDTO _postDTO;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _postTimeText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likesCountText;
    [SerializeField] private TextMeshProUGUI _commentsCountText;

    [SerializeField] private Button _modifyButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _likeButton;
    [SerializeField] private Button _commentButton;

    [SerializeField] public UI_Board UI_Board;
    public void Refresh(PostDTO postDTO)
    {
        _postDTO = postDTO;
        _nicknameText.text = postDTO.Writer.Nickname.ToString();
        _postTimeText.text = postDTO.PostTime.ToString();
        _contentText.text = postDTO.Content;
        _likesCountText.text = $"좋아요 {postDTO.Likes.Count}";
        _commentsCountText.text = $"댓글 {CommentManager.Instance.GetCommentCountForPost(postDTO.ID)}";

        if (postDTO.Likes.Contains(new User()
        {
            Email = AccountManager.Instance.User.Email,
            Nickname = AccountManager.Instance.User.Nickname
        }))
        {
            Color color = _likeButton.image.color;
            color.r = 1;
            _likeButton.image.color = color;
        }
        else
        {
            Color color = _likeButton.image.color;
            color.r = 0;
            _likeButton.image.color = color;
        }

        if(postDTO.Writer.Equals(new User()
        {
            Email = AccountManager.Instance.User.Email,
            Nickname = AccountManager.Instance.User.Nickname
        }))
        {
            _modifyButton.gameObject.SetActive(true);
            _deleteButton.gameObject.SetActive(true);
        }
        else
        {
            _modifyButton.gameObject.SetActive(false);
            _deleteButton.gameObject.SetActive(false);
        }
    }

    // Todo: 취소 구현 / Post에 isLiked?
    public async void OnLikeButtonClick()
    {
        PostDTO updatedPostDTO = await PostManager.Instance.TryLike(_postDTO.ID, new User()
        {
            Email = AccountManager.Instance.User.Email,
            Nickname = AccountManager.Instance.User.Nickname
        });

        if(updatedPostDTO != null)
        {
            Refresh(updatedPostDTO);
        }
        else
        {
            Debug.Log("좋아요 실패");
        }
    }

    public void OnModifyButtonClick()
    {
        UI_Board.PostModifyPanel.StartWrite(_postDTO);
    }

    public async void OnDeleteButtonClick()
    {
        if(await PostManager.Instance.TryDeletePost(_postDTO.ID))
        {
            UI_Board.Refresh();
        }
    }

    public void GoDetail()
    {
        UI_Board.PostDetailPanel.Refresh(_postDTO);
    }
}
