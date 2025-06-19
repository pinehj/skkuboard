using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public struct PostAndComments
{
    public string PostID;
    public List<Comment> Comments;

    public PostAndComments(string postID, List<Comment> comments)
    {
        PostID = postID;
        Comments = comments;
    }
}

/// <summary>
/// 처음 글 - 글에 대한 댓글들로 struct 만들어서 가지고 있기
/// 글을 클릭했을 경우, 그 글의 댓글들 불러오기-ui
/// 댓글 쓰기, 댓글 수정, 자신이 단 댓글 삭제 가능
/// </summary>
public class CommentManager : Singleton<CommentManager>
{
    private string _currentPostID;
    private List<Comment> _currentPostComments;
    public List<Comment> CurrentPostComments => _currentPostComments;

    private List<PostAndComments> _postAndComments;

    private CommentRepository _repository;

    protected override void Awake()
    {
        Init();
    }

    private void Init()
    {
        _repository = new CommentRepository();
        _postAndComments = new List<PostAndComments>();

        _ = LoadAllComments();
    }

    private async Task LoadAllComments()
    {
        List<string> postIDs = await _repository.GetAllPostIDs();

        foreach (string postID in postIDs)
        {
            List<CommentDTO> commentDTOs = await _repository.GetComments(postID);
            List<Comment> comments = commentDTOs.ConvertAll(dto => new Comment(dto));
            _postAndComments.Add(new PostAndComments(postID, comments));
        }

        Debug.Log($"총 {postIDs.Count}개의 게시글 댓글 불러오기 완료");
    }

    public void GetComments(string postID)
    {
        _currentPostID = postID;
        var entry = _postAndComments.Find(p => p.PostID == postID);
        _currentPostComments = entry.Comments;
    }

    public async void AddComment(string postID, string content)
    {
        string commentID = Guid.NewGuid().ToString();
        /*string writerName = LoginManager.Instance.CurrentUserName;
        string writerEmail = LoginManager.Instance.CurrentUserEmail;*/

        var dto = new CommentDTO
        {
            PostID = postID,
            CommentID = commentID,
            Content = content,/*
            WriterName = writerName,
            WriterEmail = writerEmail,*/
            PostTime = DateTime.Now
        };

        await _repository.AddComment(dto);

        // 로컬에도 반영
        Comment newComment = new Comment(dto);
        _currentPostComments.Add(newComment);
    }
}