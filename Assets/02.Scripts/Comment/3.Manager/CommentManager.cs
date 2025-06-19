using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Android.Gradle.Manifest;

/// <summary>
/// 글을 클릭했을 경우, 그 글의 댓글들 불러오기
/// 댓글 쓰기, 댓글 수정, 자신이 단 댓글 삭제 가능
/// </summary>
public class CommentManager : Singleton<CommentManager>
{
    private string _currentPostID;
    private List<Comment> _currentPostComments;
    public List<Comment> CurrentPostComments => _currentPostComments;

    private CommentRepository _repository;

    protected override void Awake()
    {
        Init();
    }

    private async void Init()
    {
        _repository = new CommentRepository();

        List<CommentDTO> commentDTOs = await _repository.GetComments();
        _currentPostComments = commentDTOs.ConvertAll<Comment>(data => new Comment(data));
    }
}