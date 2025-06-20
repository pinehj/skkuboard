using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Board : MonoBehaviour
{
    private List<UI_PostSlot> _slots;
    [SerializeField] private Transform _slotContainer;
    [SerializeField] private UI_PostSlot _slotPrefab;
    [SerializeField] private Button _writeButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private UI_PostWrite _postWritePanel;

    // 수정 필요
    [SerializeField] public UI_PostModify PostModifyPanel;
    [SerializeField] public UI_PostDetail PostDetailPanel;
    private void Start()
    {
        Refresh();
        _postWritePanel.OnUploaded += Refresh;
        PostModifyPanel.OnUploaded += Refresh;
        CommentManager.Instance.OnLoadAllComments += Refresh;
    }

    public async void Refresh()
    {
        _slots = _slotContainer.GetComponentsInChildren<UI_PostSlot>().ToList();

        if (await PostManager.Instance.TryLoadPosts())
        {
            int slotIndex = 0;
            List<PostDTO> posts = PostManager.Instance.Posts;
            Debug.Log($"슬롯 카운트{_slots.Count}");
            for(int i = 0; i< posts.Count; i++)
            {
                Debug.Log(i);
                if (_slots.Count > i)
                {
                    _slots[slotIndex].Refresh(posts[i]);
                    ++slotIndex;
                    Debug.Log("갱신");
                }
                else
                {
                    UI_PostSlot newSlot = Instantiate(_slotPrefab, _slotContainer);
                    newSlot.Refresh(posts[i]);
                    newSlot.UI_Board = this;
                    Debug.Log("생성");
                }
            }

            for(int i = _slots.Count - 1; i>=slotIndex; --i)
            {
                UI_PostSlot deleteSlot = _slots[i];
                _slots.RemoveAt(i);
                Destroy(deleteSlot.gameObject);
                Debug.Log("삭제");
            }
            Debug.Log("새로고침 성공");
        }
        else
        {
            Debug.Log("새로고침 실패");
        }
    }

    public void WritePost()
    {
        _postWritePanel.gameObject.SetActive(true);
    }
}
