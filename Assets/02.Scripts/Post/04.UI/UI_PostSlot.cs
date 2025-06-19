using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostSlot : MonoBehaviour
{
    [SerializeField] private string postID;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _postTimeText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likesCountText;

    [SerializeField] private Button _likeButton;
    [SerializeField] private Button _commentWriteButton;


    public void Refresh(PostDTO postDTO)
    {
        postID = postDTO.ID;
        _nicknameText.text = postDTO.Writer.Nickname.ToString();
        _postTimeText.text = postDTO.PostTime.ToString();
        _contentText.text = postDTO.Content;
        _likesCountText.text = $"좋아요 {postDTO.Likes.Count}";
    }

    // Todo: 취소 구현 / Post에 isLiked?
    public async void OnLikeButtonClick()
    {
        PostDTO updatedPostDTO = await PostManager.Instance.TryLike(postID, new User()
        {
            Nickname = "좋아요한사람",
            Email = "좋아요이메일"
        });

        if(updatedPostDTO != null)
        {
            Color color = _likeButton.image.color;
            color.r = 1;
            _likeButton.image.color = color;

            Refresh(updatedPostDTO);
        }
    }
}
