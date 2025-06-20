using UnityEngine;

public class UI_PostDetail : MonoBehaviour
{
    public void Init(PostDTO postDTO)
    {
        gameObject.SetActive(true);
        CommentManager.Instance.GetComments(postDTO.ID);
    }
}
