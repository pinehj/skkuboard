using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 글을 클릭했을 경우, 그 글의 댓글들 불러오기
/// 댓글 쓰기, 댓글 수정, 자신이 단 댓글 삭제 가능
/// </summary>
public class CommentManager : MonoBehaviour
{
    // 싱글톤 추가
    public static CommentManager Instance;
    private List<Comment> _currentPostComment;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {

    }
}