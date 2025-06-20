using UnityEngine;

public class UI_PostDetail : MonoBehaviour
{
    [SerializeField] private UI_PostSlot _postSlot;
    [SerializeField] private UI_Board _ui_Board;
    [SerializeField] private PostDTO _postDTO;

    private void Start()
    {
        _ui_Board.PostModifyPanel.OnUploaded += Refresh;
    }

    public async void Refresh()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        PostDTO refreshedDto = await PostManager.Instance.TryLoadPost(_postDTO.ID);
        if(refreshedDto != null)
        {
            _postDTO = refreshedDto;
            Refresh(_postDTO);
        }
    }
    public void Refresh(PostDTO postDTO)
    {
        _postDTO = postDTO;
        _postSlot.Refresh(postDTO);
        _postSlot.UI_Board = _ui_Board;
        CommentManager.Instance.GetComments(postDTO.ID);
        Debug.Log($"CommentManager에게 postid {postDTO.ID} 전달");
        gameObject.SetActive(true);
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        _ui_Board.Refresh();
    }

    public async void OnDeleteButtonClick()
    {
        if (await PostManager.Instance.TryDeletePost(_postDTO.ID))
        {
            gameObject.SetActive(false);
            _ui_Board.Refresh();
        }
    }

    public void OnModifyButtonClick()
    {
        _ui_Board.PostModifyPanel.StartWrite(_postDTO);
    }
}
