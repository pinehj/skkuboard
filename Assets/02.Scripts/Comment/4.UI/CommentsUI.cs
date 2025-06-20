using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CommentsUI : MonoBehaviour
{
    [SerializeField] private CommentPostUI _commentPost;
    [SerializeField] private CommentSlotUI _commentSlotUIPrefab;
    [SerializeField] private Transform _slotParent;
    private List<CommentSlotUI> _commentSlots;

    private List<CommentDTO> _commentDTOs;

    private void Start()
    {
        _commentSlots = new List<CommentSlotUI>();


        CommentManager.Instance.OnLoadPostComments += Refresh;
        Debug.Log("refresh is added to loadpostcomments");
    }

    private void Update()
    {
        // 테스트용
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Refresh();
        }
    }

    public void PostClickLoad()
    {
        // 게시글을 클릭했을 경우 그 게시글의 id에 맞게 댓글 받아옴
    }

    private async void HandleDeleteSlotRequest(CommentSlotUI slot)
    {
        await CommentManager.Instance.DeleteComment(slot.GetDTO().CommentID);

        _commentSlots.Remove(slot);
        Destroy(slot.gameObject);

        Refresh();
    }

    public void Refresh()
    {
        _commentDTOs = CommentManager.Instance.CurrentPostComments;
        Debug.Log($"[CommentsUI] Refresh 시작, 댓글 수: {_commentDTOs?.Count ?? -1}");

        // 기존 슬롯 제거
        foreach (var slot in _commentSlots)
        {
            Destroy(slot.gameObject);
        }
        _commentSlots.Clear();

        // 새 슬롯 생성
        foreach (CommentDTO dto in _commentDTOs)
        {
            Debug.Log($"[CommentsUI] 댓글 슬롯 생성: {dto.WriterName} / {dto.Content}");

            CommentSlotUI newSlot = Instantiate(_commentSlotUIPrefab, _slotParent);
            if (newSlot == null)
            {
                Debug.LogError("[CommentsUI] 프리팹 인스턴스화 실패");
                return;
            }

            newSlot.RefreshCommentSlot(dto);
            newSlot.OnRequestDelete += HandleDeleteSlotRequest;
            _commentSlots.Add(newSlot);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_slotParent.GetComponent<RectTransform>());
    }

}
