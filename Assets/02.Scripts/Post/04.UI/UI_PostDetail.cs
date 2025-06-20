using UnityEngine;

public class UI_PostDetail : MonoBehaviour
{
    [SerializeField] private UI_PostSlot _postSlot;
    [SerializeField] private UI_Board _ui_Board;
    public void Init(PostDTO postDTO)
    {
        gameObject.SetActive(true);
        _postSlot.Refresh(postDTO);
        _postSlot.UI_Board = _ui_Board;
        CommentManager.Instance.GetComments(postDTO.ID);
        Debug.Log($"CommentManager에게 postid {postDTO.ID} 전달");
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        _ui_Board.Refresh();
    }
}
