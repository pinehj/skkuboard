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

    private void Start()
    {
        Refresh();
        _postWritePanel.OnPosted += Refresh;
    }

    public async void Refresh()
    {
        gameObject.SetActive(true);
        _slots = _slotContainer.GetComponentsInChildren<UI_PostSlot>().ToList();

        if (await PostManager.Instance.TryLoadPosts())
        {
            int slotIndex = 0;
            List<PostDTO> posts = PostManager.Instance.Posts;
            for(int i = 0; i< posts.Count; i++)
            {
                if (_slots.Count > i)
                {
                    _slots[slotIndex].Refresh(posts[i]);
                    ++slotIndex;
                }
                else
                {
                    UI_PostSlot newSlot = Instantiate(_slotPrefab, _slotContainer);
                    newSlot.Refresh(posts[i]);
                }
            }

            for(int i = _slots.Count - 1; i>=slotIndex; --i)
            {
                UI_PostSlot deleteSlot = _slots[i];
                _slots.RemoveAt(i);
                Destroy(deleteSlot.gameObject);

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
        gameObject.SetActive(false);
    }
}
