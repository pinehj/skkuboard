using UnityEngine;
using UnityEngine.UI;

public class UI_Board : MonoBehaviour
{
    private UI_PostSlot _slots;
    [SerializeField] private Transform _slotContainer;
    [SerializeField] private UI_PostSlot _slotPrefab;
    [SerializeField] private Button _writeButton;
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        Refresh();
    }
    public async void Refresh()
    {
        if(await PostManager.Instance.TryLoadPosts())
        {
            foreach(PostDTO post in PostManager.Instance.Posts)
            {
                UI_PostSlot newSlot = Instantiate(_slotPrefab, _slotContainer);
                newSlot.Refresh(post);
            }
            Debug.Log("새로고침 성공");
        }
        Debug.Log("새로고침 실패");
    }

    public void WritePost()
    {
        
    }
}
