using UnityEngine;
using System.Collections.Generic;

public class CommentsUI : MonoBehaviour
{
    [SerializeField] private CommentSlotUI _commentSlotUIPrefab;
    [SerializeField] private Transform _slotParent;
    private List<CommentSlotUI> _commentSlots;

    private List<CommentDTO> _commentDTOs;

    private void Start()
    {
        _commentSlots = new List<CommentSlotUI>();
        // 테스트용
        CommentManager.Instance.GetComments("PostID");
    }

    private void Update()
    {
        // 테스트용
        if(Input.GetKeyDown(KeyCode.A))
        {
            _commentDTOs = CommentManager.Instance.CurrentPostComments;

            Refresh();
        }
    }

    public void PostClickLoad()
    {
        // 게시글을 클릭했을 경우 그 게시글의 id에 맞게 댓글 받아옴
    }

    public void Refresh()
    {
        // 기존 슬롯 제거
        foreach (var slot in _commentSlots)
        {
            Destroy(slot.gameObject);
        }
        _commentSlots.Clear();

        // 새 슬롯 생성
        foreach (CommentDTO dto in _commentDTOs)
        {
            CommentSlotUI newSlot = Instantiate(_commentSlotUIPrefab, _slotParent);
            newSlot.RefreshCommentSlot(dto);
            _commentSlots.Add(newSlot);
        }
    }
}
