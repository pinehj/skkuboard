using UnityEngine;
using System.Collections.Generic;

public class CommentsUI : MonoBehaviour
{
    [SerializeField] private CommentSlotUI _commentSlotUIPrefab;
    private List<CommentSlotUI> _commentSlots;
}
